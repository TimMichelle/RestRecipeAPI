using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public interface IShoppingListRepository
{
    public Task<ShoppingList> GetShoppingListById(int id);
}