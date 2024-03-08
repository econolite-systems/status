using Econolite.Ode.Status.Common;
using System.Collections.Immutable;

namespace Econolite.Ode.Status.Signal
{
    public class AdaptiveStatus : DeviceCommStatus
    {
        private static readonly ImmutableArray<uint> EmptyAdaptiveSplits;
        static AdaptiveStatus()
        {
            EmptyAdaptiveSplits = (new uint[0]).ToImmutableArray();
        }

        public uint AdaptiveCycle { get; set; }
        public uint AdaptiveOffset { get; set; }
        public ImmutableArray<uint> AdaptiveSplits { get; set; } = EmptyAdaptiveSplits;
    }
}
