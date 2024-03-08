// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Status.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;

namespace Econolite.Ode.Status.Ess.Messaging
{
    public class EssStatusConsumer : IEssStatusConsumer
    {
        private readonly IConsumer<Guid, DeviceCommStatus> _consumer;
        private readonly string[] _wantedtypes;

        public EssStatusConsumer(IConfiguration configuration, IConsumer<Guid, DeviceCommStatus> consumer, IOptions<EssStatusConsumerOptions> options)
        {
            _consumer = consumer;
            _wantedtypes = new string[]
            {
                typeof(EssStatus).Name,
            };
            _consumer.Subscribe(configuration[options.Value.ConfigTopic]);
        }

        public void Complete(ConsumeResult consumeResult) => _consumer.Complete(consumeResult);

        (ConsumeResult<Guid, DeviceCommStatus> ConsumeResult, EssStatus EssStatus) IEssStatusConsumer.Consume(CancellationToken cancellationToken)
        {
            var consumeresult = _consumer.Consume(_ => _wantedtypes.Contains(_), cancellationToken);
            return (consumeresult, consumeresult.ToObject<EssStatus>());
        }
    }
}
