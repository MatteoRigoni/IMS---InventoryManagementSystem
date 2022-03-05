using IMS.CoreBusiness;

namespace IMS.UseCases.Interfaces
{
    public interface IGetInventoryByIdUseCase
    {
        Task<Inventory> ExecuteAsync(int inventoryId);
    }
}