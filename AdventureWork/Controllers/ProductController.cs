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


        [HttpPost("Insert Product")]
        [ProducesResponseType(200 , Type = typeof(bool))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> CreateProduct([FromQuery] Product product)
        {

            if (product == null)
                return BadRequest("Your data is null");

            product.ProductID = 0;

            if (product.Name == null || product.ProductNumber == null || product.SellStartDate == null || product.rowguid == null)
                return BadRequest("Some fields are required (name, product number, sell start date, rowguid)");
            var exists = productRepo.NameExists(product.Name);
            if (exists)
                return BadRequest("Name already exists");
            exists = productRepo.NumberExists(product.ProductNumber);
            if (exists)
                return BadRequest("Number already exists");
            exists = productRepo.RowguidExists(product.rowguid);
            if (exists)
                return BadRequest("Guid already exists");
            if (product.SafetyStockLevel <= 0)
                return BadRequest("Safety stock should be greater than 0");

            if (product.ReorderPoint <= 0)
                return BadRequest("Reorder point should be greater than 0");

            if (product.StandardCost <= 0)
                return BadRequest("Standard cost should be greater than 0");

            if (product.ListPrice <= 0)
                return BadRequest("List price should be greater than 0");

            if (product.Weight != null && product.Weight <= 0)
                return BadRequest("Weight should be greater than 0");

            if (product.DaysToManufacture <= 0)
                return BadRequest("Days to manufacture should be greater than 0");

            if (product.ProductLine != null &&
                !(product.ProductLine.ToUpper() == "R" ||
                  product.ProductLine.ToUpper() == "M" ||
                  product.ProductLine.ToUpper() == "T" ||
                  product.ProductLine.ToUpper() == "S"))
                return BadRequest("Invalid product line");

            if (product.Class != null &&
                !(product.Class == "h" || product.Class == "m" || product.Class == "l" ||
                  product.Class == "H" || product.Class == "M" || product.Class == "L"))
                return BadRequest("Invalid class");

            if (product.SellStartDate >= product.SellEndDate && product.SellEndDate != null)
                return BadRequest("Sell end date should be greater than sell start date");
            product.ModifiedDate = DateTime.Now;
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
            if (!deleted)
                return BadRequest("Something went wrong or product id is used as a foreign key in another table");
            return Ok(deleted);
        }


        [HttpPut("Update Product")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> UpdateProduct([FromQuery] Product product)
        {

            if (product == null)
                return BadRequest("Your data is null");

            if (product.ProductID == null)
                return BadRequest("Product Id should not be null");

            var exists = await productRepo.ProductExists(product.ProductID);

            if (!exists)
                return BadRequest("Not found ");

            if (product.Name == null || product.ProductNumber == null || product.SellStartDate == null || product.rowguid == null)
                return BadRequest("Some fields are required (name, product number, sell start date, rowguid)");
            exists = productRepo.NameExists(product.Name);
            if (exists)
                return BadRequest("Name already exists");
            exists = productRepo.NumberExists(product.ProductNumber);
            if (exists)
                return BadRequest("Number already exists");
            exists = productRepo.RowguidExists(product.rowguid);
            if (exists)
                return BadRequest("Guid already exists");
            if (product.SafetyStockLevel <= 0)
                return BadRequest("Safety stock should be greater than 0");

            if (product.ReorderPoint <= 0)
                return BadRequest("Reorder point should be greater than 0");

            if (product.StandardCost <= 0)
                return BadRequest("Standard cost should be greater than 0");

            if (product.ListPrice <= 0)
                return BadRequest("List price should be greater than 0");

            if (product.Weight != null && product.Weight <= 0)
                return BadRequest("Weight should be greater than 0");

            if (product.DaysToManufacture <= 0)
                return BadRequest("Days to manufacture should be greater than 0");

            if (product.ProductLine != null &&
                !(product.ProductLine.ToUpper() == "R" ||
                  product.ProductLine.ToUpper() == "M" ||
                  product.ProductLine.ToUpper() == "T" ||
                  product.ProductLine.ToUpper() == "S"))
                return BadRequest("Invalid product line");

            if (product.Class != null &&
                !(product.Class == "h" || product.Class == "m" || product.Class == "l" ||
                  product.Class == "H" || product.Class == "M" || product.Class == "L"))
                return BadRequest("Invalid class");

            if (product.SellStartDate >= product.SellEndDate && product.SellEndDate != null)
                return BadRequest("Sell end date should be greater than sell start date");
            product.ModifiedDate = DateTime.Now;

            var updated = await productRepo.UpdateProduct(product);

            if (!updated)
                return StatusCode(400, "Something went wrong");

            return Ok(updated);

        }

    }
}
