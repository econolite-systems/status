// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Econolite.Ode.Messaging.Extensions;
using Econolite.Ode.Status.Common.Messaging;
using Status.SpeedEvent.Messaging;

namespace Econolite.Ode.Status.SpeedEvent.Messaging.Extensions;

public static class Defined
{
    public static IServiceCollection AddSpeedConsumer(this IServiceCollection services) => services
        .AddMessaging()
        .AddTransient<IPayloadSpecialist<Speed.SpeedEvent>, JsonPayloadSpecialist<Speed.SpeedEvent>>()
        .AddTransient<IConsumeResultFactory<Guid, Speed.SpeedEvent>, ConsumeResultFactory<Speed.SpeedEvent>>()

        .AddTransient<IConsumer<Guid, Speed.SpeedEvent>, Consumer<Guid, Speed.SpeedEvent>>()
        .AddTransient<ISpeedEventConsumer, SpeedEventConsumer>();

    public static IServiceCollection AddSpeedConsumer(this IServiceCollection services, Action<SpeedEventOptions> options) => services
        .Configure<SpeedEventOptions>(_ => options(_))
        .AddSpeedConsumer();

    public static IServiceCollection AddSpeedConsumer(this IServiceCollection services,
        Action<SpeedEventOptions> options,
        Action<ConsumerOptions<Guid, Speed.SpeedEvent>> optionsSpeedStatus) => services
            .Configure<ConsumerOptions<Guid, Speed.SpeedEvent>>(_ => optionsSpeedStatus(_))
            .AddSpeedConsumer(options);

    public static IServiceCollection AddSpeedProducer(this IServiceCollection services, Action<SpeedEventOptions> options) => services
        .AddMessaging()
        .Configure(options)
        .Configure<MessageFactoryOptions<Speed.SpeedEvent>>(_ =>
        {
            _.FuncBuildPayloadElement = _ => new BaseJsonPayload<Speed.SpeedEvent>(_);
        })
        .AddTransient<IMessageFactory<Guid, Speed.SpeedEvent>, MessageFactory<Speed.SpeedEvent>>()
        .Configure<MessageFactoryOptions<Speed.SpeedEvent>>(_ => _.FuncBuildPayloadElement = (x) => new BaseJsonPayload<Speed.SpeedEvent>(x))
        .AddTransient<MessageFactory<Speed.SpeedEvent>>()
        .AddTransient<IProducer<Guid, Speed.SpeedEvent>, Producer<Guid, Speed.SpeedEvent>>()
        .AddTransient<ISpeedEventProducer, SpeedEventProducer>();

    public static IServiceCollection AddSpeedStatusHandler(this IServiceCollection services) => services
        .AddTransient<IActionEventStatusHandler, SpeedEventStatusHandler>();
}
