using AdventureWork.Data;
using AdventureWork.Models;
using AdventureWork.Interface;
using Microsoft.EntityFrameworkCore;

namespace AdventureWork.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly DataContext _dataContext;

        public ProductRepo(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<ICollection<Product>> GetProducts()
        {
            return await _dataContext.Product.OrderBy(p => p.ProductID).ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _dataContext.Product.Where(p => p.ProductID == id).FirstOrDefaultAsync();
        }

        public async Task<bool> ProductExists(int id)
        {
            var product = await _dataContext.Product.Where(p => p.ProductID == id).FirstOrDefaultAsync();
            return product != null;
        }

        public async Task<bool> CreateProduct(Product product)
        {
            await _dataContext.AddAsync(product);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        Task<bool> IProductRepo.UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        async Task<bool> IProductRepo.DeleteProduct(Product product)
        {
            _dataContext.Remove(product);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
