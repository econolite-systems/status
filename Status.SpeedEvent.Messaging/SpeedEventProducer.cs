// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging.Elements;
using Microsoft.Extensions.Options;
using Econolite.Ode.Messaging;

namespace Econolite.Ode.Status.SpeedEvent.Messaging
{
    public class SpeedEventProducer : ISpeedEventProducer
    {
        private readonly IProducer<Guid, Speed.SpeedEvent> _producer;
        private readonly IMessageFactory<Guid, Speed.SpeedEvent> _messageFactory;
        private readonly string _topic;

        public SpeedEventProducer(IProducer<Guid, Speed.SpeedEvent> producer, IMessageFactory<Guid, Speed.SpeedEvent> messageFactory, IOptions<SpeedEventOptions> options)
        {
            _producer = producer;
            _messageFactory = messageFactory;
            _topic = options.Value.ConfigTopic;
        }

        public async Task ProduceAsync(Guid tenantId, Speed.SpeedEvent speedEventStatus, CancellationToken cancellationToken)
        {
            await _producer.ProduceAsync(_topic, _messageFactory.Build(tenantId, speedEventStatus));
        }
    }
}
