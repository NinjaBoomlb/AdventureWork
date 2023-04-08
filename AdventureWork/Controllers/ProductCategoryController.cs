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
        private readonly IProductRepo productRepo;

        public ProductCategoryController(IProductCategoryRepo productCategoryRepo, IProductRepo productRepo)
        {
                this.productCategoryRepo = productCategoryRepo;
                this.productRepo = productRepo;
        }


        [HttpGet("Get Specific products {idCategory}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductCategory>))]
        public async Task<IActionResult> GetCategories(int idCategory)
        {
            var categories = await productCategoryRepo.GetCategories(idCategory);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }
    }
}
