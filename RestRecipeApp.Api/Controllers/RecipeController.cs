using Microsoft.AspNetCore.Mvc;
using RestRecipeApp.Core.ResponseDto;
using RestRecipeApp.Persistence.Repositories;
using RestRecipeApp.Core;
using RestRecipeApp.Core.RequestDto;

namespace RestRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _repository;

        public RecipeController(IRecipeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Recipe
        [HttpGet]
        public async Task<ActionResult<List<GetRecipeStepDto>>> GetRecipes()
        {
            var result = await _repository.GetRecipes();
            return result.Right<ActionResult>(response =>
            {
                return Ok(
                    response.Map(recipe => recipe.MapGetRecipeDto()));
            }).Left(BadRequest);
        }


        // GET: api/Recipe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetRecipeDto>> GetRecipe(int id)
        {
            var recipe = await _repository.GetRecipeById(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe.MapGetRecipeDto();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<GetRecipeDto>> PatchRecipe(int id, UpdatedRecipeDto updatedRecipeDto)
        {
            if (id != updatedRecipeDto.Id)
            {
                return BadRequest();
            }

            var updatedRecipe = await _repository.UpdateRecipe(updatedRecipeDto);
            return updatedRecipe
                .Right<ActionResult>(recipe =>
                    CreatedAtAction("GetRecipe", new { id = recipe.RecipeId }, recipe))
                .Left(error => BadRequest(error.Message));
        }


        // POST: api/Recipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetRecipeDto>> PostRecipe(CreateRecipeDto recipe)
        {
            var createdRecipeOrError = await _repository.CreateRecipe(recipe);
            return createdRecipeOrError
                .Right<ActionResult>(createdRecipe =>
                    CreatedAtAction("GetRecipe", new { id = createdRecipe.RecipeId }, recipe))
                .Left(error => BadRequest(error.Message));
        }

        // DELETE: api/Recipe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var isFoundAndRemove = await _repository.RemoveRecipe(id);
            if (!isFoundAndRemove)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}