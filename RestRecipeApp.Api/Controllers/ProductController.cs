using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Core.ResponseDto;
using RestRecipeApp.Persistence.Models;
using RestRecipeApp.Persistence.Repositories;
using RestRecipeApp.Validation;

namespace RestRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<List<GetProductDto>>> GetProducts()
        {
            var result = await _productRepository.GetProducts();
            return result.Right<ActionResult>(response =>
            {
                return Ok(response.Map(product => product.MapGetProductDto()));
            }).Left(BadRequest);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var productOrError = await _productRepository.GetProductById(id);
            return productOrError
                .Right<ActionResult>(foundProduct => Ok(foundProduct.MapGetProductDto()))
                .Left(_ => NotFound($"Could not find product with id: {id}"));
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduct(int id, UpdatedProductDto updatedProductDto)
        {
            if (id != updatedProductDto.Id)
            {
                return BadRequest("Ids are not the same");
            }

            var validator = new UpdatedProductDtoValidator();
            var validatedResult = validator.Validate(updatedProductDto);
            if (!validatedResult.IsValid)
            {
                return BadRequest($"Product is not valid - {string.Join(", ", validatedResult.Errors)}");
            }

            var updatedProduct = await _productRepository.UpdateProduct(updatedProductDto);
            return updatedProduct
                .Right<ActionResult>(product =>
                    CreatedAtAction("GetProduct", new { id = product.ProductId }, product))
                .Left(error => BadRequest(error.Message));
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(CreateProductDto product)
        {
            var validator = new CreateProductDtoValidator();
            var validatedResult = validator.Validate(product);
            if (!validatedResult.IsValid)
            {
                return BadRequest($"Product is not valid - {string.Join(", ", validatedResult.Errors)}");

            }

            var createdProductOrError = await _productRepository.CreateProduct(product);

            return createdProductOrError
                .Right<ActionResult>(createdProduct =>
                    CreatedAtAction("GetProduct", new { id = createdProduct.ProductId }, createdProduct))
                .Left(error => BadRequest(error.Message));
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var isFoundAndRemove = await _productRepository.RemoveProduct(id);
            if (!isFoundAndRemove)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}