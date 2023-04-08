using Microsoft.AspNetCore.Mvc;
using AdventureWork.Interface;
using AdventureWork.Models;

namespace AdventureWork.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryRepo productCategoryRepo;

        public ProductCategoryController(IProductCategoryRepo productCategoryRepo)
        {
                this.productCategoryRepo = productCategoryRepo;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductCategory>))]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await productCategoryRepo.GetCategories();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }
    }
}
