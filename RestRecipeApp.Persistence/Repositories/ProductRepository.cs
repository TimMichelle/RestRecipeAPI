using LanguageExt;
using Microsoft.EntityFrameworkCore;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly RecipesDbContext _recipesContext;

    public ProductRepository(RecipesDbContext recipesContext)
    {
        _recipesContext = recipesContext;
    }
    public async Task<Either<DbError, Product>> GetProductById(int id)
    {
        try
        {
            return await _recipesContext.Products
                .Where(foundProduct => foundProduct.ProductId == id)
                .FirstAsync();
        }
        catch (Exception error)
        {
            return new DbError($"Could not retrieve ingredient: ${id} - error: {error.Message}");
        }
    }

    public async Task<Either<DbError, List<Product>>> GetProducts()
    {
        return await _recipesContext.Products.ToListAsync();

    }

    public async Task<bool> RemoveProduct(int id)
    {
        var product = await _recipesContext.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        _recipesContext.Products.Remove(product);
        await _recipesContext.SaveChangesAsync();
        return true;
    }

    public async Task<Either<DbError, Product>> CreateProduct(CreateProductDto product)
    {
        try
        {
            var savedIngredient = await _recipesContext.Products.AddAsync(product.MapProduct());
            await _recipesContext.SaveChangesAsync();
            return savedIngredient.Entity;
        }
        catch (Exception e)
        {
            return new DbError($"Could not create ingredient: {e.Message}");
        }
    }

    public async Task<Either<DbError, Product>> UpdateProduct(UpdatedProductDto updatedProductDto)
    {
        var currentProduct = await _recipesContext.Products
            .FirstOrDefaultAsync(foundProduct => foundProduct.ProductId == updatedProductDto.Id);
        if (currentProduct == null)
        {
            return new DbError($"Could not find ingredient with id: {updatedProductDto.Id}");
        }

        currentProduct.Name = updatedProductDto.Name;

        try
        {
            _recipesContext.Entry(currentProduct).State = EntityState.Modified;
            await _recipesContext.SaveChangesAsync();
        }
        catch (Exception error)
        {
            return new DbError($"$Could not save changes to ingredient: {updatedProductDto.Id} - {error.Message}");
        }

        return currentProduct;
    }
}