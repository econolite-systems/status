// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System.Text.Json;

namespace Status.Bsm.Messaging
{
    public interface IBsmMessageProducer
    {
        Task ProduceAsync(Guid tenantId, JsonDocument message, CancellationToken cancellationToken);
    }
}
