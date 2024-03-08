// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Status.Common;
using Econolite.Ode.Status.Common.Compare;
using Econolite.Ode.Status.Common.Messaging;

namespace Econolite.Ode.Status.Rsu.Messaging;

public class RsuEventStatusHandler : IActionEventStatusHandler
{
    private IActionEventStatusHandler _actionEventStatusHandlerImplementation;
    public string Type { get; } = "Rsu";
    public ActionEventStatus ToObject(ConsumeResult<Guid, ActionEventStatus> result)
    {
        return result.ToObject<RsuSystemStats>();
    }

    public async Task<( double Latitude, double Longitude)> GetLocation(ActionEventStatus actionEventStatus, Func<Guid, Task<(double Latitude, double Logitude)>> getDeviceLocation, Guid deviceId = default)
    {
        ArgumentNullException.ThrowIfNull(actionEventStatus);
        ArgumentNullException.ThrowIfNull(getDeviceLocation);
        return await getDeviceLocation(deviceId);
    }

    public Guid GetSourceId(ActionEventStatus actionEventStatus)
    {
        ArgumentNullException.ThrowIfNull(actionEventStatus);
        return actionEventStatus is not RsuSystemStats status ? Guid.Empty : status.DeviceId;
    }
    
    public IEnumerable<StatementStatus> HandleActionEventStatus(ActionEventStatus actionEventStatus, IEnumerable<Statement> statements)
    {
        ArgumentNullException.ThrowIfNull(actionEventStatus);
        ArgumentNullException.ThrowIfNull(statements);
        var result = new List<StatementStatus>();
        if (actionEventStatus is not RsuSystemStats rsuSystemStats) return result;

        result.AddRange(statements.ToArray()
            .Select(statement => rsuSystemStats.ToFuncCompare(statement.Property)()
                ? statement.ToStatus(rsuSystemStats.DeviceId, true)
                : statement.ToStatus(rsuSystemStats.DeviceId, false)));

        return result;
    }
}
