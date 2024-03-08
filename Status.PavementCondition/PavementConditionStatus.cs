// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;
using System.Collections.Generic;
using System.Linq;
using Econolite.Ode.Status.Common;
using Econolite.Ode.Status.Common.Compare;

namespace Status.PavementCondition
{
    public class PavementConditionStatus : ActionEventStatus
    {
        public Guid StatusId { get; set; } = Guid.Empty;
        public string Location { get; set; } = string.Empty;
        public double Latitude { get; set; } = 0.0;
        public double Longitude { get; set; } = 0.0;
        public string Severity { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool Active { get; set; } = false;
    }
    
    public static class PavementConditionStatusExtensions
    {
        private const string TYPE = "pctype";
        private const string SEVERITY = "pcseverity";

        public static Func<StatementProperty, IFuncCompare> IntStatusValueFuncCompare = (property) => new StatusValue<string>(property);

        private static Dictionary<string, Func<StatementProperty, IFuncCompare>> _comparison = new Dictionary<string, Func<StatementProperty,IFuncCompare>>();
        
        static PavementConditionStatusExtensions()
        {
            _comparison.Add(TYPE, (property) => new EnumValue<PavementConditionStatusType>(property));
            _comparison.Add(SEVERITY, (property) => new EnumValue<PavementConditionStatusSeverity>(property));
        }
        
        private static Func<StatementProperty, IFuncCompare> GetFuncCompare(string name)
        {
            if (_comparison.TryGetValue(name, out var result))
            {
                return result;
            }

            return (property) => new FalseFuncCompare(); 
        }

        public static IEnumerable<Func<bool>> ToFuncCompares(this PavementConditionStatus status, IEnumerable<StatementProperty> properties)
        {
            return properties.Select(status.ToFuncCompare);
        }
        
        public static Func<bool> ToFuncCompare(this PavementConditionStatus status, StatementProperty property)
        {
            var statementCompare = GetFuncCompare(property.Name);
            var funcCompare = statementCompare(property);

            switch(property.Name)
            {
                case TYPE:
                    return funcCompare.CompareTo(status.Type.ToPavementConditionStatusType());
                case SEVERITY:
                    return funcCompare.CompareTo(status.Severity.ToPavementConditionStatusSeverity());
            }
            return () => false;
        }
        
        public static PavementConditionStatusType ToPavementConditionStatusType(this string type)
        {
            return Enum.TryParse<PavementConditionStatusType>(type, out var result)
                ? result
                : PavementConditionStatusType.None;
        }

        public static PavementConditionStatusSeverity ToPavementConditionStatusSeverity(this string severity)
        {
            return Enum.TryParse<PavementConditionStatusSeverity>(severity, out var result)
                ? result
                : PavementConditionStatusSeverity.Low;
        }
    }
    public enum PavementConditionStatusSeverity
    {
        Low,
        Medium,
        High
    }
 
    public enum PavementConditionStatusType
    {
        None,
        Bump,
        Pothole
    }
}
