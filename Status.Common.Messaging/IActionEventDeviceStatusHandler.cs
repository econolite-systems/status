// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Status.Common.Compare;

namespace Econolite.Ode.Status.Common.Messaging
{
    public interface IActionEventDeviceStatusHandler 
    {
        string Type { get; }
        ActionEventDeviceStatus ToObject(ConsumeResult<Guid, ActionEventDeviceStatus> result);
        Task<(double Latitude, double Longitude)> GetLocation(ActionEventDeviceStatus actionEventStatus, Func<Guid, Task<(double Latitude, double Logitude)>> getDeviceLocation, Guid deviceId = default);
        Guid GetSourceId(ActionEventDeviceStatus actionEventStatus);
        IEnumerable<StatementStatus> HandleActionEventStatus(ActionEventDeviceStatus actionEventStatus, IEnumerable<Statement> statements);
    }

}
