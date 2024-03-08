// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Status.Common.Compare;

namespace Econolite.Ode.Status.Common.Messaging;

public class ActionEventStatusHandler : IActionEventStatusHandler
{
    public string Type => nameof(ActionEventStatus);
    
    public ActionEventStatus ToObject(ConsumeResult<Guid, ActionEventStatus> result)
    {
        return result.ToObject<ActionEventStatus>();
    }

    public Guid GetSourceId(ActionEventStatus actionEventStatus)
    {
        return Guid.Empty;
    }
    
    public async Task<(double Latitude, double Longitude)> GetLocation(ActionEventStatus actionEventStatus, Func<Guid, Task<(double Latitude, double Logitude)>> getDeviceLocation,
        Guid deviceId = default)
    {
        return await Task.FromResult((Latitude:(double)0, Longitude:(double)0));
    }

    public virtual IEnumerable<StatementStatus> HandleActionEventStatus(ActionEventStatus actionEventStatus, IEnumerable<Statement> statements)
    {
        ArgumentNullException.ThrowIfNull(actionEventStatus);
        ArgumentNullException.ThrowIfNull(statements);
        return Array.Empty<StatementStatus>();
    }
}
