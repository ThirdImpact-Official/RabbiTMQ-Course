// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, World!");


    var factory = new ConnectionFactory()
    {
        HostName = "localhost",
    };

    await using (var connection = await factory.CreateConnectionAsync())
    await using (var channel = await connection.CreateChannelAsync())
    {
        await channel.QueueDeclareAsync("hello_queue", false, false, false, null);
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
        };

        await channel.BasicConsumeAsync("hello_queue", true, consumer);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
