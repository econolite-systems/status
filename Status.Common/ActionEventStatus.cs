// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;

namespace Econolite.Ode.Status.Common
{
    public class ActionEventStatus
    {
        public string ActionEventType { get; set; } = "";
        public DateTime TimeStamp { get; set; }
    }

    public class ActionEventDeviceStatus : ActionEventStatus
    {
        public Guid DeviceId { get; set; }
    }
}
