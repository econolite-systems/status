// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Signal
{
    public enum LocalFreeStatus : byte
    {
        None = 0,
        Other = 1,
        NotFree = 2,
        CommandFree = 3,
        TransitionFree = 4,
        InputFree = 5,
        CoordFree = 6,
        BadPlan = 7,
        BadCycleTime = 8,
        SplitOverrun = 9,
        InvalidOffset = 10,
        Failed = 11,
    }
}
