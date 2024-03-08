// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using System;
using System.Threading.Tasks;

namespace Econolite.Ode.Status.Common.Messaging
{
    public class DeviceCommStatusProducer : IDeviceCommStatusProducer
    {
        private readonly IProducer<Guid, DeviceCommStatus> _producer;
        private readonly IMessageFactory<Guid, DeviceCommStatus> _messageFactory;

        public DeviceCommStatusProducer(IProducer<Guid, DeviceCommStatus> producer, IMessageFactory<Guid, DeviceCommStatus> messageFactory)
        {
            _producer = producer;
            _messageFactory = messageFactory;
        }

        public async Task ProduceAsync(string topic, Guid tenantId, DeviceCommStatus deviceCommStatus)
        {
            await _producer.ProduceAsync(topic, _messageFactory.Build(tenantId, deviceCommStatus.DeviceId, deviceCommStatus.DeviceId, deviceCommStatus));
        }
    }
}
