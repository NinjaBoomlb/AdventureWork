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
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProduct()
        {
            var products = productRepo.GetProducts();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }
    }
}
