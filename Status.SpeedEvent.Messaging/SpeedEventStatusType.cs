// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Status.Common;

namespace Econolite.Ode.Status.SpeedEvent.Messaging
{
    public class SpeedEventStatusType : IActionEventStatusType
    {
        public string Type { get; } = nameof(Speed.SpeedEvent);
        public async Task ProcessAsync(ActionEventStatus actionEventStatus)
        {
            if (actionEventStatus is not Speed.SpeedEvent status)
            {
                throw new ArgumentException($"ActionEventStatus is not of type {nameof(Speed.SpeedEvent)}");
            }

            await Task.CompletedTask;
        }
    }
}
