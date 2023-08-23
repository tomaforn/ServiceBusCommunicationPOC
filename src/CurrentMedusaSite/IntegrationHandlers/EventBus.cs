using IntegrationContracts;
using MassTransit;
using System.Threading;
using System.Threading.Tasks;

namespace CurrentMedusaSite.IntegrationHandlers
{
    public sealed class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
            where T : class =>
            _publishEndpoint.Publish(message, cancellationToken);
    }
}
