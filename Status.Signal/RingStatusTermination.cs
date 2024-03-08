// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;

namespace Econolite.Ode.Status.Signal
{
    /// <summary>
    /// Termination for a ring as defined by NTCIP.  See section 2.8.6.1.
    /// This specifies why the active phase was terminated.
    /// </summary>
    [Flags]
    public enum RingStatusTermination : byte
    {
        Unknown = 0,
        GapOut = 0x1,
        MaxOut = 0x2,
        ForceOff = 0x4
    }
}
