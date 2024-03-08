// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;

namespace Econolite.Ode.Status.Signal
{
    [Flags]
    public enum pedestrianDetectorAlarmsEnum
    {
        None = 0,
        NoActivity = 0x1,
        MaxPresence = 0x2,
        ErraticOutput = 0x4,
        Communications = 0x8,
        Configuration = 0x10,
        Other = 0x80,
    }
}
