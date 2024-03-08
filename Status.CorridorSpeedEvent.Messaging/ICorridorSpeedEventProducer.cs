namespace Econolite.Ode.Status.CorridorSpeedEvent.Messaging
{
    public interface ICorridorSpeedEventProducer
    {
        Task ProduceAsync(Guid tenantId, CorridorSegmentSpeedEvent corridorSegmentSpeedEventStatus, CancellationToken cancellationToken);
    }
}
