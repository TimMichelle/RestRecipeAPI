using Microsoft.EntityFrameworkCore;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public class ShoppingListRepository : IShoppingListRepository
{
    private readonly RecipesDbContext _context;

    public ShoppingListRepository(RecipesDbContext context)
    {
        _context = context;
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

    public async Task<ShoppingList> CreateShoppingList(ShoppingList shoppingList)
    {
        _context.ShoppingLists.Add(shoppingList);
        await _context.SaveChangesAsync();
        return shoppingList;
    }

    public async Task<ShoppingList> UpdateShoppingList(ShoppingList shoppingList)
    {
        _context.ShoppingLists.Update(shoppingList);
        await _context.SaveChangesAsync();
        return shoppingList;
    }
    
    public async Task RemoveShoppingList(ShoppingList shoppingList)
    {
        _context.ShoppingLists.Remove(shoppingList);
        await _context.SaveChangesAsync();
    }
}