using IMS.CoreBusiness;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Inventories
{
    public class PurchaseInventoryUseCase : IPurchaseInventoryUseCase
    {
        private readonly IInventoryTransactionRepository _inventoryTransRepo;
        private readonly IInventoryRepository _inventoryRepo;

        public PurchaseInventoryUseCase(
            IInventoryTransactionRepository inventoryTransRepo, 
            IInventoryRepository inventoryRepo)
        {
            _inventoryTransRepo = inventoryTransRepo;
            _inventoryRepo = inventoryRepo;
        }

        public async Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string doneBy)
        {
            await _inventoryTransRepo.PurchaseAsync(poNumber, inventory, quantity, inventory.Price, doneBy);

            inventory.Quantity += quantity;
            await _inventoryRepo.UpdateInventory(inventory);
        }
    }
}
