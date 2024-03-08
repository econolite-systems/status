// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Ess.Extensions
{
    public static class Defined
    {
        public static enumRoadCondition FromLufftToenumRoadCondition(this int _)
        {
            switch (_)
            {
                case 0:
                case 10:
                    return enumRoadCondition.Dry;
                case 1:
                case 15:
                    return enumRoadCondition.Damp;
                case 2:
                case 20:
                    return enumRoadCondition.Wet;
                case 3:
                case 35:
                    return enumRoadCondition.Ice;
                case 4:
                    return enumRoadCondition.SnowOrIce;
                case 5:
                case 30:
                    return enumRoadCondition.ChemicallyWet;
                case 6:
                    return enumRoadCondition.CriticallyWet;
                case 8:
                case 40:
                    return enumRoadCondition.Snow;
                case 25:
                    return enumRoadCondition.ChemicallyDamp;
                case 45:
                    return enumRoadCondition.FrostOrRime;
                default:
                    return enumRoadCondition.Unknown;
            }
        }
    }
}
