// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Status.SpeedEvent.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Econolite.Ode.Status.Speed;

namespace Status.SpeedEvent.Messaging
{
    public class SpeedEventConsumer : ISpeedEventConsumer
    {
        private readonly IConsumer<Guid, Econolite.Ode.Status.Speed.SpeedEvent> _consumer;
        private readonly string[] _wantedtypes;

        public SpeedEventConsumer(IConfiguration configuration, IConsumer<Guid, Econolite.Ode.Status.Speed.SpeedEvent> consumer, IOptions<SpeedEventOptions> options)
        {
            _consumer = consumer;
            _wantedtypes = new string[]
            {
            typeof(Econolite.Ode.Status.Speed.SpeedEvent).Name,
            };
            _consumer.Subscribe(configuration[options.Value.ConfigTopic]);
        }

        public void Complete(ConsumeResult consumeResult) => _consumer.Complete(consumeResult);

        public (ConsumeResult ConsumeResult, Econolite.Ode.Status.Speed.SpeedEvent SpeedEvent) Consume(CancellationToken cancellationToken)
        {
            var consumeresult = _consumer.Consume(_ => _wantedtypes.Contains(_), cancellationToken);
            return (consumeresult, consumeresult.ToObject<Econolite.Ode.Status.Speed.SpeedEvent>());
        }
    }
}
