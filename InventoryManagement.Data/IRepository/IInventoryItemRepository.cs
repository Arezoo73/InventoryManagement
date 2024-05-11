using InventoryManagement.Model.Entities;

namespace InventoryManagement.Data.IRepository
{
    public interface IInventoryItemRepository
    {
        Task<InventoryItem> GetByIdAsync(int itemId);
        Task SaveChangesAsync();
       }
}
