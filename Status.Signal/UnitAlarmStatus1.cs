// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;

namespace Econolite.Ode.Status.Signal
{
    [Flags]
    public enum UnitAlarmStatus1 : byte
    {
        CycleFault = 0x1,
        CoordFault = 0x2,
        CoordFail = 0x4,
        CycleFail = 0x8,
        MMUFlash = 0x10,
        LocalFlash = 0x20,
        LocalFree = 0x40,
        CoordActive = 0x80,
    }
}
