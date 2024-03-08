// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Status.Common;
using Econolite.Ode.Status.Common.Compare;
using Econolite.Ode.Status.Common.Messaging;

namespace Econolite.Ode.Status.Ess
{
    public class EssActionEventStatusHandler : IActionEventStatusHandler
    {
        public string Type => "EssActionEventStatus";

        public ActionEventStatus ToObject(ConsumeResult<Guid, ActionEventStatus> result)
        {
            return result.ToObject<EssActionEventStatus>();
        }
        
        public Guid GetSourceId(ActionEventStatus actionEventStatus)
        {
            ArgumentNullException.ThrowIfNull(actionEventStatus);
            if (actionEventStatus is not EssActionEventStatus ess) throw new ArgumentException("ActionEventStatus is not EssActionEventStatus");
            return ess.DeviceId;
        }
        
        public async Task<( double Latitude, double Longitude)> GetLocation(ActionEventStatus actionEventStatus, Func<Guid, Task<(double Latitude, double Logitude)>> getDeviceLocation, Guid deviceId = default)
        {
            ArgumentNullException.ThrowIfNull(actionEventStatus);
            ArgumentNullException.ThrowIfNull(getDeviceLocation);
            if (actionEventStatus is not EssActionEventStatus ess) throw new ArgumentException("ActionEventStatus is not EssActionEventStatus");
            return await getDeviceLocation(ess.DeviceId);
        }
        
        public IEnumerable<StatementStatus> HandleActionEventStatus(ActionEventStatus actionEventStatus, IEnumerable<Statement> statements)
        {
            ArgumentNullException.ThrowIfNull(actionEventStatus);
            ArgumentNullException.ThrowIfNull(statements);
            var result = new List<StatementStatus>();
            if (actionEventStatus is not EssActionEventStatus essStatus) return result;
            
            foreach (var statement in statements.ToArray())
            {
                result.Add(essStatus.ToFuncCompare(statement.Property)() 
                    ? statement.ToStatus(essStatus.DeviceId, true)
                    : statement.ToStatus(essStatus.DeviceId, false));
            }

            return result;
        }
    }
}
