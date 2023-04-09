using AdventureWork.Models;

namespace AdventureWork.Interface
{
    public interface IProductPhotoRepo
    {
        public Task<byte[]> GetThumbNail(int productId);
        public Task<byte[]> GetLargePhoto(int productId);
        public Task<ICollection<ProductPhoto>> GetAllPhotos();
    }
}
