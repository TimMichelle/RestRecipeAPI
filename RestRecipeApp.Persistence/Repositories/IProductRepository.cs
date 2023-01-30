using LanguageExt;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public interface IProductRepository
{
        public Task<Either<DbError, Product>> GetProductById(int id);
        public Task<Either<DbError, List<Product>>> GetProducts();
        public Task<bool> RemoveProduct(int id);
        public Task<Either<DbError, Product>> CreateProduct(CreateProductDto product);
        public Task<Either<DbError, Product>> UpdateProduct(UpdatedProductDto updatedProductDto);
}