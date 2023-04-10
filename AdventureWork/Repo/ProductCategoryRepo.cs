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
            var category = await _dataContext.ProductCategories.Where(p => p.ProductCategoryID == id).FirstOrDefaultAsync();
            return category != null;
        }

        public async Task<bool> CreateCategory(ProductCategory category)
        {
            await _dataContext.AddAsync(category);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategory(ProductCategory category)
        {
            _dataContext.Remove(category);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<Product>> GetCategories(int idCategory)
        {
            var subcategoryIds = await _dataContext.ProductSubcategories.Where(p => p.ProductCategoryID == idCategory).Select(p => p.ProductSubcategoryID).ToListAsync();
            return await _dataContext.Product.Where(p => _dataContext.ProductSubcategories.Any(s => s.ProductSubcategoryID == p.ProductSubcategoryID && subcategoryIds.Contains(s.ProductSubcategoryID))).ToListAsync();
        }

        public async Task<bool> UpdateCategory(ProductCategory category)
        {

            var existingCategory = await this.GetCategory(category.ProductCategoryID);

            if (existingCategory != null)
            {
                _dataContext.Entry(existingCategory).State = EntityState.Detached;
            }
            _dataContext.Update(category);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<ProductCategory> GetCategory(int idCategory)
        {
            return await _dataContext.ProductCategories.Where(p => p.ProductCategoryID == idCategory).FirstOrDefaultAsync();
        }
    }
}
