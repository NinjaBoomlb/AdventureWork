using AdventureWork.Data;
using AdventureWork.Models;
using AdventureWork.Interface;
using Microsoft.EntityFrameworkCore;

namespace AdventureWork.Repo
{
    public class ProductCategoryRepo : IProductCategoryRepo
    {

        private readonly DataContext _dataContext;

        public ProductCategoryRepo(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<bool> CategoryExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateCategory(ProductCategory category)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCategory(ProductCategory category)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Product>> GetCategories(int idCategory)
        {
            var subcategoryIds = await _dataContext.ProductSubcategories.Where(p => p.ProductCategoryID == idCategory).Select(p => p.ProductSubcategoryID).ToListAsync();
            return await _dataContext.Product.Where(p => _dataContext.ProductSubcategories.Any(s => s.ProductSubcategoryID == p.ProductSubcategoryID && subcategoryIds.Contains(s.ProductSubcategoryID))).ToListAsync();
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCategory(ProductCategory category)
        {
            throw new NotImplementedException();
        }

    }
}
