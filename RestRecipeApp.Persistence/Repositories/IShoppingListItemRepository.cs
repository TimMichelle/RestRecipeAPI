
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public interface IShoppingListItemRepository
{
    public Task<IEnumerable<ShoppingListItem>> GetAll();
    public Task<ShoppingListItem?> GetShoppingListItemByListId(int shoppingListId);
    public Task<ShoppingListItem> UpdateShoppingListItem(ShoppingListItem shoppingListItem);
    public Task RemoveShoppingListItem(ShoppingListItem shoppingListItem);
}