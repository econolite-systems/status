// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging.Elements;

namespace Econolite.Ode.Status.WrongWayDriver.Messaging;

public interface IWrongWayDriverConsumer
{
    (ConsumeResult ConsumeResult, WrongWayDriverEvent WrongWayDriverEvent) Consume(CancellationToken cancellationToken);
    void Complete(ConsumeResult consumeResult);
}
