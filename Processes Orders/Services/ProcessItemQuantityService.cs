using OrderProcessing.Core.IServices;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrderProcessing.Core.Services
{
    public class ProcessItemQuantityService : IProcessItemQuantityService
    {
        private SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        public async Task ProcessItemQuantityAsync(int itemId, int quantity)
        {

            var factory = new ConnectionFactory()
            {
                HostName = "localhost"

            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "inventory_updates", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                   
                    // Process order and deduct items from inventory
                    await ProcessOrder(itemId, quantity, message);
                };
                channel.BasicConsume(queue: "inventory_updates", autoAck: true, consumer: consumer);
            }
        }

        private async Task ProcessOrder(int itemIdInput, int quantityInput, string message)
        {
            try
            {
                await _lock.WaitAsync();
                try
                {

                    var itemId = int.Parse(message.Split(',')[0].Split(':')[1]);
                    var quantity = int.Parse(message.Split(',')[1].Split(':')[1]);

                   
                    if (itemId == itemIdInput && quantity >= quantityInput)
                    {
                        quantity -= quantity;
                    }
                    else
                    {
                        throw new NullReferenceException($"Item with ID {itemId} not found.");
                    }
                }
                finally
                {
                    _lock.Release();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while processing the order: {ex.Message}");
            }
        }
    }
}
