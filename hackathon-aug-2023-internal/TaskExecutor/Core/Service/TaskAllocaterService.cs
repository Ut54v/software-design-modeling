using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TaskAllocatorCommons.Models;

namespace TaskExecutor.Core.Service
{
    public class TaskAllocaterService
    {
        private readonly ILogger<TaskAllocaterService> _logger;
        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;

        public TaskAllocaterService()
        {
            InitializeRabbitmq();
        }

        void InitializeRabbitmq()
        {
            var factory = new ConnectionFactory { HostName = "localhost", UserName = "admin", Password = "admin" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "task_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        void AddTaskToQueue(TaskModel task)
        {
            var message = JsonSerializer.Serialize(task);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "task_queue",
                                 basicProperties: properties,
                                 body: body);
            Console.WriteLine($" [x] Sent {message}");
        }
    }
}
