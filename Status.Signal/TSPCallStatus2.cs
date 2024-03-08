// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Signal
{
    public enum TspCallStatus2 : byte
    {
        EnabledIdle = 0,
        CallFromInput = 1,
        CallFromNTCIP = 2,
        CallBeingServed = 3,
        CallReserviced = 4,
        CallInhibited = 5,
        Disabled = 6,
        ProgrammingError = 7,
        EarlyExtendedGreen = 8,
    }
}
