﻿using AdventureWork.Data;
using AdventureWork.Models;
using AdventureWork.Interface;
using Microsoft.EntityFrameworkCore;

namespace AdventureWork.Repo
{
    public class ProductInventoryRepo : IProductInventoryRepo
    {


        private readonly DataContext _dataContext;

        public ProductInventoryRepo(DataContext context)
        {
            _dataContext = context;
        }


        public async Task<bool> CreateInventory(ProductInventory inventory)
        {
            _dataContext.AddAsync(inventory);
            return await _dataContext.SaveChangesAsync() > 0; 
        }

        public async Task<ProductInventory> GetInventory(int productid, int locationid)
        {
            return await _dataContext.ProductInventories.Where(p => p.ProductID == productid && p.LocationID == locationid).FirstOrDefaultAsync(); 
        }

        public async Task<bool> CheckLocation(int locid)
        {
            return await _dataContext.Location.AnyAsync(p=> p.LocationID == locid);
        }

        public async Task<bool> InventoryExists(int productid, int locationid)
        {
            var inventory = await _dataContext.ProductInventories.Where(p => p.ProductID == productid && p.LocationID == locationid).FirstOrDefaultAsync();
            return inventory != null;
        }

        public async Task<bool> UpdateInventory(ProductInventory inventory)
        {
            var exists = await this.GetInventory(inventory.ProductID, inventory.LocationID);

            if (exists != null)
            {
                _dataContext.Entry(exists).State = EntityState.Detached;
            }
            _dataContext.Update(inventory);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<Product>> GetProdcutsInShelf(string shelf)
        {
            var idsinshelf = _dataContext.ProductInventories.Where(p => p.Shelf.Equals(shelf)).Select(pc => pc.ProductID).ToList();
            var products = _dataContext.Product.Where(p => idsinshelf.Contains(p.ProductID)).ToList();

            return products;
        }

        public async Task<bool> CheckShelf(string shelf)
        {
            var shelfvar =  await _dataContext.ProductInventories.Where(p => p.Shelf == shelf).FirstOrDefaultAsync();
            return shelfvar != null;
        }

        public async Task<Int16> GetAllProductsQuantities()
        {
            var quantity = _dataContext.ProductInventories.Sum(p => p.Quantity);

            return (short)quantity;
        }
    }
}
