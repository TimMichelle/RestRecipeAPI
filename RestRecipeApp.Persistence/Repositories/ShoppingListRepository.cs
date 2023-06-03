using Microsoft.EntityFrameworkCore;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public class ShoppingListRepository : IShoppingListRepository
{
    private readonly RecipesDbContext _recipesContext;

    public ShoppingListRepository(RecipesDbContext recipesContext)
    {
        _recipesContext = recipesContext;
    }

    public Task<ShoppingList> GetShoppingListById(int id)
    {
        // return _recipesContext.ShoppingLists.Include(shoppingList => shoppingList.Items).
        throw new NotImplementedException();
    }
}