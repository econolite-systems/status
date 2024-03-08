// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;

namespace Econolite.Ode.Status.Signal
{
    [Flags]
    public enum UnitAlarmStatus2 : byte
    {
        PowerRestart = 0x1,
        LowBattery = 0x2,
        ResponseFault = 0x4,
        ExternalStart = 0x8,
        StopTime = 0x10,
        OffsetTransitioning = 0x20,
    }
}
