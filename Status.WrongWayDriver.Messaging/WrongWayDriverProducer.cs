// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Microsoft.Extensions.Options;

namespace Econolite.Ode.Status.WrongWayDriver.Messaging
{
    public class WrongWayDriverProducer : IWrongWayDriverProducer
    {
        private readonly IProducer<Guid, WrongWayDriverEvent> _producer;
        private readonly IMessageFactory<Guid, WrongWayDriverEvent> _messageFactory;
        private readonly string _topic;

        public WrongWayDriverProducer(IProducer<Guid, WrongWayDriverEvent> producer, IMessageFactory<Guid, WrongWayDriverEvent> messageFactory, IOptions<WrongWayDriverOptions> options)
        {
            _producer = producer;
            _messageFactory = messageFactory;
            _topic = options.Value.ConfigTopic;
        }

        public async Task ProduceAsync(Guid tenantId, WrongWayDriverEvent wrongWayDriverStatus, CancellationToken cancellationToken)
        {
            await _producer.ProduceAsync(_topic, _messageFactory.Build(tenantId, wrongWayDriverStatus));
        }
    }
}
