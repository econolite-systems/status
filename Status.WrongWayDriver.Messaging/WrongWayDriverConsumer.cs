// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Econolite.Ode.Status.WrongWayDriver.Messaging;

public class WrongWayDriverConsumer : IWrongWayDriverConsumer
{
    private readonly IConsumer<Guid, WrongWayDriverEvent> _consumer;
    private readonly string[] _wantedtypes;

    public WrongWayDriverConsumer(IConfiguration configuration, IConsumer<Guid, WrongWayDriverEvent> consumer, IOptions<WrongWayDriverOptions> options)
    {
        _consumer = consumer;
        _wantedtypes = new string[]
        {
            typeof(WrongWayDriverEvent).Name,
        };
        _consumer.Subscribe(configuration[options.Value.ConfigTopic]);
    }
    
    public void Complete(ConsumeResult consumeResult) => _consumer.Complete(consumeResult);
    
    public (ConsumeResult ConsumeResult, WrongWayDriverEvent WrongWayDriverEvent) Consume(CancellationToken cancellationToken)
    {
        var consumeresult = _consumer.Consume(_ => _wantedtypes.Contains(_), cancellationToken);
        return (consumeresult, consumeresult.ToObject<WrongWayDriverEvent>());
    }
}
