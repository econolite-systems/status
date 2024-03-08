// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Microsoft.Extensions.DependencyInjection;

namespace Status.Ess.Cache.Extensions
{
    public static class Defined
    {
        public static IServiceCollection AddEssCache(this IServiceCollection services) => services
            .AddTransient<IEssStatusCache, EssStatusCache>();
        public static IServiceCollection AddEssCache(this IServiceCollection services, Action<EssStatusCacheOptions> options) => services
            .Configure<EssStatusCacheOptions>(_ => options(_))
            .AddEssCache();

        public static string ToEssStatusKey(this Guid deviceId) =>
            $"ess-status-{deviceId}";
    }
}
