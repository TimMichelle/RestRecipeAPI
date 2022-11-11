
namespace RestRecipeApp.Persistence.Models;

public class RecipeStep
{
    public int RecipeStepId { get; set; }
    public int StepNumber { get; set; }
    public string Description { get; set; }
    public int RecipeId { get; set; }

}