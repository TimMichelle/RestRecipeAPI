using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence.Models;
using RestRecipeApp.Persistence.Repositories;

namespace RestRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public ShoppingListController(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        // GET api/shoppinglist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingList>>> GetShoppingLists()
        {
            var shoppingLists = await _shoppingListRepository.GetAll();
            return Ok(shoppingLists);
        }
        // GET api/shoppinglistitem/{shoppingListId}
        [HttpGet("recipe/{recipeId}")]
        public async Task<ActionResult<ShoppingListItem>> GetShoppingListsForRecipe(int recipeId)
        {
            var shoppingListItems = await _shoppingListRepository.GetShoppingListsForRecipe(recipeId);

            if (shoppingListItems == null)
                return NotFound();

            return Ok(shoppingListItems);
        }

        // GET api/shoppinglist/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingList>> GetShoppingList(int id)
        {
            var shoppingList = await _shoppingListRepository.GetShoppingListById(id);

            if (shoppingList == null)
                return NotFound();

            return Ok(shoppingList);
        }

        // POST api/shoppinglist
        [HttpPost]
        public async Task<ActionResult<ShoppingList>> CreateShoppingList([BindRequired, FromBody] CreateShoppingListDto createShoppingListDto)
        {
            var createdShoppingList
                = await _shoppingListRepository.CreateShoppingList(createShoppingListDto.RecipeId);

            if (createdShoppingList != null)
                return CreatedAtAction(nameof(GetShoppingList), new { id = createdShoppingList.ShoppingListId },
                    createdShoppingList);
            return BadRequest("Could not create shopping list");
        }

        // DELETE api/shoppinglist/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingList(int id)
        {
            var shoppingList = await _shoppingListRepository.GetShoppingListById(id);

            if (shoppingList == null)
                return NotFound();

            await _shoppingListRepository.RemoveShoppingList(shoppingList.ShoppingListId);

            return NoContent();
        }
    }
}