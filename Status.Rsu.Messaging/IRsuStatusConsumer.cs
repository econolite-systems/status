// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging.Elements;

namespace Econolite.Ode.Status.Rsu.Messaging;

public interface IRsuStatusConsumer
{
    (ConsumeResult<Guid, RsuSystemStats> ConsumeResult, RsuSystemStats rsuSystemStats) Consume(CancellationToken cancellationToken);
    void Complete(ConsumeResult consumeResult);
}
