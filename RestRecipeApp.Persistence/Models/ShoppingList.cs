namespace RestRecipeApp.Persistence.Models;

public class ShoppingList
{
    public int ShoppingListId{ get; set; }
    public int RecipeId { get; set; }
    private ICollection<ShoppingListItem> Items { get; set; }
}