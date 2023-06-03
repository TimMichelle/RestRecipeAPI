namespace RestRecipeApp.Persistence.Models;

public class ShoppingList
{
    public int ShoppingListId{ get; set; }
    public int RecipeId { get; set; }
    public ICollection<ShoppingListItem> Items { get; set; }
}