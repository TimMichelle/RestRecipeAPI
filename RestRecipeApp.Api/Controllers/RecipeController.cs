using Microsoft.AspNetCore.Mvc;
using RecipesApp.Domain;
using RestRecipeApp.Core.RequestDto.Recipe;
using RestRecipeApp.Repositories;

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

        // PUT: api/Recipe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        // {
            // if (id != recipe.RecipeId)
            // {
            //     return BadRequest();
            // }
            //
            // _repository.Entry(recipe).State = EntityState.Modified;
            //
            // try
            // {
            //     await _repository.SaveChangesAsync();
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!RecipeExists(id))
            //     {
            //         return NotFound();
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }
            //
            // return NoContent();
        // }

        // POST: api/Recipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(CreateRecipeDto recipe)
        {
            var createdRecipe = await _repository.CreateRecipe(recipe);

            return CreatedAtAction("GetRecipe", new { id = createdRecipe.RecipeId }, recipe);
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

        // private bool RecipeExists(int id)
        // {
        //     // return (_repository.Recipes?.Any(e => e.RecipeId == id)).GetValueOrDefault();
        // }
    }
}