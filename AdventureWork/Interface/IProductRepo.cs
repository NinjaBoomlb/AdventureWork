using AdventureWork.Models;

namespace AdventureWork.Interface
{
    public interface IProductRepo
    {
        public Task<ICollection<Product>> GetProducts();
        public Task<Product> GetProduct(int id);
        public Task<bool> ProductExists(int id);
        public Task<bool> CreateProduct(Product product);
        public Task<bool> UpdateProduct(Product product);
        public Task<bool> DeleteProduct(Product product);
    }
}
