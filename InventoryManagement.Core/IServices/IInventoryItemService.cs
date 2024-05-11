namespace InventoryManagement.Core.IServices
{
    public interface IInventoryItemService
    {
        Task UpdateStockAsync(int itemId, int quantity);
    }
}
