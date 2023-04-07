using AdventureWork.Models;

namespace AdventureWork.Interface
{
    public interface IProductRepo
    {
        public Task<ICollection<Product>> GetProducts();
    }
}
