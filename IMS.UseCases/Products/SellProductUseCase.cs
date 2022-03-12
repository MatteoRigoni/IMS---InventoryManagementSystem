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
    public class SellProductUseCase : ISellProductUseCase
    {
        private readonly IProductTransactionRepository _productTransactionRepository;
        private readonly IProductRepository _productRepository;

        public SellProductUseCase(IProductTransactionRepository productTransactionRepository, IProductRepository productRepository)
        {
            _productTransactionRepository = productTransactionRepository;
            _productRepository = productRepository;
        }

        public async Task ExecuteAsync(string salesOrderNumber, Product product, int quantity, string doneBy)
        {
            await _productTransactionRepository.SellProduct(salesOrderNumber, product, quantity, product.Price, doneBy);
            product.Quantity -= quantity;
            await _productRepository.UpdateProduct(product);


        }
    }
}
