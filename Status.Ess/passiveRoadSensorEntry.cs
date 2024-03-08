// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Ess
{
    public class passiveRoadSensorEntry
    {
        public int Height { get; set; } = 0;
        //public string Location { get; set; } = string.Empty;
        public int SurfaceTemperature { get; set; } = 0;
        public int ExternalTemperature1 { get; set; } = 0;
        public int ExternalTemperature2 { get; set; } = 0;
        public int FreezingTemperature { get; set; } = 0;
        public int WaterFilmHeight { get; set; } = 0;
        public int SalineConcentrationNaCl { get; set; } = 0;
        public int SalineConcentrationMgCl2 { get; set; } = 0;
        public int SalineConcentrationCaCl2 { get; set; } = 0;
        public int IcePercentage { get; set; } = 0;
        public int Friction { get; set; } = 0;
        //public int RoadCondition { get; set; } = 0;
        public enumRoadCondition RoadCondition { get; set; } = enumRoadCondition.Unknown;
        public int CouplingState { get; set; } = 0;
        //public int MeasureCounter { get; set; } = 0;
    }
}
