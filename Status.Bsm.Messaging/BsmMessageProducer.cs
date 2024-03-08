// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Confluent.Kafka;
using Econolite.Ode.Messaging;

using Microsoft.Extensions.Options;
using Status.Bsm.Messaging.Extensions;
using System.Text;
using System.Text.Json;

namespace Status.Bsm.Messaging
{
    public class BsmMessageProducer : IBsmMessageProducer
    {
        private readonly ISink<JsonDocument> _producer;
        private readonly BsmMessagingOptions _bsmMessagingOptions;

        public BsmMessageProducer(ISink<JsonDocument> producer, IOptions<BsmMessagingOptions> bsmMessagingOptions)
        {
            _producer = producer;
            _bsmMessagingOptions = bsmMessagingOptions.Value;
        }

        public async Task ProduceAsync(Guid tenantId, JsonDocument? message, CancellationToken cancellationToken)
        {
            var headers = new Headers
    {
        { "tenantId", Encoding.ASCII.GetBytes(tenantId.ToString()) }
    };

            var options = (_bsmMessagingOptions.BsmMessageTopic, tenantId);
            await _producer.SinkAsync(options, tenantId, message, cancellationToken);
        }
    }
}
