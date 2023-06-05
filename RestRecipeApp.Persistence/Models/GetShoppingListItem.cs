using RestRecipeApp.Core.Domain;

namespace RestRecipeApp.Persistence.Models;

public class GetShoppingListItem
{
    public int ShoppingListItemId{ get; set; }
    public int ShoppingListId{ get; set; }
    public int IngredientId { get; set; }
    public float Amount { get; set; }
    public UnitOfMeasurement UnitOfMeasurement { get; set; }
    public string IngredientName { get; set; }
    public bool IsBought { get; set; } = false; 
}