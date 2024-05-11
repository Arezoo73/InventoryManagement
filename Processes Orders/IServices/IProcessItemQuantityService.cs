namespace OrderProcessing.Core.IServices
{
    public interface IProcessItemQuantityService
    {
        Task ProcessItemQuantityAsync(int itemId, int quantity);
    }
}
