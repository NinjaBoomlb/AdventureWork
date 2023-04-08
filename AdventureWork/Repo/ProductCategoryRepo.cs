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

        public async Task<ICollection<ProductCategory>> GetCategories()
        {
            return await _dataContext.ProductCategories.OrderBy(p => p.ProductCategoryID).ToListAsync();
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
