using InventoryManagement.Core.IServices;
using InventoryManagement.Data.IRepository;
using RabbitMQ.Client;
using System.Text;

namespace InventoryManagement.Core.Services
{
    public class InventoryItemService : IInventoryItemService
    {

        private readonly IInventoryItemRepository inventoryItemRepository;

        public InventoryItemService(IInventoryItemRepository inventoryItemRepository)
        {
            this.inventoryItemRepository = inventoryItemRepository;
        }

        public async Task UpdateStockAsync(int itemId, int quantity)
        {
            var item = await inventoryItemRepository.GetByIdAsync(itemId);
            if (item != null)
            {
                item.Quantity += quantity;
                await inventoryItemRepository.SaveChangesAsync();
            }

            var factory = new ConnectionFactory()
            {
                HostName = "localhost"

            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "inventory_updates", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var message = $"{item.Id}:{item.Quantity}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: "inventory_updates", basicProperties: null, body: body);
            }
        }
    }
}

