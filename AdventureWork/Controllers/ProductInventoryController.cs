using Microsoft.AspNetCore.Mvc;
using AdventureWork.Interface;
using AdventureWork.Models;



namespace AdventureWork.Controllers
{
    public class ProductInventoryController : Controller
    {

        private readonly IProductInventoryRepo inventoryRepo;
        private readonly IProductRepo productRepo;

        public ProductInventoryController(IProductInventoryRepo inventoryRepo, IProductRepo productRepo)
        {
            this.productRepo = productRepo;
            this.inventoryRepo = inventoryRepo;
        }


        [HttpGet("Get {locid} {productid} Inventory")]
        [ProducesResponseType(200, Type = typeof(ProductInventory))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetInventory(int locid, int productid)
        {

            var exists = await inventoryRepo.CheckLocation(locid);
            if(!exists)
                return NotFound("Location not found");
            exists = await productRepo.ProductExists(productid);
            if (!exists)
                return NotFound("Product Not Found");

            exists = await inventoryRepo.InventoryExists(productid, locid);
            if (!exists)
                return NotFound("Inventory Not Found");

            var inventory = await inventoryRepo.GetInventory(productid, locid);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(inventory);
        }
    }
}
