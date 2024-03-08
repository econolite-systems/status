// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Status.Common;
using Econolite.Ode.Status.Common.Compare;
using Econolite.Ode.Status.Common.Messaging;

namespace Econolite.Ode.Status.WrongWayDriver.Messaging;

public class WrongWayDriverEventStatusHandler : IActionEventStatusHandler
{
    public string Type { get; } = "WrongWayDriverEvent";
    
    public ActionEventStatus ToObject(ConsumeResult<Guid, ActionEventStatus> result)
    {  
        var wwd = result.ToObject<WrongWayDriverEvent>();
        wwd.ActionEventType = "WrongWayDriver";
        return wwd;
    }
    
    public async Task<(double Latitude, double Longitude)> GetLocation(ActionEventStatus actionEventStatus, Func<Guid, Task<(double Latitude, double Logitude)>> getDeviceLocation, Guid deviceId = default)
    {
        ArgumentNullException.ThrowIfNull(actionEventStatus);
        return actionEventStatus switch
        {
            WrongWayDriverEvent wrongWayDriverEvent => await Task.FromResult((wrongWayDriverEvent.Latitude, wrongWayDriverEvent.Longitude)),
            _ => await Task.FromResult((0, 0))
        };
    }
    
    public Guid GetSourceId(ActionEventStatus actionEventStatus)
    {
        return Guid.Empty;
    }
    
    public IEnumerable<StatementStatus> HandleActionEventStatus(ActionEventStatus actionEventStatus, IEnumerable<Statement> statements)
    {
        ArgumentNullException.ThrowIfNull(actionEventStatus);
        ArgumentNullException.ThrowIfNull(statements);
        var result = new List<StatementStatus>();
        if (actionEventStatus is not WrongWayDriverEvent status) return result;

        result.AddRange(statements.ToArray()
            .Select(statement => status.ToFuncCompare(statement.Property)()
                ? statement.ToStatus(Guid.Empty, true)
                : statement.ToStatus(Guid.Empty, false)));

        return result;
    }
}
