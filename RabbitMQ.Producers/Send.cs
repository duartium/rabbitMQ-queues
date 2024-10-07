using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
};

using (var connection = factory.CreateConnection())
{
    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare(queue: "orders",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        string messageToSend = $"Order added with price $874.82";
        var body = Encoding.UTF8.GetBytes(messageToSend);

        channel.BasicPublish(exchange: "",
            routingKey: "orders",
            basicProperties: null,
            body: body);
        Console.WriteLine($"[x] sent {messageToSend}");
    }
}

Console.WriteLine("¡Perfect! press any key to continue (*.*)// ");
Console.ReadLine();