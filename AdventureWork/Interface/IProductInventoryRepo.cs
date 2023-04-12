using AdventureWork.Models;

namespace AdventureWork.Interface
{
    public interface IProductInventoryRepo
    {
        public Task<ProductInventory> GetInventory(int productid, int locationid);
        public Task<ICollection<Product>> GetProdcutsInShelf(string shelf);
        public Task<Int16> GetAllProductsQuantities();
        public Task<bool> CheckShelf(string shelf);
        public Task<bool> InventoryExists(int productid, int locationid);
        public Task<bool> CreateInventory(ProductInventory inventory);
        public Task<bool> UpdateInventory(ProductInventory inventory);
        public Task<bool> CheckLocation(int locid);
    }
}
