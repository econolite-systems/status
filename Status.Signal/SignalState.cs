// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Signal
{
    public enum SignalState
    {
        Offline,
        CommFail,
        Standby,
        Flash,
        Preempt,
        Transition,
        Coordination,
        Free,
        AutomaticFlash,
    }
}
