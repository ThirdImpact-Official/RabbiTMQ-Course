// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, World!");


var factory = new ConnectionFactory()
{
    HostName = "localhost"
};

await using (var connection = await factory.CreateConnectionAsync())
await using (var channel = await connection.CreateChannelAsync())
{
    // Déclarer la queue
    await channel.QueueDeclareAsync(
    queue: "hello_queue",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

    string Message = "Hello World RabbitMq Async";

    var body = Encoding.UTF8.GetBytes(Message);

    await channel.BasicPublishAsync
    (
        exchange: "",
        routingKey: "hello_queue",
        body: body
    );
    Console.WriteLine($" [x] Sent {Message}");
    Console.ReadLine();
};