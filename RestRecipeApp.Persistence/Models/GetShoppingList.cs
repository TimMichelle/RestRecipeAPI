namespace RestRecipeApp.Persistence.Models;

public class GetShoppingList
{
    public int ShoppingListId{ get; set; }
    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public List<GetShoppingListItem> Items { get; set; }
}