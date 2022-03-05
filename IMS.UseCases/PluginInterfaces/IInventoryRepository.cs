using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetInventoriesByName(string name);
        Task AddInventoryAsync(Inventory inventory);
        Task UpdateInventory(Inventory inventory);
        Task<Inventory> GetInventoryByIdAsync(int inventoryId);
    }
}
