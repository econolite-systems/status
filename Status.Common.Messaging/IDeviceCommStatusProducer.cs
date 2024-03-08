// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;
using System.Threading.Tasks;

namespace Econolite.Ode.Status.Common.Messaging
{
    public interface IDeviceCommStatusProducer
    {
        Task ProduceAsync(string topic, Guid tenantId, DeviceCommStatus deviceCommStatus);
    }
}
