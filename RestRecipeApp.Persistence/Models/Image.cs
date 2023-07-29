namespace RestRecipeApp.Persistence.Models;

public class Image
{
    public int ImageId { get; set; }
    public string Name { get; set; }
    public byte[] Content { get; set; }
}