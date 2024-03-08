// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Status.Signal;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Econolite.Ode.Status.Signal.Cache
{
    public interface ISignalStatusCache
    {
        Task PutStatusAsync(Guid tenantId, Guid deviceId, SignalStatus status, CancellationToken cancellationToken = default(CancellationToken));
        Task<SignalStatus> GetStatusAsync(Guid tenantId, Guid deviceId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
