// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;
using Econolite.Ode.Status.Common;

namespace Econolite.Ode.Status.WrongWayDriver
{
    public class WrongWayDriverEvent : ActionEventStatus
    {
        public double Latitude { get; set; } = 0.0;
        public double Longitude { get; set; } = 0.0;
        public string Location { get; set; } = string.Empty;
    }
}
