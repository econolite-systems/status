// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;

namespace Econolite.Ode.Status.Signal
{
    [Flags]
    public enum ShortAlarmStatus : byte
    {
        None = 0,
        Preempt = 0x1,
        TandFFlash = 0x2,
        LocalZero = 0x4,
        LocalOverride = 0x8,
        CoordinationAlarm = 0x10,
        DetectorFaulted = 0x20,
        NonCriticalAlarm = 0x40,
        CriticalAlarm = 0x80,
    }
}
