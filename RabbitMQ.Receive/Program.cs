using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"[x] Received {message}");
        };

        channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);
        Console.WriteLine("Receive: Presiona cualquier tecla para continuar...");
        Console.ReadLine();
    }
}

Console.WriteLine("¡Perfect! press any key to continue (*.*)// ");
Console.ReadLine();