// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;
using Econolite.Ode.Status.Common.Compare;

namespace Econolite.Ode.Status.WrongWayDriver
{
    public static class WrongWayDriverEventExtensions
    {
        public const string WRONG_WAY_DRIVER = "wrongwaydriver";
    
        public static Func<bool> ToFuncCompare( this WrongWayDriverEvent status, StatementProperty property)
        {
            if ( property.Name.ToLower() != WRONG_WAY_DRIVER.ToLower())
            {
                return () => false;
            }
    
            if (bool.Parse(property.Value))
            {
                return () => true;
            }
    
            return () => false;
        }
    }
}

