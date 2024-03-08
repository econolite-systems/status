// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Signal
{
    public enum SignalStatusSource : byte
    {
        Unknown = 0,
        DirectPolling = 1,
        SPaTMessage = 2,
    }
}
