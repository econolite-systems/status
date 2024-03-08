// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Econolite.Ode.Status.Rsu.Messaging;

public class RsuStatusConsumer : IRsuStatusConsumer
{
    private readonly IConsumer<Guid, RsuSystemStats> _consumer;
    private readonly string[] _wantedtypes;

    public RsuStatusConsumer(IConfiguration configuration, IConsumer<Guid, RsuSystemStats> consumer, IOptions<RsuStatusConsumerOptions> options)
    {
        _consumer = consumer;
        _wantedtypes = new string[]
        {
            typeof(RsuSystemStats).Name,
        };
        _consumer.Subscribe(configuration[options.Value.ConfigTopic] ?? "rsu.status");
    }
    
    public void Complete(ConsumeResult consumeResult) => _consumer.Complete(consumeResult);
    
    public (ConsumeResult<Guid, RsuSystemStats> ConsumeResult, RsuSystemStats rsuSystemStats) Consume(CancellationToken cancellationToken)
    {
        var consumeresult = _consumer.Consume(_ => true, cancellationToken);
        return (consumeresult, consumeresult.ToObject<RsuSystemStats>());
    }
}
