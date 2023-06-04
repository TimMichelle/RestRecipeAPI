using Microsoft.EntityFrameworkCore;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public class ShoppingListRepository : IShoppingListRepository
{
    private readonly RecipesDbContext _context;
    private readonly IIngredientRepository _ingredientRepository;

    public ShoppingListRepository(RecipesDbContext context, IIngredientRepository ingredientRepository)
    {
        _context = context;
        _ingredientRepository = ingredientRepository;
    }

    public async Task<ShoppingList?> GetShoppingListById(int id)
    {
        return await _context.ShoppingLists
            .Include(sl => sl.Items)
            .FirstOrDefaultAsync(sl => sl.ShoppingListId == id);
    }

    public async Task<List<ShoppingList>> GetAll()
    {
        return await _context.ShoppingLists
            .Include(sl => sl.Items)
            .ToListAsync();
    }

    public async Task<ShoppingList> CreateShoppingList(int recipeId)
    {
        var ingredients = await _ingredientRepository.GetIngredients(recipeId);
        var items = ingredients.Select(x => new ShoppingListItem
        {
            IngredientId = x.IngredientId,
            IsBought = false
        }).ToList();

        var createdShoppingList = _context.ShoppingLists.Add(new ShoppingList()
        {
            RecipeId = recipeId,
            Items = items
        });
        
        await _context.SaveChangesAsync();
        return createdShoppingList.Entity;
    }

    public async Task RemoveShoppingList(ShoppingList shoppingList)
    {
        _context.ShoppingLists.Remove(shoppingList);
        await _context.SaveChangesAsync();
    }
}