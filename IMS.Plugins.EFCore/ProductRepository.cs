using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EFCore
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMSContext _context;

        public ProductRepository(IMSContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Product product)
        {
            if (_context.Products.Any(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))) return;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductByIdAsync(int id)
        {
            var productDb = await _context.Products.FindAsync(id);
            if (productDb != null)
            {
                productDb.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products
                .Include(p => p.ProductInventories)
                .ThenInclude(p => p.Inventory)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsByName(string name)
        {
            return await _context.Products
                .Where(i => (i.Name.Contains(name, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(name))
                 && i.IsActive).ToListAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            if (_context.Products.Any(i => i.Id != product.Id &&
                i.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))) return;

            var productDb = await _context.Products.FindAsync(product.Id);
            if (productDb != null)
            {
                productDb.Name = product.Name;
                productDb.Price = product.Price;
                //productDb.Quantity = product.Quantity;
                productDb.ProductInventories = product.ProductInventories;  

                await _context.SaveChangesAsync();
            }
        }
    }
}
