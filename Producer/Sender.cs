using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

namespace Producer
{
    public class Sender
    {
        private const string queueName = "hello_queue";
        private const string HostName = "localhost";

        public static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = "localhost" 
            };

            await using (var connection = await factory.CreateConnectionAsync())
            await using (var channel = await connection.CreateChannelAsync())
            {
                // Déclarer la queue
                    await channel.QueueDeclareAsync(
                    queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                    string Message = "Hello World RabbitMq Async";
            
                    var body = Encoding.UTF8.GetBytes(Message);

                    await channel.BasicPublishAsync
                    (
                        exchange: "",
                        routingKey: queueName,
                        body: body
                    );
                    Console.WriteLine($" [x] Sent {Message}");
            };
        }

    }
}
