// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Status.PavementCondition;

namespace Econolite.Ode.Status.PavementCondition.Messaging;

public class PavementConditionConsumer : IPavementConditionConsumer
{
    private readonly IConsumer<Guid, PavementConditionStatus> _consumer;
    private readonly string[] _wantedtypes;

    public PavementConditionConsumer(IConfiguration configuration, IConsumer<Guid, PavementConditionStatus> consumer, IOptions<PavementConditionConsumerOptions> options)
    {
        _consumer = consumer;
        _wantedtypes = new string[]
        {
            typeof(PavementConditionStatus).Name,
        };
        _consumer.Subscribe(configuration[options.Value.ConfigTopic]);
    }
    
    public void Complete(ConsumeResult consumeResult) => _consumer.Complete(consumeResult);
    
    public (ConsumeResult ConsumeResult, PavementConditionStatus PavementConditionStatus) Consume(CancellationToken cancellationToken)
    {
        var consumeresult = _consumer.Consume(_ => _wantedtypes.Contains(_), cancellationToken);
        return (consumeresult, consumeresult.ToObject<PavementConditionStatus>());
    }
}
