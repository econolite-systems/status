using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Microsoft.Extensions.Options;

namespace Econolite.Ode.Status.CorridorSpeedEvent.Messaging;

public class CorridorSegmentSpeedEventProducer : ICorridorSegmentSpeedEventProducer
{
    private readonly IProducer<Guid, CorridorSegmentSpeedEvent> _producer;
    private readonly IMessageFactory<Guid, CorridorSegmentSpeedEvent> _messageFactory;
    private readonly string _topic;

    public CorridorSegmentSpeedEventProducer(IProducer<Guid, CorridorSegmentSpeedEvent> producer, IMessageFactory<Guid, CorridorSegmentSpeedEvent> messageFactory, IOptions<CorridorSegmentSpeedEventOptions> options)
    {
        _producer = producer;
        _messageFactory = messageFactory;
        _topic = options.Value.ConfigTopic;
    }

    public async Task ProduceAsync(Guid tenantId, CorridorSegmentSpeedEvent speedEventStatus, CancellationToken cancellationToken)
    {
        await _producer.ProduceAsync(_topic, _messageFactory.Build(tenantId, speedEventStatus));
    }
}