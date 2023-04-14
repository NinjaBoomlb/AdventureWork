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

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> InsertCategory([FromQuery] ProductCategory category)
        {

            if (category == null)
                return BadRequest("Your data is null");

            var exists = await productCategoryRepo.CategoryExists(category.ProductCategoryID);

            if (exists)
                return BadRequest("Already in the table");
            if (category.ModifiedDate != null)
                return BadRequest("The modified date should be null (will be added automatically)");

            category.ModifiedDate = DateTime.Now;
            var added = await productCategoryRepo.CreateCategory(category);

            if (!added)
                return StatusCode(400, "Something went wrong");

            return Ok(added);
        }

        [HttpPut("Update Category")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> UpdateCategory([FromQuery] ProductCategory category)
        {

            if (category == null)
                return BadRequest("Your data is null");


            var exists = await productCategoryRepo.CategoryExists(category.ProductCategoryID);

            if (!exists)
                return BadRequest("Not found ");
            if (category.ModifiedDate != null)
                return BadRequest("The modified date should be null (will be added automatically)");

            category.ModifiedDate = DateTime.Now;
            var updated = await productCategoryRepo.UpdateCategory(category);

            if (!updated)
                return StatusCode(400, "Something went wrong");

            return Ok(updated);

        }


        [HttpDelete("Delete Category {id}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var exists = await productCategoryRepo.CategoryExists(id);

            if (!exists)
                return BadRequest("Not found");

            var category = await productCategoryRepo.GetCategory(id);
            var deleted = await productCategoryRepo.DeleteCategory(category);
            if(!deleted)
                return BadRequest("Something went wrong or category id is used as a foreign key in another table");
            return Ok(deleted);
        }



        [HttpGet("Get Specific products {id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public async Task<IActionResult> GetCategories(int id)
        {
            var categories = await productCategoryRepo.GetCategories(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }



    }
}
