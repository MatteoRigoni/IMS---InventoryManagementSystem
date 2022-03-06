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
    public class AddProductUseCase : IAddProductUseCase
    {
        private readonly IProductRepository _productRepo;

        public AddProductUseCase(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task ExecuteAsync(Product product)
        {
            if (product == null) return;

            await _productRepo.AddProduct(product);
        }
    }
}
