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
    public class ValidateEnoughInventoriesFOrProductingUseCase : IValidateEnoughInventoriesFOrProductingUseCase
    {
        private readonly IProductRepository _productRepository;
        public ValidateEnoughInventoriesFOrProductingUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> ExecuteAsync(Product product, int quantity)
        {
            var prod = await _productRepository.GetProductById(product.Id);
            foreach (var pi in prod.ProductInventories)
            {
                if (pi.InventoryQuantity * quantity > pi.Inventory.Quantity)
                    return false;
            }
            return true;
        }
    }
}
