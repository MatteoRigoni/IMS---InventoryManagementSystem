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
    public class ProductTransactionRepository : IProductTransactionRepository
    {
        private readonly IMSContext _context;

        public ProductTransactionRepository(IMSContext context)
        {
            _context = context;
        }


        public async Task ProduceAsync(string productionNumber, Product product, int quantity, double price, string doneBy)
        {
            var prod = await _context.Products
                .Include(x => x.ProductInventories)
                .ThenInclude(x => x.Inventory)
                .FirstOrDefaultAsync(x => x.Id == product.Id);

            // Descrease inventories' resources
            if (prod != null)
            {
                foreach (var pi in prod.ProductInventories)
                {
                    pi.Inventory.Quantity -= quantity * pi.InventoryQuantity;
                }
            }

            _context.ProductTransactions.Add(new ProductTransaction()
            {
                ProductionNumber = productionNumber,
                ProductId = product.Id,
                QuantityBefore = product.Quantity,
                QuantityAfter = product.Quantity + quantity,
                TransactionType = ProductTransactionType.Produce,
                DoneBy = doneBy,
                TransactionDate = DateTime.Now,
                UnitPrice = price
            });

            await _context.SaveChangesAsync();
        }
    }
}
