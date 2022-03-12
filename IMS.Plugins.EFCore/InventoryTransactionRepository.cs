using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
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
                    Cost = price * quantity
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
