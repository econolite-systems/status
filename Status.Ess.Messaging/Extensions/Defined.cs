// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Extensions;
using Econolite.Ode.Status.Common;
using Econolite.Ode.Status.Common.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Status.Common.Messaging.Extensions;
using System;

namespace Econolite.Ode.Status.Ess.Messaging.Extensions
{
    public static class Defined
    {
        /// <summary>
        /// There is only a EssStatusConsumer as the producer can simply be the
        /// IProducer&lt;Guid, DeviceCommStatus>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEssStatusConsumer(this IServiceCollection services) => services
            .AddDeviceStatusConsumer()
            .AddTransient<IEssStatusConsumer, EssStatusConsumer>();
        public static IServiceCollection AddEssStatusConsumer(this IServiceCollection services, Action<EssStatusConsumerOptions> options)
        {
            services.Configure<EssStatusConsumerOptions>(_ => options(_))
            .AddEssStatusConsumer();

            return services;
        }
        public static IServiceCollection AddEssStatusConsumer(this IServiceCollection services, Action<EssStatusConsumerOptions> options, Action<ConsumerOptions<Guid, DeviceCommStatus>> optionsDeviceCommStatus) => services
            .Configure<ConsumerOptions<Guid, DeviceCommStatus>>(_ => optionsDeviceCommStatus(_))
            .AddEssStatusConsumer(options);

        public static IServiceCollection AddEssActionEventStatusHandler(this IServiceCollection services) => services
            .AddTransient<IActionEventStatusHandler, EssActionEventStatusHandler>();

        public static IServiceCollection AddEssStatusSink(this IServiceCollection services) => services
            .AddEssStatusSink(Consts.ESS_STATUS_DEFAULT_CHANNEL);
        public static IServiceCollection AddEssStatusSink(this IServiceCollection services, IConfiguration configuration) => services
            .AddEssStatusSink(configuration[Consts.ESS_STATUS_DEFAULT_CONFIGURATION_PATH] ?? Consts.ESS_STATUS_DEFAULT_CHANNEL);
        public static IServiceCollection AddEssStatusSink(this IServiceCollection services, string channel) => services
            .AddEssStatusSink(_ => _.DefaultChannel = channel);
        public static IServiceCollection AddEssStatusSink(this IServiceCollection services, Action<SinkOptions<EssStatus>> sinkOptions) => services
            .AddMessaging()
            .AddMessagingJsonSink(sinkOptions);

        public static IServiceCollection AddEssStatusSource(this IServiceCollection services) => services
            .AddMessaging()
            .AddMessagingJsonSource<EssStatus>();
    }
}
