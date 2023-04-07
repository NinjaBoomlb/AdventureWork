using AdventureWork.Models;

namespace AdventureWork.Interface
{
    public interface IProductRepo
    {
        ICollection<Product> GetProducts();
    }
}
