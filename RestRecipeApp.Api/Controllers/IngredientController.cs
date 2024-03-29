using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Core.ResponseDto;
using RestRecipeApp.Persistence.Repositories;
using RestRecipeApp.Validation;

namespace RestRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        // GET: api/Ingredient
        [HttpGet]
        public async Task<ActionResult<List<GetIngredientDto>>> GetIngredients()
        {
            var result = await _ingredientRepository.GetIngredients(null);

            return Ok(result.Map(ingredient => ingredient.MapGetIngredientDto()));
        }

        // GET: api/Ingredient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetIngredientDto>> GetIngredient(int id)
        {
            var ingredientOrError = await _ingredientRepository.GetIngredientById(id);

            return ingredientOrError
                .Right<ActionResult>(foundIngredient => Ok(foundIngredient.MapGetIngredientDto()))
                .Left(error => NotFound($"Error occured while retrieving ingredient: {error.Message}"));
        }

        // PUT: api/Ingredient/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<ActionResult<GetIngredientDto>> PatchIngredient(int id,
            UpdatedIngredientDto updatedIngredientDto)
        {
            if (id != updatedIngredientDto.Id)
            {
                return BadRequest("Ids are not the same");
            }

            var updateIngredient = await _ingredientRepository.UpdateIngredient(updatedIngredientDto);
            return updateIngredient
                .Right<ActionResult>(ingredient =>
                    CreatedAtAction("GetIngredient", new { id = ingredient.IngredientId }, ingredient))
                .Left(error => BadRequest(error.Message));
        }

        // POST: api/Ingredient
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetIngredientDto>> PostIngredient(CreateIngredientDto ingredient)
        {
            var validator = new CreateIngredientDtoValidator();
            var validatedResult = validator.Validate(ingredient);
            if (!validatedResult.IsValid)
            {
                return BadRequest($"Ingredient is not valid - {string.Join(", ", validatedResult.Errors)}");
            }

            var createdIngredientOrError = await _ingredientRepository.CreateIngredient(ingredient);
            return createdIngredientOrError
                .Right<ActionResult>(createdIngredient =>
                    CreatedAtAction("GetIngredient", new { id = createdIngredient.IngredientId }, createdIngredient))
                .Left(error => BadRequest(error.Message));
        }

        // DELETE: api/Ingredient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var isFoundAndRemove = await _ingredientRepository.RemoveIngredient(id);
            if (!isFoundAndRemove)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}