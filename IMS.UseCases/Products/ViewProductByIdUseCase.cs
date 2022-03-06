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
    public class ViewProductByIdUseCase : IViewProductByIdUseCase
    {
        private readonly IProductRepository _productRepo;

        public ViewProductByIdUseCase(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<Product> ExecuteAsync(int id)
        {
            return await _productRepo.GetProductById(id);
        }
    }
}
