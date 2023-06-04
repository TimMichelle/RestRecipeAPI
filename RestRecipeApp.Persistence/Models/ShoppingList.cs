using LanguageExt;
using RestRecipeApp.Persistence.Repositories;

namespace RestRecipeApp.Persistence.Models;

public class ShoppingList
{
    public int ShoppingListId{ get; set; }
    public int RecipeId { get; set; }
    public IEnumerable<ShoppingListItem> Items { get; set; }
}