using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Products
{
    public class ProduceProductUseCase : IProduceProductUseCase
    {
        private readonly IInventoryRepository _inventoryRepo;
        private readonly IProductRepository _productRepo;
        private readonly IInventoryTransactionRepository _inventoryTransactionRepo;
        private readonly IProductTransactionRepository _productTransactionRepo;

        public ProduceProductUseCase(
            IInventoryRepository inventoryRepo,
            IProductRepository productRepo,
            IInventoryTransactionRepository inventoryTransactionRepo,
            IProductTransactionRepository productTransactionRepo)
        {
            _inventoryRepo = inventoryRepo;
            _productRepo = productRepo;
            _inventoryTransactionRepo = inventoryTransactionRepo;
            _productTransactionRepo = productTransactionRepo;
        }

        public async Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
            await _productTransactionRepo.ProduceAsync(productionNumber, product, quantity, product.Price, doneBy);

            product.Quantity += quantity;
            await _productRepo.UpdateProduct(product);
        }
    }
}
