// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Status.Ess;

namespace Status.Ess.Cache
{
    public interface IEssStatusCache
    {
        Task PutStatusAsync(Guid deviceId, EssStatus essStatus, CancellationToken cancellationToken = default(CancellationToken));
        Task<EssStatus> GetStatusAsync(Guid deviceId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
