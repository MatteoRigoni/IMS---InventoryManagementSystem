using IMS.CoreBusiness;
using IMS.UseCases;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMSContext _context;

        public InventoryRepository(IMSContext context)
        {
            _context = context;
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            if (_context.Inventories.Any(i => i.Name.Equals(inventory.Name, StringComparison.OrdinalIgnoreCase))) return;

            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByName(string name)
        {
            return await _context.Inventories.Where(i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase)
                || string.IsNullOrWhiteSpace(name)).ToListAsync();
        }

        public async Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            return await _context.Inventories.FindAsync(inventoryId);
        }

        public async Task UpdateInventory(Inventory inventory)
        {
            if (_context.Inventories.Any(i => i.Id != inventory.Id &&
                i.Name.Equals(inventory.Name, StringComparison.OrdinalIgnoreCase))) return;

            var inventoryDb = await _context.Inventories.FindAsync(inventory.Id);
            if (inventoryDb != null)
            {
                inventoryDb.Name = inventory.Name;
                inventoryDb.Price = inventory.Price;
                inventoryDb.Quantity = inventory.Quantity;

                await _context.SaveChangesAsync();
            }
        }
    }
}
