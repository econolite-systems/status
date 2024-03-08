// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Ess
{
    public enum enumRoadCondition
    {
        Unknown = 0,
        Dry = 1,
        Damp = 2,
        ChemicallyDamp = 3,
        Wet = 4,
        ChemicallyWet = 5,
        CriticallyWet = 6,
        FrostOrRime = 7,
        Snow = 8,
        SnowOrIce = 9,
        Ice = 10,
        Error = 255,
    }
}
