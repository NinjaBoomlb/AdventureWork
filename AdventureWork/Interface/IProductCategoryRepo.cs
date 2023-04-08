using AdventureWork.Models;

namespace AdventureWork.Interface
{
    public interface IProductCategoryRepo
    {
        public Task<ICollection<Product>> GetCategories(int idCategory);
        public Task<ProductCategory> GetCategory(int id);
        public Task<bool> CategoryExists(int id);
        public Task<bool> CreateCategory(ProductCategory category);
        public Task<bool> UpdateCategory(ProductCategory category);
        public Task<bool> DeleteCategory(ProductCategory category);
    }
}
