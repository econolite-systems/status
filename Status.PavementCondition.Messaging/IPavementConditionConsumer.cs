// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging.Elements;
using Status.PavementCondition;

namespace Econolite.Ode.Status.PavementCondition.Messaging;

public interface IPavementConditionConsumer
{
    (ConsumeResult ConsumeResult, PavementConditionStatus PavementConditionStatus) Consume(CancellationToken cancellationToken);
    void Complete(ConsumeResult consumeResult);
}
