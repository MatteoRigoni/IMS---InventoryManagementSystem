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
    public class GetInventoryByIdUseCase : IGetInventoryByIdUseCase
    {
        private readonly IInventoryRepository _inventoryRepo;

        public GetInventoryByIdUseCase(IInventoryRepository inventoryRepo)
        {
            _inventoryRepo = inventoryRepo;
        }

        public async Task<Inventory> ExecuteAsync(int inventoryId)
        {
            return await _inventoryRepo.GetInventoryByIdAsync(inventoryId);
        }
    }
}
