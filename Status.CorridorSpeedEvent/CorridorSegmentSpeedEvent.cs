using Econolite.Ode.Status.Common;

namespace Econolite.Ode.Status.CorridorSpeedEvent;

public class CorridorSegmentSpeedEvent : ActionEventDeviceStatus
{
    public double SegmentSpeed { get; set; } = 0.0;
    public CommStatus CommStatus { get; set; } = default!;
    public double Latitude { get; set; } = 0.0;
    public double Longitude { get; set; } = 0.0;
    public string Location { get; set; } = string.Empty;
}

