namespace RestRecipeApp.Persistence.Models;

public class ShoppingListItem
{
    public int ShoppingListItemId{ get; set; }
    public int ShoppingListId{ get; set; }
    public int IngredientId { get; set; }
    public bool IsBought { get; set; } = false;
}