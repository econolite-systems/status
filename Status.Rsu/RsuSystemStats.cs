// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;
using System.Collections.Generic;
using Econolite.Ode.Status.Common;
using Econolite.Ode.Status.Common.Compare;

namespace Econolite.Ode.Status.Rsu
{
    public class RsuSystemStats : ActionEventDeviceStatus
    {
        public bool IsConnected { get; set; }
        public string Error { get; set; } = string.Empty;
        public TimeSpan TimeSincePowerOn { get; set; }
        public TimeSpan TotalRunTime { get; set; }
        public DateTime LastRestartTime { get; set; }
        public string MibVersion { get; set; }
        public string FirmwareVersion { get; set; }
        public string LocationDescription { get; set; }
        public string RsuId { get; set; }
        public string Manufacturer { get; set; }
        public ChanStatus ChannelStatus { get; set; } = ChanStatus.NoneOp;
    }

    public enum ChanStatus
    {
        BothOp,
        AltOp,
        ContOp,
        NoneOp
    }
    
    public static class RsuSystemStatsExtensions
    {
        private const string IS_CONNECTED = "isConnected";

        private static Func<StatementProperty, IFuncCompare> BoolStatusValueFuncCompare = (property) => new StatusValue<bool>(property);

        private static Dictionary<string, Func<StatementProperty, IFuncCompare>> _comparison = new Dictionary<string, Func<StatementProperty,IFuncCompare>>();
        
        static RsuSystemStatsExtensions()
        {
            _comparison.Add(IS_CONNECTED, BoolStatusValueFuncCompare);
        }
        
        private static Func<StatementProperty, IFuncCompare> GetFuncCompare(string name)
        {
            if (_comparison.TryGetValue(name, out var result))
            {
                return result;
            }

            return (property) => new FalseFuncCompare(); 
        }
        
        public static Func<bool> ToFuncCompare(this RsuSystemStats status, StatementProperty property)
        {
            var statementCompare = GetFuncCompare(property.Name);
            var funcCompare = statementCompare(property);
            
            if (IS_CONNECTED == property.Name)
            {
                return funcCompare.CompareTo(status.IsConnected);
            }

            return () => false;
        }
    }
}
