// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Confluent.Kafka;
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Extensions;
using Econolite.Ode.Status.Signal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Status.Signal.Messaging.Extensions;
using System.Text;

namespace Status.Signal.Messaging
{
    public class SignalStatusProducer : ISignalStatusProducer
    {
        private readonly Confluent.Kafka.IProducer<string, string> _producer;
        private readonly SignalStatusMessagingOptions _signalStatusMessagingOptions;

        public SignalStatusProducer(IBuildMessagingConfig buildMessagingConfig, IOptions<SignalStatusMessagingOptions> signalMessagingOptions, IOptions<ProducerOptions<Guid, SignalStatus>> producerOptions, ILoggerFactory loggerFactory)
        {
            _producer = new ProducerBuilder<string, string>(buildMessagingConfig.BuildProducerClientConfig(producerOptions.Value))
            .AddLogging(loggerFactory.CreateLogger(GetType().Name))
            .Build();
            _signalStatusMessagingOptions = signalMessagingOptions.Value;
        }

        public async Task ProduceAsync(Guid tenantId, SignalStatus signalStatus, CancellationToken cancellationToken)
        {
            var headers = new Headers
            {
                { "tenantId", Encoding.ASCII.GetBytes(tenantId.ToString()) }
            };
            await _producer.ProduceAsync(_signalStatusMessagingOptions.SignalStatusTopic, new Message<string, string>
            {
                Key = signalStatus.DeviceId.ToString(),
                Value = signalStatus.ToSerializedPollingStatusMessage(tenantId),
                Headers = headers
            });
        }
    }
}
