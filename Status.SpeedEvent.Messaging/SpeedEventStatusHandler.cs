// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Status.Common;
using Econolite.Ode.Status.Common.Compare;
using Econolite.Ode.Status.Common.Messaging;
using Econolite.Ode.Status.Speed;

namespace Econolite.Ode.Status.SpeedEvent.Messaging
{
    public class SpeedEventStatusHandler : IActionEventStatusHandler
    {
        public string Type { get; } = "SpeedEvent";
        
        public ActionEventStatus ToObject(ConsumeResult<Guid, ActionEventStatus> result)
        {
            var spd = result.ToObject<Speed.SpeedEvent>();
             spd.ActionEventType = "SpeedEvent";
             return spd;
        }

        public async Task<(double Latitude, double Longitude)> GetLocation(ActionEventStatus actionEventStatus, Func<Guid, Task<(double Latitude, double Logitude)>> getDeviceLocation, Guid deviceId = default)
        {
            ArgumentNullException.ThrowIfNull(actionEventStatus);
            return actionEventStatus switch
            {
                Speed.SpeedEvent speedEvent => await Task.FromResult((speedEvent.Latitude, speedEvent.Longitude)),
                _ => await Task.FromResult((0, 0))
            };
        }

        public Guid GetSourceId(ActionEventStatus actionEventStatus)
        {
            ArgumentNullException.ThrowIfNull(actionEventStatus);
            return actionEventStatus is not Speed.SpeedEvent status ? Guid.Empty : status.DeviceId;
        }

        public IEnumerable<StatementStatus> HandleActionEventStatus(ActionEventStatus actionEventStatus, IEnumerable<Statement> statements)
        {
            ArgumentNullException.ThrowIfNull(actionEventStatus);
            ArgumentNullException.ThrowIfNull(statements);
            var result = new List<StatementStatus>();
            if (actionEventStatus is not Speed.SpeedEvent status) return result;

            result.AddRange(statements.ToArray()
                .Select(statement => status.ToFuncCompare(statement.Property)()
                    ? statement.ToStatus(status.DeviceId, true)
                    : statement.ToStatus(status.DeviceId, false)));

            return result;
        }

        public async Task<(double Latitude, double Longitude)> GetLocation(ActionEventDeviceStatus actionEventStatus, Func<Guid, Task<(double Latitude, double Logitude)>> getDeviceLocation, Guid deviceId = default)
        {
            ArgumentNullException.ThrowIfNull(actionEventStatus);
            return actionEventStatus switch
            {
                Speed.SpeedEvent speedEvent => await Task.FromResult((speedEvent.Latitude, speedEvent.Longitude)),
                _ => await Task.FromResult((0, 0))
            };
        }
    }
}

