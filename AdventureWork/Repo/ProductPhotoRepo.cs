using AdventureWork.Data;
using AdventureWork.Models;
using AdventureWork.Interface;
using Microsoft.EntityFrameworkCore;

namespace AdventureWork.Repo
{
    public class ProductPhotoRepo : IProductPhotoRepo
    {

        private readonly DataContext _dataContext;

        public ProductPhotoRepo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<byte[]> GetLargePhoto(int productId)
        {
            var productphotomany = await _dataContext.ProductProductPhoto.Where(pc => pc.ProductID == productId).Select(p => p.ProductPhotoID).ToListAsync();

            var photos = await _dataContext.ProductPhoto.Where(pc => productphotomany.Contains(pc.ProductPhotoID)).Select(p => p.LargePhoto).FirstOrDefaultAsync();

            return photos;
        }

        public async Task<byte[]> GetThumbNail(int productId)
        {
            var productphotomany = await _dataContext.ProductProductPhoto.Where(pc => pc.ProductID == productId).Select(p => p.ProductPhotoID).ToListAsync();

            var photos = await _dataContext.ProductPhoto.Where(pc => productphotomany.Contains(pc.ProductPhotoID)).Select(p => p.ThumbNailPhoto).FirstOrDefaultAsync();

            return photos;
        }

        public async Task<ICollection<ProductPhoto>> GetAllPhotos(int productId)
        {
            var productphotomany = await _dataContext.ProductProductPhoto.Where(pc => pc.ProductID == productId).Select(p => p.ProductPhotoID).ToListAsync();

            var photos = await _dataContext.ProductPhoto.Where(pc => productphotomany.Contains(pc.ProductPhotoID)).ToListAsync();

            return photos;
        }
    }
}
