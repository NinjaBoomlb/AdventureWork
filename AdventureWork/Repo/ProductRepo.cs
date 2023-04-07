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
    }
}
