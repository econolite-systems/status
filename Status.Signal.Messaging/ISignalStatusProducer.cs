// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Status.Signal;

namespace Status.Signal.Messaging
{
    public interface ISignalStatusProducer
    {
        Task ProduceAsync(Guid tenantId, SignalStatus signalStatus, CancellationToken cancellationToken);
    }
}
