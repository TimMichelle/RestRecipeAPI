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



        // PUT api/shoppinglistitem/{shoppingListId}/{shoppingListItemId}
        [HttpPut("{shoppingListId}/{shoppingListItemId}")]
        public async Task<IActionResult> UpdateShoppingListItem(int shoppingListId, int shoppingListItemId, ShoppingListItem updatedShoppingListItem)
        {
            var shoppingListItem = await _shoppingListItemRepository.GetShoppingListItemByListId(shoppingListItemId);

            if (shoppingListItem == null)
                return NotFound();

            shoppingListItem.IngredientId = updatedShoppingListItem.IngredientId;
            shoppingListItem.IsBought = updatedShoppingListItem.IsBought;

            var updatedItem =  await _shoppingListItemRepository.UpdateShoppingListItem(shoppingListItem);

            return CreatedAtAction(nameof(GetShoppingListItemById), new { shoppingListId = updatedItem.ShoppingListItemId },
                updatedItem);
        }

        // DELETE api/shoppinglistitem/{shoppingListId}/{shoppingListItemId}
        [HttpDelete("{shoppingListId}/{shoppingListItemId}")]
        public async Task<IActionResult> DeleteShoppingListItem(int shoppingListId, int shoppingListItemId)
        {
            var shoppingListItem = await _shoppingListItemRepository.GetShoppingListItemByListId(shoppingListItemId);

            if (shoppingListItem == null)
                return NotFound();

            await _shoppingListItemRepository.RemoveShoppingListItem(shoppingListItem);

            return NoContent();
        }
    }
}
