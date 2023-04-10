using Microsoft.AspNetCore.Mvc;
using AdventureWork.Interface;
using AdventureWork.Models;

namespace AdventureWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepo productRepo;

        public ProductController(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public async Task<IActionResult> GetProducts()
        {
            var products = await productRepo.GetProducts();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

/*        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProdcut(int id)
        {
            var exists = await productRepo.ProductExists(id);
            
            
            if(!exists)
                return StatusCode(400, "Not Found");

            if (!ModelState.IsValid)
                return StatusCode(400, "No product");
            var product = await productRepo.GetProduct(id);
            return Ok(product);

        }*/

        [HttpPost("Insert Product")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> CreateProduct([FromBody] Product product) { 
        
            var exists = await productRepo.ProductExists(product.ProductID);

            if (exists)
                return BadRequest("Already in the table");

            var added = await productRepo.CreateProduct(product);

            if (!added)
                return StatusCode(400, "Something went wrong");

            return Ok(added);

        }
        [HttpDelete("Delete Product {id}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteProduct( int id)
        {
            var exists = await productRepo.ProductExists(id);

            if (!exists)
                return BadRequest("Not found");

            var product = await productRepo.GetProduct(id);
            var deleted = await productRepo.DeleteProduct(product);

            return Ok(deleted);
        }


        [HttpPut("Update Product {id}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {

            if (product == null)
                return BadRequest("Your data is null");

            if (product.ProductID != id)
                return BadRequest("Not Compatible");

            var exists = await productRepo.ProductExists(product.ProductID);

            if (!exists)
                return BadRequest("Not found ");

            var updated = await productRepo.UpdateProduct(product);

            if (!updated)
                return StatusCode(400, "Something went wrong");

            return Ok(updated);

        }

    }
}
