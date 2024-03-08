// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Messaging.Extensions;
using Econolite.Ode.Status.Common.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Status.PavementCondition;

namespace Econolite.Ode.Status.PavementCondition.Messaging.Extensions;

public static class Defined
{
    public static IServiceCollection AddPavementConditionConsumer(this IServiceCollection services) => services
        .AddMessaging()
        .AddTransient<IPayloadSpecialist<PavementConditionStatus>, JsonPayloadSpecialist<PavementConditionStatus>>()
        .AddTransient<IConsumeResultFactory<Guid, PavementConditionStatus>, ConsumeResultFactory<PavementConditionStatus>>()
        .AddTransient<IConsumer<Guid, PavementConditionStatus>, Consumer<Guid, PavementConditionStatus>>()
        .AddTransient<IPavementConditionConsumer, PavementConditionConsumer>();

    public static IServiceCollection AddPavementConditionConsumer(this IServiceCollection services,
        Action<PavementConditionConsumerOptions> options)
    {
        services.Configure<PavementConditionConsumerOptions>(_ => options(_))
            .AddPavementConditionConsumer();

        return services;
    }
    
    public static IServiceCollection AddPavementConditionConsumer(this IServiceCollection services,
        Action<PavementConditionConsumerOptions> options,
        Action<ConsumerOptions<Guid, PavementConditionStatus>> optionsPavementConditionStatus) => services
            .Configure<ConsumerOptions<Guid, PavementConditionStatus>>(_ => optionsPavementConditionStatus(_))
            .AddPavementConditionConsumer(options);
    
    public static IServiceCollection AddPavementConditionStatusHandler(this IServiceCollection services) => services
        .AddTransient<IActionEventStatusHandler, PavementConditionEventStatusHandler>();
}
