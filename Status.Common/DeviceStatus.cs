// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;

namespace Econolite.Ode.Status.Common
{
    public class DeviceStatus
    {
        public string ExternalTag { get; set; } = null;
        public Guid DeviceId { get; set; } = Guid.Empty;
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
