using Econolite.Ode.Status.Common;

namespace Econolite.Ode.Status.CorridorSpeedEvent.Messaging
{
    public class CorridorSpeedEventStatusType : IActionEventStatusType
    {
        public string Type { get; } = nameof(CorridorSegmentSpeedEvent);
        public async Task ProcessAsync(ActionEventStatus actionEventStatus)
        {
            if (actionEventStatus is not CorridorSegmentSpeedEvent status)
            {
                throw new ArgumentException($"ActionEventStatus is not of type {nameof(CorridorSegmentSpeedEvent)}");
            }

            await Task.CompletedTask;
        }
    }
}
