// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Status.Ess;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Status.Ess.Cache.Extensions;
using System.Text.Json;

namespace Status.Ess.Cache
{
    public class EssStatusCache : IEssStatusCache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _cacheOptions;

        public EssStatusCache(IDistributedCache distributedCache, IOptions<EssStatusCacheOptions> options)
        {
            _distributedCache = distributedCache;
            _cacheOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(options.Value.StatusTimeout);
        }

        public async Task<EssStatus> GetStatusAsync(Guid deviceId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var cached = (await _distributedCache.GetStringAsync(deviceId.ToEssStatusKey(), cancellationToken)) ?? "";
            return !string.IsNullOrWhiteSpace(cached)
                ? JsonSerializer.Deserialize<EssStatus>(cached) ?? new EssStatus()
                : new EssStatus();
        }

        public async Task PutStatusAsync(Guid deviceId, EssStatus essStatus, CancellationToken cancellationToken = default(CancellationToken)) =>
            await _distributedCache.SetStringAsync(deviceId.ToEssStatusKey(), JsonSerializer.Serialize(essStatus), _cacheOptions, cancellationToken);
    }
}
