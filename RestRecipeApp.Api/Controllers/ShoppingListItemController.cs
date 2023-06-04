using Microsoft.AspNetCore.Mvc;
using RestRecipeApp.Persistence.Models;
using RestRecipeApp.Persistence.Repositories;

namespace RestRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListItemController : ControllerBase
    {
        private readonly IShoppingListItemRepository _shoppingListItemRepository;

        public ShoppingListItemController(IShoppingListItemRepository shoppingListItemRepository)
        {
            _shoppingListItemRepository = shoppingListItemRepository;
        }
        
        // GET api/shoppinglistitem/
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ShoppingListItem>>> GetShoppingListItems()
        {
            var shoppingListItems = await _shoppingListItemRepository.GetAll();

            if (shoppingListItems == null)
                return NotFound();

            return Ok(shoppingListItems);
        }

        // GET api/shoppinglistitem/{shoppingListId}
        [HttpGet("{shoppingListId}")]
        public async Task<ActionResult<ShoppingListItem>> GetShoppingListItemById(int shoppingListId)
        {
            var shoppingListItems = await _shoppingListItemRepository.GetShoppingListItemByListId(shoppingListId);

            if (shoppingListItems == null)
                return NotFound();

            return Ok(shoppingListItems);
        }

        // PUT api/shoppinglistitem/{shoppingListId}/{itemId}
        [HttpPut("{shoppingListId}/{itemId}")]
        public async Task<IActionResult> UpdateShoppingListItem(int shoppingListId, int itemId, ShoppingListItem updatedShoppingListItem)
        {
            var shoppingListItem = await _shoppingListItemRepository.GetShoppingListItemByListId(itemId);

            if (shoppingListItem == null)
                return NotFound();

            shoppingListItem.IngredientId = updatedShoppingListItem.IngredientId;
            shoppingListItem.IsBought = updatedShoppingListItem.IsBought;

            await _shoppingListItemRepository.UpdateShoppingListItem(shoppingListItem);

            return NoContent();
        }

        // DELETE api/shoppinglistitem/{shoppingListId}/{itemId}
        [HttpDelete("{shoppingListId}/{itemId}")]
        public async Task<IActionResult> DeleteShoppingListItem(int shoppingListId, int itemId)
        {
            var shoppingListItem = await _shoppingListItemRepository.GetShoppingListItemByListId(itemId);

            if (shoppingListItem == null)
                return NotFound();

            await _shoppingListItemRepository.RemoveShoppingListItem(shoppingListItem);

            return NoContent();
        }
    }
}
