using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Econolite.Ode.Status.CorridorSpeedEvent.Messaging;

public class CorridorSegmentSpeedEventConsumer : ICorridorSegmentSpeedEventConsumer
{
    private readonly IConsumer<Guid, CorridorSegmentSpeedEvent> _consumer;
    private readonly string[] _wantedtypes;

    public CorridorSegmentSpeedEventConsumer(IConfiguration configuration, IConsumer<Guid, CorridorSegmentSpeedEvent> consumer, IOptions<CorridorSegmentSpeedEventOptions> options)
    {
        _consumer = consumer;
        _wantedtypes = new string[]
        {
            typeof(CorridorSegmentSpeedEvent).Name,
        };
        _consumer.Subscribe(configuration[options.Value.ConfigTopic]);
    }

    public void Complete(ConsumeResult consumeResult) => _consumer.Complete(consumeResult);

    public (ConsumeResult ConsumeResult, CorridorSegmentSpeedEvent CorridorSegmentSpeedEvent) Consume(CancellationToken cancellationToken)
    {
        var consumeresult = _consumer.Consume(_ => _wantedtypes.Contains(_), cancellationToken);
        return (consumeresult, consumeresult.ToObject<CorridorSegmentSpeedEvent>());
    }
}