// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Status.Common;
using System;
using System.Threading;

namespace Econolite.Ode.Status.Ess.Messaging
{
    public interface IEssStatusConsumer
    {
        (ConsumeResult<Guid, DeviceCommStatus> ConsumeResult, EssStatus EssStatus) Consume(CancellationToken cancellationToken);
        void Complete(ConsumeResult consumeResult);
    }
}
