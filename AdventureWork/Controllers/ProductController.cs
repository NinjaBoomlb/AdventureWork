using Microsoft.AspNetCore.Mvc;
using AdventureWork.Interface;
using AdventureWork.Models;
using AdventureWork.Data;

namespace AdventureWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepo productRepo;
        private readonly DataContext context;

        public ProductController(IProductRepo productRepo, DataContext context)
        {
            this.productRepo = productRepo;
            this.context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public async Task<IActionResult> GetProducts()
        {
            var products = await productRepo.GetProducts();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }
    }
}
