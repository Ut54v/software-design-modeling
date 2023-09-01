using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TaskAllocatorCommons.Models;
using Worker.Core.Enums;
using Worker.Core.Models;

namespace Worker.Core.Service
{
    public class TaskReceiver: DefaultBasicConsumer
    {
        public IModel _channel { set; private get; }
        private readonly TaskHandler _taskHandler;
        private readonly ILogger<TaskReceiver> _logger;
        public TaskReceiver(TaskHandler taskHandler, ILogger<TaskReceiver> logger)
        {
            _taskHandler = taskHandler;
            _logger = logger;
        }
        public override async void HandleBasicDeliver(
            string consumerTag,
            ulong deliveryTag,
            bool redelivered,
            string exchange,
            string routingKey,
            IBasicProperties properties,
            ReadOnlyMemory<byte> body)
        {
            _logger.LogInformation($"Consuming Task");
            _logger.LogInformation(string.Concat("Message received from the exchange ", exchange));
            string message = Encoding.UTF8.GetString(body.Span);
            _logger.LogInformation(string.Concat("Task: ", message));
            TaskModel task = JsonSerializer.Deserialize<TaskModel>(message);
            ResponseStatus responseStatus = ResponseStatus.Success;
            if(task != null)
                responseStatus = await _taskHandler.HandelTask(task);

            if (responseStatus.Equals(ResponseStatus.Failed))
            {
                _channel.BasicNack(deliveryTag, false, false);
                return;
            }
            if (responseStatus.Equals(ResponseStatus.Faulted))
            {
                _channel.BasicNack(deliveryTag, false, true);
                return;
            }
            _channel.BasicAck(deliveryTag, false);
        }
    }
}
