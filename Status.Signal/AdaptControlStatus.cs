using System;

namespace Econolite.Ode.Status.Signal
{
    [Flags]
    public enum AdaptControlStatus : byte
    {
        None = 0x0,
        SplitsPending = 0x1,
        OffsetPending = 0x2,
        CyclePending = 0x4,
        SplitsInEffect = 0x10,
        OffsetInEffect = 0x20,
        CycleInEffect = 0x40,
        CancelPending = 0x80,
    }
}
