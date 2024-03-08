// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Status.Common;

namespace Econolite.Ode.Status.WrongWayDriver.Messaging;

public class WrongWayDriverStatusType : IActionEventStatusType
{
    public string Type { get; } = nameof(WrongWayDriverEvent);
    public async Task ProcessAsync(ActionEventStatus actionEventStatus)
    {
        if (actionEventStatus is not WrongWayDriverEvent status)
        {
            throw new ArgumentException($"ActionEventStatus is not of type {nameof(WrongWayDriverEvent)}");
        }
        
        await Task.CompletedTask;
    }
}
