using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public class ShoppingListRepository : IShoppingListRepository
{
    public Task<ShoppingList> GetShoppingListById(int id)
    {
        throw new NotImplementedException();
    }
}