using Microsoft.AspNetCore.Http;

namespace RestRecipeApp.Core.RequestDto;

public class CreateRecipeImageDto
{
    public string Name { get; set; }
    public IFormFile File { get; set; }
}