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



        [HttpPost]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> InsertInventory([FromBody] ProductInventory inventory)
        {
            var exists = await inventoryRepo.CheckLocation(inventory.LocationID);
            if (!exists)
                return NotFound("Location not found");
            exists = await productRepo.ProductExists(inventory.ProductID);
            if (!exists)
                return NotFound("Product Not Found");
             exists = await inventoryRepo.InventoryExists(inventory.ProductID, inventory.LocationID);
            if (!exists)
                return NotFound("Inventory Already in table");


            var added = await inventoryRepo.CreateInventory(inventory);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!added)
                return StatusCode(400, "Something went wrong");

            return Ok(added);
        }


        [HttpPut("Update Inventory")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> UpdateInventory([FromBody] ProductInventory inventory)
        {

            if (inventory == null)
                return BadRequest("Your data is null");
            var exists = await inventoryRepo.CheckLocation(inventory.LocationID);
            if (!exists)
                return NotFound("Location not found");
            exists = await productRepo.ProductExists(inventory.ProductID);
            if (!exists)
                return NotFound("Product Not Found");
            exists = await inventoryRepo.InventoryExists(inventory.ProductID, inventory.LocationID);

            if (!exists)
                return BadRequest("Inventory Not Found ");

            var updated = await inventoryRepo.UpdateInventory(inventory);

            if (!updated)
                return StatusCode(400, "Something went wrong");

            return Ok(updated);

        }

        [HttpGet("Get {shelf} Products")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetProductsInShelf(string shelf)
        {

            var exists = await inventoryRepo.CheckShelf(shelf);
            if (!exists)
                return NotFound("Shelf not found");

            var products = await inventoryRepo.GetProdcutsInShelf(shelf);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpGet("Get Products Quantities")]
        [ProducesResponseType(200, Type = typeof(short))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetQuantitiesOfProducts()
        {

            var qty = await inventoryRepo.GetAllProductsQuantities();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(qty);
        }
    }
}
