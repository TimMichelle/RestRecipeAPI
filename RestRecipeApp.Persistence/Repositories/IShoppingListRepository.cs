using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public interface IShoppingListRepository
{
    public Task<ShoppingList?> GetShoppingListById(int id);
    public Task<List<ShoppingList>> GetAll();
    public Task<ShoppingList> CreateShoppingList(ShoppingList shoppingList);
    public Task<ShoppingList> UpdateShoppingList(ShoppingList shoppingList);
    public Task RemoveShoppingList(ShoppingList shoppingList);
}