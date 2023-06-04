using Microsoft.EntityFrameworkCore;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public class ShoppingListItemRepository : IShoppingListItemRepository
{
    private readonly RecipesDbContext _context;

    public ShoppingListItemRepository(RecipesDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ShoppingListItem>> GetAll()
    {
        return await _context.ShoppingListItems.ToListAsync();
    }

    public async Task<ShoppingListItem?> GetShoppingListItemByListId(int shoppingListId)
    {
        return await _context.ShoppingListItems.FindAsync(shoppingListId);
    }

    public async Task<ShoppingListItem> UpdateShoppingListItem(ShoppingListItem shoppingListItem)
    {
        _context.ShoppingListItems.Update(shoppingListItem);
        await _context.SaveChangesAsync();

        return shoppingListItem;
    }

    public async Task RemoveShoppingListItem(ShoppingListItem shoppingListItem)
    {
        _context.ShoppingListItems.Remove(shoppingListItem);
        await _context.SaveChangesAsync();
    }


}