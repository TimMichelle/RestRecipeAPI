using Microsoft.AspNetCore.Mvc;
using RestRecipeApp.Core.RequestDto.Recipe;
using RestRecipeApp.Persistence.Models;
using RestRecipeApp.Persistence.Repositories;

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
        public async Task<ActionResult> GetRecipes()
        {
            var result =  await _repository.GetRecipes();
            return result.Right<ActionResult>(response =>
            {
                return Ok(response);
            }).Left(BadRequest);
        }

        // GET: api/Recipe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _repository.GetRecipeById(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchRecipe(int id, UpdatedRecipeDto updatedRecipeDto)
        {
            if (id != updatedRecipeDto.RecipeId)
            {
                return BadRequest();
            }

            var updatedRecipe = await _repository.UpdateRecipe(updatedRecipeDto);
            return updatedRecipe.Right<ActionResult>((recipe) => CreatedAtAction("GetRecipe", new { id = recipe.RecipeId }, recipe))
                .Left(error => BadRequest(error.Message));
        }
    

        // POST: api/Recipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(CreateRecipeDto recipe)
        {
            var createdRecipeOrError = await _repository.CreateRecipe(recipe);
            return createdRecipeOrError
                .Right<ActionResult>(createdRecipe => CreatedAtAction("GetRecipe", new { id = createdRecipe.RecipeId }, recipe))
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