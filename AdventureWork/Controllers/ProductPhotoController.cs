using Microsoft.AspNetCore.Mvc;
using AdventureWork.Interface;
using AdventureWork.Models;

namespace AdventureWork.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductPhotoController : Controller
    {
        private readonly IProductPhotoRepo productPhotoRepo;
        private readonly IProductRepo productRepo;

        public ProductPhotoController(IProductPhotoRepo productPhotoRepo, IProductRepo productRepo)
        {
            this.productPhotoRepo = productPhotoRepo;
            this.productRepo = productRepo;
        }

        [HttpGet("Get {id} Thumbnail")]
        [ProducesResponseType(200, Type = typeof(Byte[]))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetThumbNail(int id)
        {
            var exists = await productRepo.ProductExists(id);
            if (!exists)
                return StatusCode(400, "Not Found");

            var photos = await productPhotoRepo.GetThumbNail(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(photos);
        }

        [HttpGet("Get {id} LargePhoto")]
        [ProducesResponseType(200, Type = typeof(Byte[]))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetLargePhoto(int id)
        {
            var exists = await productRepo.ProductExists(id);
            if (!exists)
                return StatusCode(400, "Not Found");

            var photos = await productPhotoRepo.GetLargePhoto(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(photos);
        }


        [HttpGet("Get All Photos")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Byte[]>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllPhotos()
        {

            var photos = await productPhotoRepo.GetAllPhotos();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(photos);
        }
    }
}
