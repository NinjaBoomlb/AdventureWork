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

        public async Task<bool> UpdateProduct(Product product)
        {

            var existingProduct = await this.GetProduct(product.ProductID);

            if (existingProduct != null)
            {
                _dataContext.Entry(existingProduct).State = EntityState.Detached;
            }
            _dataContext.Update(product);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        async Task<bool> IProductRepo.DeleteProduct(Product product)
        {
            var productExists = _dataContext.ProductInventories.Select(p => p.ProductID).ToList();

            if (productExists.Contains(product.ProductID))
                return false;
            _dataContext.Remove(product);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public bool NameExists(string name)
        {
            var names =  _dataContext.Product.Select(p => p.Name).ToList();

            return names.Contains(name);
        }

        public bool RowguidExists(Guid guid)
        {
            var guids =  _dataContext.Product.Select(p => p.rowguid).ToList();

            return guids.Contains(guid);
        }

        public bool NumberExists(string number)
        {
            var numbers =  _dataContext.Product.Select(p => p.ProductNumber).ToList();

            return numbers.Contains(number);
        }

    }
}
