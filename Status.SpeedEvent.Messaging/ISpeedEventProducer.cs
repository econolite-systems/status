// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.SpeedEvent.Messaging
{
    public interface ISpeedEventProducer
    {
        Task ProduceAsync(Guid tenantId, Speed.SpeedEvent speedEventStatus, CancellationToken cancellationToken);
    }
}
