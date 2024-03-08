// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Messaging.Extensions;
using Econolite.Ode.Status.Common;
using Econolite.Ode.Status.Common.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using Consts = Econolite.Ode.Status.Common.Messaging.Consts;

namespace Status.Common.Messaging.Extensions
{
    public static class Defined
    {
        public static IServiceCollection AddDeviceStatusConsumer(this IServiceCollection services) => services
            .AddMessaging()
            .AddTransient<IPayloadSpecialist<DeviceCommStatus>, JsonPayloadSpecialist<DeviceCommStatus>>()
            .AddTransient<IConsumeResultFactory<Guid, DeviceCommStatus>, ConsumeResultFactory<DeviceCommStatus>>()
            .AddTransient<IConsumer<Guid, DeviceCommStatus>, Consumer<Guid, DeviceCommStatus>>();
        public static IServiceCollection AddDeviceStatusConsumer(this IServiceCollection services, Action<ConsumerOptions<Guid, DeviceCommStatus>> options) => services
            .Configure<ConsumerOptions<Guid, DeviceCommStatus>>(_ => options(_))
            .AddDeviceStatusConsumer();

        public static IServiceCollection AddDeviceStatusProducer(this IServiceCollection services) => services
            .AddMessaging()
            .Configure<MessageFactoryOptions<DeviceCommStatus>>(_ =>
            {
                _.FuncBuildPayloadElement = _ => new BaseJsonPayload<DeviceCommStatus>(_);
            })
            .AddTransient<IMessageFactory<DeviceCommStatus>, MessageFactory<DeviceCommStatus>>()
            .AddTransient<IMessageFactory<Guid, DeviceCommStatus>, MessageFactory<DeviceCommStatus>>()
            .AddTransient<ISink<DeviceCommStatus>, Sink<DeviceCommStatus>>()
            .AddTransient<IProducer<Guid, DeviceCommStatus>, Producer<Guid, DeviceCommStatus>>()
            .AddTransient<IDeviceCommStatusProducer, DeviceCommStatusProducer>();
        public static IServiceCollection AddDeviceStatusProducer(this IServiceCollection services, Action<ProducerOptions<Guid, DeviceCommStatus>> options) => services
            .Configure<ProducerOptions<Guid, DeviceCommStatus>>(_ => options(_))
            .AddDeviceStatusProducer();

        public static IServiceCollection AddActionEventStatusSink(this IServiceCollection services) => services
            .AddActionEventStatusSink(Consts.ACTION_SET_EVENT_DEFAULT_CHANNEL);
        public static IServiceCollection AddActionEventStatusSink(this IServiceCollection services, IConfiguration configuration) => services
            .AddActionEventStatusSink(configuration[Consts.ACTION_SET_EVENT_DEFAULT_CONFIGURATION_PATH] ?? Consts.ACTION_SET_EVENT_DEFAULT_CHANNEL);
        public static IServiceCollection AddActionEventStatusSink(this IServiceCollection services, string channel) => services
            .AddActionEventStatusSink(_ => _.DefaultChannel = channel);
        public static IServiceCollection AddActionEventStatusSink(this IServiceCollection services, Action<SinkOptions<ActionEventStatus>> sinkOptions) => services
            .AddMessaging()
            .AddMessagingJsonSink(sinkOptions);

        public static IServiceCollection AddActionEventStatusSource(this IServiceCollection services) => services
            .AddMessaging()
            .AddMessagingJsonSource<ActionEventStatus>();
    }
}

