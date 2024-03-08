// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Econolite.Ode.Status.Signal.Cache.Extensions
{
    public static class Defined
    {
        public static IServiceCollection AddSignalStatusCache(this IServiceCollection services) => services
            .AddTransient<ISignalStatusCache, SignalStatusCache>();
        public static IServiceCollection AddSignalStatusCache(this IServiceCollection services, Action<SignalStatusCacheOptions> options) => services
            .Configure<SignalStatusCacheOptions>(_ => options(_))
            .AddSignalStatusCache();

        public static string ToSignalStatusHashKey(this Guid tenantId) =>
            $"{tenantId.ToString().ToUpper()}:SignalStatus";
    }
}
