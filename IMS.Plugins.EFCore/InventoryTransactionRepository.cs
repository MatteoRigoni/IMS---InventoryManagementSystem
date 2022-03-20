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
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly IMSContext _context;

        public InventoryTransactionRepository(IMSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryTransaction>> GetInventoryTransactions(
            string inventoryName,
            DateTime? dateFrom,
            DateTime? dateTo,
            InventoryTransactionType? transactionType)
        {
            var query = from it in _context.InventoryTransactions
                        join inv in _context.Inventories on it.InventoryId equals inv.Id
                        where (String.IsNullOrEmpty(inv.Name) || inv.Name.Contains(inventoryName, StringComparison.OrdinalIgnoreCase)) &&
                              (!dateFrom.HasValue || it.TransactionDate >= dateFrom) &&
                              (!dateTo.HasValue || it.TransactionDate >= dateTo) &&
                              (!transactionType.HasValue || it.TransactionType == transactionType)
                        select it;

            return await query.Include(x => x.Inventory).ToListAsync();
        }

        public async Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, double price, string doneBy)
        {
            try
            {
                _context.InventoryTransactions.Add(new CoreBusiness.InventoryTransaction()
                {
                    InventoryId = inventory.Id,
                    PoNumber = poNumber,
                    QuantityBefore = inventory.Quantity,
                    QuantityAfter = inventory.Quantity + quantity,
                    TransactionType = InventoryTransactionType.PurchaseInventory,
                    TransactionDate = DateTime.Now,
                    DoneBy = doneBy,
                    UnitPrice = price
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
