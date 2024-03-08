// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.

using Econolite.Ode.Status.Common.Compare;

namespace Econolite.Ode.Status.Speed
{
    public static class SpeedEventExtensions
    {
        public const string SPEED_EVENT = "speedevent";
        public static Func<StatementProperty, IFuncCompare> IntStatusValueFuncCompare = (property) => new StatusValue<int>(property);
        private static Dictionary<string, Func<StatementProperty, IFuncCompare>> _comparison = new Dictionary<string, Func<StatementProperty, IFuncCompare>>();

        static SpeedEventExtensions()
        {

            _comparison.Add(SPEED_EVENT, IntStatusValueFuncCompare);
        }

        public static Func<bool> ToFuncCompare(this SpeedEvent status, StatementProperty property)
        {

            if (property.Name.ToLower() != SPEED_EVENT.ToLower())
            {
                return () => false;
            }

            var funcCompare = IntStatusValueFuncCompare(property);
            return funcCompare.CompareTo((int)status.SegmentSpeed);

        }

        private static Func<StatementProperty, IFuncCompare> GetFuncCompare(string name)
        {
            if (_comparison.TryGetValue(name, out var result))
            {
                return result;
            }

            return (property) => new FalseFuncCompare();
        }
    }
}