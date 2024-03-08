using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Messaging;
using Econolite.Ode.Status.Common.Messaging;
using Econolite.Ode.Messaging.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Econolite.Ode.Status.CorridorSpeedEvent.Messaging;

public static class Defined
{
    public static IServiceCollection AddCorridorSpeedConsumer(this IServiceCollection services) => services
        .AddMessaging()
        .AddTransient<IPayloadSpecialist<CorridorSegmentSpeedEvent>, JsonPayloadSpecialist<CorridorSegmentSpeedEvent>>()
        .AddTransient<IConsumeResultFactory<Guid, CorridorSegmentSpeedEvent>, ConsumeResultFactory<CorridorSegmentSpeedEvent>>()

        .AddTransient<IConsumer<Guid, CorridorSegmentSpeedEvent>, Consumer<Guid, CorridorSegmentSpeedEvent>>()
        .AddTransient<ICorridorSegmentSpeedEventConsumer, CorridorSegmentSpeedEventConsumer>();

    public static IServiceCollection AddCorridorSpeedConsumer(this IServiceCollection services, Action<CorridorSegmentSpeedEventOptions> options) => services
        .Configure<CorridorSegmentSpeedEventOptions>(_ => options(_))
        .AddCorridorSpeedConsumer();

    public static IServiceCollection AddCorridorSpeedConsumer(this IServiceCollection services,
        Action<CorridorSegmentSpeedEventOptions> options,
        Action<ConsumerOptions<Guid, CorridorSegmentSpeedEvent>> optionsSpeedStatus) => services
            .Configure<ConsumerOptions<Guid, CorridorSegmentSpeedEvent>>(_ => optionsSpeedStatus(_))
            .AddCorridorSpeedConsumer(options);

    public static IServiceCollection AddCorridorSpeedProducer(this IServiceCollection services, Action<CorridorSegmentSpeedEventOptions> options) => services
        .AddMessaging()
        .Configure(options)
        .Configure<MessageFactoryOptions<CorridorSegmentSpeedEvent>>(_ =>
        {
            _.FuncBuildPayloadElement = _ => new BaseJsonPayload<CorridorSegmentSpeedEvent>(_);
        })
        .AddTransient<IMessageFactory<Guid, CorridorSegmentSpeedEvent>, MessageFactory<CorridorSegmentSpeedEvent>>()
        .Configure<MessageFactoryOptions<CorridorSegmentSpeedEvent>>(_ => _.FuncBuildPayloadElement = (x) => new BaseJsonPayload<CorridorSegmentSpeedEvent>(x))
        .AddTransient<MessageFactory<CorridorSegmentSpeedEvent>>()
        .AddTransient<IProducer<Guid, CorridorSegmentSpeedEvent>, Producer<Guid, CorridorSegmentSpeedEvent>>()
        .AddTransient<ICorridorSegmentSpeedEventProducer, CorridorSegmentSpeedEventProducer>();

    public static IServiceCollection AddCorridorSpeedStatusHandler(this IServiceCollection services) => services
        .AddTransient<IActionEventDeviceStatusHandler, CorridorSegmentSpeedEventStatusHandler>();
}