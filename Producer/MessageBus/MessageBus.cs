using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Producer.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly ISendEndpointProvider _sendpointProvider;
        private readonly IConfiguration _configuration;

        public MessageBus(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
        {
            _sendpointProvider = sendEndpointProvider;
            _configuration = configuration;
        }

        public async Task Publish(object entity)
        {
            var queueName = _configuration["RabbitMQ:QueueName"];
            var endpoint = await _sendpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));

            await endpoint.Send(entity);
        }
    }
}