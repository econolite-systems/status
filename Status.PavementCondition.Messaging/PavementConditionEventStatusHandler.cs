// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Status.Common;
using Econolite.Ode.Status.Common.Compare;
using Econolite.Ode.Status.Common.Messaging;
using Status.PavementCondition;

namespace Econolite.Ode.Status.PavementCondition.Messaging;

public class PavementConditionEventStatusHandler : IActionEventStatusHandler
{
    public string Type { get; } = "PavementConditionStatus";
    public ActionEventStatus ToObject(ConsumeResult<Guid, ActionEventStatus> result)
    {
        return result.ToObject<PavementConditionStatus>();
    }

    public async Task<( double Latitude, double Longitude)> GetLocation(ActionEventStatus actionEventStatus, Func<Guid, Task<(double Latitude, double Logitude)>> getDeviceLocation, Guid deviceId = default)
    {
        ArgumentNullException.ThrowIfNull(actionEventStatus);
        return actionEventStatus switch
        {
            PavementConditionStatus pavementConditionStatus => await Task.FromResult((pavementConditionStatus.Latitude, pavementConditionStatus.Longitude)),
            _ => await Task.FromResult((0, 0))
        };
    }
    
    public Guid GetSourceId(ActionEventStatus actionEventStatus)
    {
        ArgumentNullException.ThrowIfNull(actionEventStatus);
        return actionEventStatus is not PavementConditionStatus status ? Guid.Empty : status.StatusId;
    }
    
    public IEnumerable<StatementStatus> HandleActionEventStatus(ActionEventStatus actionEventStatus, IEnumerable<Statement> statements)
    {
        ArgumentNullException.ThrowIfNull(actionEventStatus);
        ArgumentNullException.ThrowIfNull(statements);
        var result = new List<StatementStatus>();
        if (actionEventStatus is not PavementConditionStatus pavementConditionStatus) return result;

        result.AddRange(statements.ToArray()
            .Select(statement => pavementConditionStatus.ToFuncCompare(statement.Property)()
                ? statement.ToStatus(pavementConditionStatus.StatusId, true)
                : statement.ToStatus(pavementConditionStatus.StatusId, false)));
        
        return result;
    }
}
