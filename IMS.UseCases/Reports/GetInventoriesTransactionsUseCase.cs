using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Reports
{
    public class GetInventoriesTransactionsUseCase : IGetInventoriesTransactionsUseCase
    {
        private readonly IInventoryTransactionRepository _inventoryTransactionRepository;

        public GetInventoriesTransactionsUseCase(IInventoryTransactionRepository inventoryTransactionRepository)
        {
            _inventoryTransactionRepository = inventoryTransactionRepository;
        }

        public async Task<IEnumerable<InventoryTransaction>> ExecuteAsync(
            string inventoryName,
            DateTime dateFrom,
            DateTime dateTo,
            InventoryTransactionType transactionType
            )
        {
            return await _inventoryTransactionRepository.GetInventoryTransactions(inventoryName, dateFrom, dateTo, transactionType);
        }
    }
}
