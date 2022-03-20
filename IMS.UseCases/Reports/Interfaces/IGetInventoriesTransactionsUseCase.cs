﻿using IMS.CoreBusiness;

namespace IMS.UseCases.Reports.Interfaces
{
    public interface IGetInventoriesTransactionsUseCase
    {
        Task<IEnumerable<InventoryTransaction>> ExecuteAsync(string inventoryName, DateTime dateFrom, DateTime dateTo, InventoryTransactionType transactionType);
    }
}