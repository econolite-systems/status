// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System.Text.Json;
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Messaging.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Status.Bsm.Messaging.Extensions
{
    public static class Defined
    {
        public static IServiceCollection AddBsmMessageProducer(this IServiceCollection services, Action<BsmMessagingOptions> messageOptions, Action<ProducerOptions<Guid, JsonDocument>> producerOptions) =>
            services.AddMessaging()
            .Configure<BsmMessagingOptions>(_ => messageOptions(_))
            .Configure<ProducerOptions<Guid, JsonDocument>>(_ => producerOptions(_))
            .AddTransient<IMessageFactory<JsonDocument>, MessageFactory<JsonDocument>>()
            .AddTransient<ISink<JsonDocument>, Sink<JsonDocument>>()
            .AddTransient<IBsmMessageProducer, BsmMessageProducer>();
    }

}
