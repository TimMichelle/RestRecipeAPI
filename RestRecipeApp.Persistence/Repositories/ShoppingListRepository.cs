using Microsoft.EntityFrameworkCore;
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

    public async Task<GetShoppingList?> GetShoppingListById(int id)
    {
        var shoppingList = await _context.ShoppingLists
            .Include(sl => sl.Items)
            .FirstOrDefaultAsync(sl => sl.ShoppingListId == id);

        if (shoppingList == null)
        {
            return null;
        }

        var items = GetShoppingListItems(shoppingList);

        return new GetShoppingList
        {
            ShoppingListId = shoppingList.ShoppingListId,
            RecipeId = shoppingList.RecipeId,
            Items = items
        };
    }


    public async Task<List<GetShoppingList>> GetAll()
    {
        var shoppingLists = await _context.ShoppingLists
            .Include(sl => sl.Items)
            .ToListAsync();
        return shoppingLists.Select(x =>
        {
            var items = GetShoppingListItems(x);
            return new GetShoppingList
            {
                ShoppingListId = x.ShoppingListId,
                RecipeId = x.RecipeId,
                Items = items
            };
        }).ToList();
    }

    public async Task<GetShoppingList?> CreateShoppingList(int recipeId)
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
        var result = await GetShoppingListById(createdShoppingList.Entity.ShoppingListId);
        return result;
    }

    public async Task RemoveShoppingList(int shoppingListId)
    {
        var shoppingList = _context.ShoppingLists.FirstOrDefault(x => x.ShoppingListId == shoppingListId);
        ;
        if (shoppingList == null)
        {
            return;
        }

        _context.ShoppingLists.Remove(shoppingList);
        await _context.SaveChangesAsync();
    }

    private List<GetShoppingListItem> GetShoppingListItems(ShoppingList shoppingList)
    {
        var items = shoppingList.Items.Select(item =>
        {
            var ingredient = _context.Ingredients
                .Include(i => i.Product)
                .FirstOrDefault(i => i.IngredientId == item.IngredientId);

            return new GetShoppingListItem
            {
                ShoppingListItemId = item.ShoppingListItemId,
                ShoppingListId = item.ShoppingListId,
                IngredientId = item.IngredientId,
                Amount = ingredient.Amount,
                UnitOfMeasurement = ingredient.UnitOfMeasurement,
                IngredientName = ingredient.Product.Name,
                IsBought = item.IsBought
            };
        }).ToList();
        return items;
    }
}