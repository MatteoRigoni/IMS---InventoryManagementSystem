using IMS.CoreBusiness;
using IMS.UseCases.Interfaces;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class AddInventoryUseCase : IAddInventoryUseCase
    {
        private readonly IInventoryRepository _inventoryRepo;

        public AddInventoryUseCase(IInventoryRepository inventoryRepo)
        {
            _inventoryRepo = inventoryRepo;
        }

        public async Task ExecuteAsync(Inventory inventory)
        {
            await _inventoryRepo.AddInventoryAsync(inventory);
        }
    }
}
