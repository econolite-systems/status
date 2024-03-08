// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Signal
{
    public enum RingStatus : byte
    {
        MinGreen = 0,
        Extension = 1,
        Maximum = 2,
        GreenRest = 3,
        YellowChange = 4,
        RedClearance = 5,
        RedRest = 6,
        Undefined = 7,
    }
}
