using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Status.Common.Compare;
using Econolite.Ode.Status.Common.Messaging;
using Econolite.Ode.Status.Common;

namespace Econolite.Ode.Status.CorridorSpeedEvent.Messaging;

public class CorridorSegmentSpeedEventStatusHandler : IActionEventDeviceStatusHandler
{
    public string Type { get; } = "SpeedEvent";

    public ActionEventDeviceStatus ToObject(ConsumeResult<Guid, ActionEventDeviceStatus> result)
    {
        var spd = result.ToObject<CorridorSegmentSpeedEvent>();
        spd.ActionEventType = "SpeedEvent";
        return spd;
    }

    public async Task<(double Latitude, double Longitude)> GetLocation(ActionEventStatus actionEventStatus, Func<Guid, Task<(double Latitude, double Logitude)>> getDeviceLocation, Guid deviceId = default)
    {
        ArgumentNullException.ThrowIfNull(actionEventStatus);
        return actionEventStatus switch
        {
            CorridorSegmentSpeedEvent speedEvent => await Task.FromResult((speedEvent.Latitude, speedEvent.Longitude)),
            _ => await Task.FromResult((0, 0))
        };
    }

    public Guid GetSourceId(ActionEventDeviceStatus actionEventDeviceStatus)
    {
        return actionEventDeviceStatus.DeviceId;
    }

    public async Task<(double Latitude, double Longitude)> GetLocation(ActionEventDeviceStatus actionEventStatus, Func<Guid, Task<(double Latitude, double Logitude)>> getDeviceLocation, Guid deviceId = default)
    {
        ArgumentNullException.ThrowIfNull(actionEventStatus);
        return actionEventStatus switch
        {
            CorridorSegmentSpeedEvent speedEvent => await Task.FromResult((speedEvent.Latitude, speedEvent.Longitude)),
            _ => await Task.FromResult((0, 0))
        };
    }

    public IEnumerable<StatementStatus> HandleActionEventStatus(ActionEventDeviceStatus actionEventStatus, IEnumerable<Statement> statements)
    {
        ArgumentNullException.ThrowIfNull(actionEventStatus);
        ArgumentNullException.ThrowIfNull(statements);
        var result = new List<StatementStatus>();
        if (actionEventStatus is not CorridorSegmentSpeedEvent status) return result;

        result.AddRange(statements.ToArray()
            .Select(statement => status.ToFuncCompare(statement.Property)()
                ? statement.ToStatus(status.DeviceId, true)
                : statement.ToStatus(status.DeviceId, false)));

        return result;
    }
}