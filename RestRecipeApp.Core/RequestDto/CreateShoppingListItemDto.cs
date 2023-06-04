namespace RestRecipeApp.Core.RequestDto;

public class CreateShoppingListItemDto
{
    public int IngredientId { get; set; }
    public bool IsBought { get; set; } = false;
}