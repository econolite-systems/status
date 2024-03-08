// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Status.Common;
using Econolite.Ode.Status.Common.Compare;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Econolite.Ode.Status.Ess
{
    public class EssStatus : DeviceCommStatus
    {
        public int WetBulbTemp { get; set; } = 0;
        public int DewPointTemp { get; set; } = 0;
        public int MaxTemp { get; set; } = 0;
        public int MinTemp { get; set; } = 0;
        public int AdjacentSnowDepth { get; set; } = 0;
        public int RoadwaySnowDepth { get; set; } = 0;
        public int RoadwaySnowPackDepth { get; set; } = 0;
        public EssPrecipYesNoEnum PrecipYesNo { get; set; } = EssPrecipYesNoEnum.NoPrecip;
        public int PrecipRate { get; set; } = 0;
        public int SnowfallAccumRate { get; set; } = 0;
        public EssPrecipSituationEnum PrecipSituation { get; set; } = EssPrecipSituationEnum.Unknown;
        public int IceThickness { get; set; } = 0;
        public DateTime PrecipitationStartTime { get; set; } = DateTime.MinValue;
        public DateTime PrecipitationEndTime { get; set; } = DateTime.MinValue;
        public int Visibility { get; set; } = 0;
        public EssVisibilitySituationEnum VisibilitySituation { get; set; } = EssVisibilitySituationEnum.Unknown;
        public int TotalSun { get; set; } = 0;
        public int InstantaneousTerrestrialRadiation { get; set; } = 0;
        public int InstantaneousSolarRadiation { get; set; } = 0;
        public int TotalRadiation { get; set; } = 0;
        public int TotalRadiationPeriod { get; set; } = 0;
        public EssCloudSituationEnum CloudSituation { get; set; } = EssCloudSituationEnum.Clear;
        public int RelativeHumidity { get; set; } = 0;
        public int AtmosphericPressure { get; set; } = 0;

        /// <summary>
        /// Lufft Only sensor
        /// </summary>
        public passiveRoadSensorEntry[] PassiveRoadSensorEntries { get; set; } = Array.Empty<passiveRoadSensorEntry>();
    }
    
    public class EssActionEventStatus : ActionEventDeviceStatus
    {
        public int WetBulbTemp { get; set; } = 0;
        public int DewPointTemp { get; set; } = 0;
        public int MaxTemp { get; set; } = 0;
        public int MinTemp { get; set; } = 0;
        public int AdjacentSnowDepth { get; set; } = 0;
        public int RoadwaySnowDepth { get; set; } = 0;
        public int RoadwaySnowPackDepth { get; set; } = 0;
        public EssPrecipYesNoEnum PrecipYesNo { get; set; } = EssPrecipYesNoEnum.NoPrecip;
        public int PrecipRate { get; set; } = 0;
        public int SnowfallAccumRate { get; set; } = 0;
        public EssPrecipSituationEnum PrecipSituation { get; set; } = EssPrecipSituationEnum.Unknown;
        public int IceThickness { get; set; } = 0;
        public DateTime PrecipitationStartTime { get; set; } = DateTime.MinValue;
        public DateTime PrecipitationEndTime { get; set; } = DateTime.MinValue;
        public int Visibility { get; set; } = 0;
        public EssVisibilitySituationEnum VisibilitySituation { get; set; } = EssVisibilitySituationEnum.Unknown;
        public int TotalSun { get; set; } = 0;
        public int InstantaneousTerrestrialRadiation { get; set; } = 0;
        public int InstantaneousSolarRadiation { get; set; } = 0;
        public int TotalRadiation { get; set; } = 0;
        public int TotalRadiationPeriod { get; set; } = 0;
        public EssCloudSituationEnum CloudSituation { get; set; } = EssCloudSituationEnum.Clear;
        public int RelativeHumidity { get; set; } = 0;
        public int AtmosphericPressure { get; set; } = 0;

        /// <summary>
        /// Lufft Only sensor
        /// </summary>
        public passiveRoadSensorEntry[] PassiveRoadSensorEntries { get; set; } = Array.Empty<passiveRoadSensorEntry>();
    }

    public static class EssStatusExtensions
    {
        public const string WET_BULB_TEMP = "wetBulbTemp";
        public const string DEW_POINT_TEMP = "dewPointTemp";
        public const string MAX_TEMP = "maxTemp";
        public const string MIN_TEMP = "minTemp";
        public const string ADJACENT_SNOW_DEPTH = "adjacentSnowDepth";
        public const string ROADWAY_SNOW_DEPTH = "roadwaySnowDepth";
        public const string ROADWAY_SNOW_PACK_DEPTH = "roadwaySnowPackDepth";
        public const string PRECIP_YES_NO = "precipYesNo";
        public const string PRECIP_RATE = "precipRate";
        public const string SNOWFALL_ACCUM_RATE = "snowfallAccumRate";
        public const string PRECIP_SITUATION = "precipSituation";
        public const string ICE_THICKNESS = "iceThickness";
        public const string PRECIPITATION_START_TIME = "precipitationStartTime";
        public const string PRECIPITATION_END_TIME = "precipitationEndTime";
        public const string VISIBILITY = "visibility";
        public const string VISIBILITY_SITUATION = "visibilitySituation";
        public const string TOTAL_SUN = "totalSun";
        public const string INSTANTANEOUS_TERRESTRIAL_RADIATION = "InstantaneousTerrestrialRadiation";
        public const string INSTANTANEOUS_SOLAR_RADIATION = "InstantaneousSolarRadiation";
        public const string TOTAL_RADIATION = "TotalRadiation";
        public const string TOTAL_RADIATION_PERIOD = "TotalRadiationPeriod";
        public const string CLOUD_SITUATION = "cloudSituation";
        public const string RELATIVE_HUMIDITY = "relativeHumidity";
        public const string ATMOSPHERIC_PRESSURE = "atmosphericPressure";

        public static Func<StatementProperty, IFuncCompare> IntStatusValueFuncCompare = (property) => new StatusValue<int>(property);
        public static Func<StatementProperty, IFuncCompare> DateTimeValueFuncCompare = (property) => new DateTimeValue(property);

        private static Dictionary<string, Func<StatementProperty, IFuncCompare>> _comparison = new Dictionary<string, Func<StatementProperty,IFuncCompare>>();

        static EssStatusExtensions()
        {
            _comparison.Add(WET_BULB_TEMP, IntStatusValueFuncCompare);
            _comparison.Add(DEW_POINT_TEMP, IntStatusValueFuncCompare);
            _comparison.Add(MAX_TEMP, IntStatusValueFuncCompare);
            _comparison.Add(MIN_TEMP, IntStatusValueFuncCompare);
            _comparison.Add(ADJACENT_SNOW_DEPTH, IntStatusValueFuncCompare);
            _comparison.Add(ROADWAY_SNOW_DEPTH, IntStatusValueFuncCompare);
            _comparison.Add(ROADWAY_SNOW_PACK_DEPTH, IntStatusValueFuncCompare);
            _comparison.Add(PRECIP_YES_NO, (property) => new EnumValue<EssPrecipYesNoEnum>(property));
            _comparison.Add(PRECIP_RATE, IntStatusValueFuncCompare);
            _comparison.Add(SNOWFALL_ACCUM_RATE, IntStatusValueFuncCompare);
            _comparison.Add(PRECIP_SITUATION, (property) => new EnumValue<EssPrecipSituationEnum>(property));
            _comparison.Add(ICE_THICKNESS, IntStatusValueFuncCompare);
            _comparison.Add(PRECIPITATION_START_TIME, DateTimeValueFuncCompare);
            _comparison.Add(PRECIPITATION_END_TIME, DateTimeValueFuncCompare);
            _comparison.Add(VISIBILITY, IntStatusValueFuncCompare);
            _comparison.Add(VISIBILITY_SITUATION, (property) => new EnumValue<EssVisibilitySituationEnum>(property));
            _comparison.Add(TOTAL_SUN, IntStatusValueFuncCompare);
            _comparison.Add(INSTANTANEOUS_TERRESTRIAL_RADIATION, IntStatusValueFuncCompare);
            _comparison.Add(INSTANTANEOUS_SOLAR_RADIATION, IntStatusValueFuncCompare);
            _comparison.Add(TOTAL_RADIATION, IntStatusValueFuncCompare);
            _comparison.Add(TOTAL_RADIATION_PERIOD, IntStatusValueFuncCompare);
            _comparison.Add(CLOUD_SITUATION, (property) => new EnumValue<EssCloudSituationEnum>(property));
            _comparison.Add(RELATIVE_HUMIDITY, IntStatusValueFuncCompare);
            _comparison.Add(ATMOSPHERIC_PRESSURE, IntStatusValueFuncCompare);
        }

        private static Func<StatementProperty, IFuncCompare> GetFuncCompare(string name)
        {
            if (_comparison.TryGetValue(name, out var result))
            {
                return result;
            }

            return (property) => new FalseFuncCompare(); 
        }

        public static IEnumerable<Func<bool>> ToFuncCompares(this EssActionEventStatus status, IEnumerable<StatementProperty> properties)
        {
            return properties.Select(status.ToFuncCompare);
        }

        public static Func<bool> ToFuncCompare( this EssActionEventStatus status, StatementProperty property )
        {
            var statementCompare = GetFuncCompare(property.Name);
            var funcCompare = statementCompare(property);

            switch(property.Name)
            {
                case WET_BULB_TEMP:
                    return funcCompare.CompareTo(status.WetBulbTemp);
                case DEW_POINT_TEMP:
                    return funcCompare.CompareTo(status.DewPointTemp);
                case MAX_TEMP:
                    return funcCompare.CompareTo(status.MaxTemp);
                case MIN_TEMP:
                    return funcCompare.CompareTo(status.MinTemp);
                case ADJACENT_SNOW_DEPTH:
                    return funcCompare.CompareTo(status.AdjacentSnowDepth);
                case ROADWAY_SNOW_DEPTH:
                    return funcCompare.CompareTo(status.RoadwaySnowDepth);
                case ROADWAY_SNOW_PACK_DEPTH:
                    return funcCompare.CompareTo(status.RoadwaySnowPackDepth);
                case PRECIP_YES_NO:
                    return funcCompare.CompareTo(status.PrecipYesNo);
                case PRECIP_RATE:
                    return funcCompare.CompareTo(status.PrecipRate);
                case SNOWFALL_ACCUM_RATE:
                    return funcCompare.CompareTo(status.SnowfallAccumRate);
                case PRECIP_SITUATION:
                    return funcCompare.CompareTo(status.PrecipSituation);
                case ICE_THICKNESS:
                    return funcCompare.CompareTo(status.IceThickness);
                case PRECIPITATION_START_TIME:
                    return funcCompare.CompareTo(status.PrecipitationStartTime);
                case PRECIPITATION_END_TIME:
                    return funcCompare.CompareTo(status.PrecipitationEndTime);
                case VISIBILITY:
                    return funcCompare.CompareTo(status.Visibility);
                case VISIBILITY_SITUATION:
                    return funcCompare.CompareTo(status.VisibilitySituation);
                case TOTAL_SUN:
                    return funcCompare.CompareTo(status.TotalSun);
                case INSTANTANEOUS_TERRESTRIAL_RADIATION:
                    return funcCompare.CompareTo(status.InstantaneousTerrestrialRadiation);
                case INSTANTANEOUS_SOLAR_RADIATION:
                    return funcCompare.CompareTo(status.InstantaneousSolarRadiation);
                case TOTAL_RADIATION:
                    return funcCompare.CompareTo(status.TotalRadiation);
                case TOTAL_RADIATION_PERIOD:
                    return funcCompare.CompareTo(status.TotalRadiationPeriod);
                case CLOUD_SITUATION:
                    return funcCompare.CompareTo(status.CloudSituation);
                case RELATIVE_HUMIDITY:
                    return funcCompare.CompareTo(status.RelativeHumidity);
                case ATMOSPHERIC_PRESSURE:
                    return funcCompare.CompareTo(status.AtmosphericPressure);

            }
            return () => false;
        }
    }
}
