// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Status.Common.Compare;

namespace Econolite.Ode.Status.Common.Messaging;

public interface IActionEventStatusHandler
{
    string Type { get; }
    ActionEventStatus ToObject(ConsumeResult<Guid, ActionEventStatus> result);
    Task<(double Latitude, double Longitude)> GetLocation(ActionEventStatus actionEventStatus, Func<Guid,Task<(double Latitude, double Logitude)>> getDeviceLocation, Guid deviceId = default);
    Guid GetSourceId(ActionEventStatus actionEventStatus);
    IEnumerable<StatementStatus> HandleActionEventStatus(ActionEventStatus actionEventStatus, IEnumerable<Statement> statements);
}
