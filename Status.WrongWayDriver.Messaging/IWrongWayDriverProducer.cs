// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.WrongWayDriver.Messaging
{
    public interface IWrongWayDriverProducer
    {
        Task ProduceAsync(Guid tenantId, WrongWayDriverEvent wrongWayDriverStatus, CancellationToken cancellationToken);
    }
}
