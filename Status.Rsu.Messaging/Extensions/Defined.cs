// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Messaging.Extensions;
using Econolite.Ode.Status.Common.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Econolite.Ode.Status.Rsu.Messaging.Extensions;

public static class Defined
{
    public static IServiceCollection AddRsuStatusConsumer(this IServiceCollection services) => services
        .AddMessaging()
        .AddTransient<IPayloadSpecialist<RsuSystemStats>, JsonPayloadSpecialist<RsuSystemStats>>()
        .AddTransient<IConsumeResultFactory<Guid, RsuSystemStats>, ConsumeResultFactory<RsuSystemStats>>()
        .AddTransient<IConsumer<Guid, RsuSystemStats>, Consumer<Guid, RsuSystemStats>>()
        .AddTransient<IRsuStatusConsumer, RsuStatusConsumer>();

    public static IServiceCollection AddRsuStatusConsumer(this IServiceCollection services,
        Action<RsuStatusConsumerOptions> options)
    {
        services.Configure<RsuStatusConsumerOptions>(_ => options(_))
            .AddRsuStatusConsumer();

        return services;
    }
    
    public static IServiceCollection AddRsuStatusConsumer(this IServiceCollection services,
        Action<RsuStatusConsumerOptions> options,
        Action<ConsumerOptions<Guid, RsuSystemStats>> optionsRsuStatusStatus) => services
            .Configure<ConsumerOptions<Guid, RsuSystemStats>>(_ => optionsRsuStatusStatus(_))
            .AddRsuStatusConsumer(options);
    
    public static IServiceCollection AddRsuEventStatusHandler(this IServiceCollection services) => services
        .AddTransient<IActionEventStatusHandler, RsuEventStatusHandler>();
}
