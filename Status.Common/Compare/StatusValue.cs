// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;

namespace Econolite.Ode.Status.Common.Compare
{
    public class TrueFuncCompare : IFuncCompare
    {
        public Func<bool> CompareTo(object obj)
        {
            return () => true;
        }
    }

    public class FalseFuncCompare : IFuncCompare
    {
        public Func<bool> CompareTo(object obj)
        {
            return () => false;
        }
    }

    public class StatusValue<T> : IFuncCompare
    {
        private readonly StatementProperty _property;

        public StatusValue(StatementProperty property)
        {
            _property = property ?? throw new ArgumentNullException(nameof(property));
        }

        public Func<bool> CompareTo(object value)
        {
            var currentType = typeof(T);

            if (currentType == typeof(int))
            {
                return () => CompareInt((int)value);
            }
            else if (currentType == typeof(bool))
            {
                return () => CompareBool((bool)value);
            }

            return () => CompareString((string)value);
        }

        private bool CompareInt(int value)
        {
            // ['=', '!=', '>', '>=', '<', '<=']
            var statementValue = int.Parse(_property.Value);
            switch(_property.Comparator)
            {
                case "=":
                    return value == statementValue;
                case "!=":
                    return value != statementValue;
                case ">":
                    return value > statementValue;
                case ">=":
                    return value >= statementValue;
                case "<":
                    return value < statementValue;
                case "<=":
                    return value <= statementValue;
            }

            return false;
        }

        private bool CompareString(string value)
        {
            // ['=', '!=']
            var statementValue = _property.Value;
            switch (_property.Comparator)
            {
                case "=":
                    return value.ToLower() == statementValue.ToLower();
                case "!=":
                    return value.ToLower() != statementValue.ToLower();
            }

            return false;
        }

        private bool CompareBool(bool value)
        {
            var statementValue = bool.Parse(_property.Value);
            switch (_property.Comparator)
            {
                case "=":
                    return value == statementValue;
                case "!=":
                    return value != statementValue;
            }

            return false;
        }
    }

    public class EnumValue<T> : IFuncCompare
    {
        private readonly StatementProperty _property;

        public EnumValue(StatementProperty property)
        {
            _property = property ?? throw new ArgumentNullException(nameof(property));
        }

        public Func<bool> CompareTo(object obj)
        {
            var value = (T)obj;
            var status = Enum.GetName(typeof(T), value);

            return () => CompareString(status);
        }

        private bool CompareString(string value)
        {
            // ['=', '!=']
            var statementValue = _property.Value;
            switch (_property.Comparator)
            {
                case "=":
                    return value.ToLower() == statementValue.ToLower();
                case "!=":
                    return value.ToLower() != statementValue.ToLower();
            }

            return false;
        }
    }

    public class DateTimeValue : IFuncCompare
    {
        private readonly StatementProperty _property;

        public DateTimeValue(StatementProperty property)
        {
            _property = property ?? throw new ArgumentNullException(nameof(property));
        }

        public Func<bool> CompareTo(object obj)
        {
            var value = (DateTime)obj;

            return () => Compare(value);
        }

        private bool Compare(DateTime value)
        {
            // ['=', '!=']
            var statementValue = DateTime.Parse(_property.Value);
            switch (_property.Comparator)
            {
                case "=":
                    return value == statementValue;
                case "!=":
                    return value != statementValue;
            }

            return false;
        }
    }
}

