using AdventureWork.Data;
using AdventureWork.Models;
using AdventureWork.Interface;

namespace AdventureWork.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly DataContext _dataContext;

        public ProductRepo(DataContext context)
        {
            _dataContext = context;
        }

        public ICollection<Product> GetProducts()
        {
            return _dataContext.Product.OrderBy(p => p.ProductID).ToList();
        }
    }
}
