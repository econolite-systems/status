using Econolite.Ode.Messaging.Elements;

namespace Econolite.Ode.Status.CorridorSpeedEvent.Messaging;

public interface ICorridorSegmentSpeedEventConsumer
{
    (ConsumeResult ConsumeResult, CorridorSegmentSpeedEvent CorridorSegmentSpeedEvent) Consume(CancellationToken cancellationToken);
    void Complete(ConsumeResult consumeResult);
}
