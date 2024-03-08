// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging.Elements;

namespace Econolite.Ode.Status.SpeedEvent.Messaging;

public interface ISpeedEventConsumer
{
    (ConsumeResult ConsumeResult, Speed.SpeedEvent SpeedEvent) Consume(CancellationToken cancellationToken);
    void Complete(ConsumeResult consumeResult);
}
