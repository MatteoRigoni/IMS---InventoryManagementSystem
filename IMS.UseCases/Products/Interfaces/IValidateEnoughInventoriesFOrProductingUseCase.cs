using IMS.CoreBusiness;

namespace IMS.UseCases.Products.Interfaces
{
    public interface IValidateEnoughInventoriesFOrProductingUseCase
    {
        Task<bool> ExecuteAsync(Product product, int quantity);
    }
}