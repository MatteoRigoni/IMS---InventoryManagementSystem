using IMS.CoreBusiness;

namespace IMS.UseCases.Products.Interfaces
{
    public interface IEdiProductUseCase
    {
        Task ExecuteAsync(Product product);
    }
}