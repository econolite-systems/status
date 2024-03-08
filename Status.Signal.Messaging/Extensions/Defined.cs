// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Extensions;
using Econolite.Ode.Status.Signal;
using Microsoft.Extensions.DependencyInjection;

namespace Status.Signal.Messaging.Extensions
{
    public static class Defined
    {
        public static IServiceCollection AddSignalStatusProducer(this IServiceCollection services, Action<SignalStatusMessagingOptions> messageOptions, Action<ProducerOptions<Guid, SignalStatus>> producerOptions) =>
            services.AddMessaging()
            .Configure<SignalStatusMessagingOptions>(_ => messageOptions(_))
            .Configure<ProducerOptions<Guid, SignalStatus>>(_ => producerOptions(_))
            .AddTransient<ISignalStatusProducer, SignalStatusProducer>();

        public static string ToSerializedPollingStatusMessage(this SignalStatus @this, Guid tenantId)
        {
            var message = new PollingStatusMessage()
            {
                Type = nameof(SignalStatus),
                TenantId = tenantId.ToString(),
                Payload = Convert.ToBase64String(Serializer.Serialize(@this))
            };

            return System.Text.Json.JsonSerializer.Serialize(message);
        }
    }
}

