using Microsoft.AspNetCore.Mvc;
using RestRecipeApp.Persistence.Models;
using RestRecipeApp.Persistence.Repositories;

namespace RestRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<ShoppingList>> CreateShoppingList(ShoppingList shoppingList)
        {
            await _shoppingListRepository.CreateShoppingList(shoppingList);

            return CreatedAtAction(nameof(GetShoppingList), new { id = shoppingList.ShoppingListId }, shoppingList);
        }

        // PUT api/shoppinglist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingList(int id, ShoppingList updatedShoppingList)
        {
            var shoppingList = await _shoppingListRepository.GetShoppingListById(id);

            if (shoppingList == null)
                return NotFound();

            shoppingList.RecipeId = updatedShoppingList.RecipeId;
            shoppingList.Items = updatedShoppingList.Items;

            await _shoppingListRepository.UpdateShoppingList(shoppingList);

            return NoContent();
        }

        // DELETE api/shoppinglist/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingList(int id)
        {
            var shoppingList = await _shoppingListRepository.GetShoppingListById(id);

            if (shoppingList == null)
                return NotFound();

            await _shoppingListRepository.RemoveShoppingList(shoppingList);

            return NoContent();
        }
    }
}