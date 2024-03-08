// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Status.Common;

namespace Econolite.Ode.Status.Speed
{
    public class SpeedEvent : ActionEventDeviceStatus
    {
        public int SegmentId { get; set; }
        public double SegmentSpeed { get; set; } = 0.0;
        public CommStatus CommStatus { get; set; } = default!;
        public TrafficStatus TrafficStatus { get; set; } = TrafficStatus.Normal;
        public double Latitude { get; set; } = 0.0;
        public double Longitude { get; set; } = 0.0;
        public double [][] PolylineCoordinates { get; set; } = default!;
        public string Location { get; set; } = string.Empty;
    }
}
