using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Worker.Core.Service
{
    public class TaskListnerService: BackgroundService
    {
        private readonly ILogger<TaskListnerService> _logger;
        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;
        private TaskReceiver _taskReciver;
        public TaskListnerService(ILogger<TaskListnerService> logger, TaskReceiver taskReceiver)
        {
            _logger = logger;
            _taskReciver = taskReceiver;
            InitializeRabbitmq();
        }

        

        public void InitializeRabbitmq()
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            factory = new ConnectionFactory { HostName = "localhost", UserName = "admin", Password = "admin" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: "task_queue",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _taskReciver._channel = channel;
            channel.BasicConsume("task_queue", false, _taskReciver);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _logger.LogInformation("Dispose called");
            connection.Close();
        }
    }
}
