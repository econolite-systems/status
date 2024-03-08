namespace Econolite.Ode.Status.CorridorSpeedEvent.Messaging
{
    public interface ICorridorSegmentSpeedEventProducer
    {
        Task ProduceAsync(Guid tenantId, CorridorSegmentSpeedEvent corridorSegmentSpeedEventStatus, CancellationToken cancellationToken);
    }
}
