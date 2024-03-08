// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Status.Signal.Cache.Extensions;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Econolite.Ode.Status.Signal.Cache
{
    public class SignalStatusCache : ISignalStatusCache
    {
        private readonly IDatabase _redisdb;
        public SignalStatusCache(IConnectionMultiplexer redis)
        {
            _redisdb = redis.GetDatabase();
        }

        public async Task<SignalStatus> GetStatusAsync(Guid tenantId, Guid deviceId, CancellationToken cancellationToken = default)
        {
            SignalStatus result = default;
            byte[] rawdata = await _redisdb.HashGetAsync(tenantId.ToSignalStatusHashKey(), deviceId.ToString());
            if (rawdata != null)
            {
                result = Serializer.Deserialize(rawdata);
            }
            return result ?? new SignalStatus
            { 
                DeviceId = deviceId
            };
        }

        public async Task PutStatusAsync(Guid tenantId, Guid deviceId, SignalStatus status, CancellationToken cancellationToken = default)
        {
            await _redisdb.HashSetAsync(tenantId.ToSignalStatusHashKey(), new[] { new HashEntry(deviceId.ToString(), Serializer.Serialize(status)) }, CommandFlags.FireAndForget);
        }
    }
}
