using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public interface IShoppingListRepository
{
    public Task<GetShoppingList?> GetShoppingListById(int id);
    public Task<List<GetShoppingList>> GetAll();
    public Task<GetShoppingList?> CreateShoppingList(int recipeId);
    public Task RemoveShoppingList(int shoppingListId);
}