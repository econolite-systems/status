// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Signal
{
    public enum UnitFlashStatus : byte
    {
        None = 0,
        Other = 1,
        NotFlash = 2,
        Automatic = 3,
        LocalManual = 4,
        FaultMonitor = 5,
        Mmu = 6,
        Startup = 7,
        Preempt = 8,
    }
}
