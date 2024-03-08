// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Messaging.Extensions;
using Econolite.Ode.Status.Common.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Econolite.Ode.Status.WrongWayDriver.Messaging.Extensions;

public static class Defined
{
    public static IServiceCollection AddWrongWayDriverConsumer(this IServiceCollection services) => services
        .AddMessaging()
        .AddTransient<IPayloadSpecialist<WrongWayDriverEvent>, JsonPayloadSpecialist<WrongWayDriverEvent>>()
        .AddTransient<IConsumeResultFactory<Guid, WrongWayDriverEvent>, ConsumeResultFactory<WrongWayDriverEvent>>()

        .AddTransient<IConsumer<Guid, WrongWayDriverEvent>, Consumer<Guid, WrongWayDriverEvent>>()
        .AddTransient<IWrongWayDriverConsumer, WrongWayDriverConsumer>();

    public static IServiceCollection AddWrongWayDriverConsumer(this IServiceCollection services, Action<WrongWayDriverOptions> options) =>  services
        .Configure<WrongWayDriverOptions>(_ => options(_))
        .AddWrongWayDriverConsumer();
    
    public static IServiceCollection AddWrongWayDriverConsumer(this IServiceCollection services,
        Action<WrongWayDriverOptions> options,
        Action<ConsumerOptions<Guid, WrongWayDriverEvent>> optionsWrongWayDriverStatus) => services
            .Configure<ConsumerOptions<Guid, WrongWayDriverEvent>>(_ => optionsWrongWayDriverStatus(_))
            .AddWrongWayDriverConsumer(options);

    public static IServiceCollection AddWrongWayDriverProducer(this IServiceCollection services, Action<WrongWayDriverOptions> options) => services
        .AddMessaging()
        .Configure(options)
        .Configure<MessageFactoryOptions<WrongWayDriverEvent>>(_ =>
        {
            _.FuncBuildPayloadElement = _ => new BaseJsonPayload<WrongWayDriverEvent>(_);
        })
        .AddTransient<IMessageFactory<Guid, WrongWayDriverEvent>, MessageFactory<WrongWayDriverEvent>>()
        .Configure<MessageFactoryOptions<WrongWayDriverEvent>>(_ => _.FuncBuildPayloadElement = (x) => new BaseJsonPayload<WrongWayDriverEvent>(x))
        .AddTransient<MessageFactory<WrongWayDriverEvent>>()
        .AddTransient<IProducer<Guid, WrongWayDriverEvent>, Producer<Guid, WrongWayDriverEvent>>()
        .AddTransient<IWrongWayDriverProducer, WrongWayDriverProducer>();
    
    public static IServiceCollection AddWrongWayDriverStatusHandler(this IServiceCollection services) => services
        .AddTransient<IActionEventStatusHandler, WrongWayDriverEventStatusHandler>();
}
