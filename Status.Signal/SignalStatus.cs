// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Status.Common;
using System.Collections.Immutable;

namespace Econolite.Ode.Status.Signal
{
    public class SignalStatus : DeviceCommStatus
    {
        private static readonly ImmutableArray<PreemptState> EmptyPreemptStates;
        private static readonly ImmutableArray<RingStatus> EmptyRingStatuses;
        private static readonly ImmutableArray<RingStatusTermination> EmptyRingStatusTerminations;
        private static readonly ImmutableArray<TspCallStatus2> EmptyTspCallStatus2s;
        static SignalStatus()
        {
            EmptyPreemptStates = (new PreemptState[0]).ToImmutableArray();
            EmptyRingStatuses = (new RingStatus[0]).ToImmutableArray();
            EmptyRingStatusTerminations = (new RingStatusTermination[0]).ToImmutableArray();
            EmptyTspCallStatus2s = (new TspCallStatus2[0]).ToImmutableArray();
        }
        public SignalStatusSource SignalStatusSource { get; set; } = SignalStatusSource.Unknown;

        public uint PollResponseTime { get; set; } = 0;

        public ulong PhaseGreen { get; set; } = 0;
        public ulong PhaseYellow { get; set; } = 0;
        public ulong PhaseRed { get; set; } = 0;
        public ulong PhaseFlash { get; set; } = 0;
        public ulong PhaseNext { get; set; } = 0;
        public ulong Walk { get; set; } = 0;
        public ulong PedClearance { get; set; } = 0;
        public ulong DontWalk { get; set; } = 0;

        public uint OverlapGreen { get; set; } = 0;
        public uint OverlapYellow { get; set; } = 0;
        public uint OverlapRed { get; set; } = 0;
        public uint OverlapFlash { get; set; } = 0;
        public uint OverlapWalk { get; set; } = 0;
        public uint OverlapPedClearance { get; set; } = 0;
        public uint OverlapDontWalk { get; set; } = 0;

        public ulong VehCalls { get; set; } = 0;
        public ulong PedCalls { get; set; } = 0;

        public ushort SystemClock { get; set; } = 0;
        public ushort LocalClock { get; set; } = 0;
        public byte Offset { get; set; } = 0;
        public ShortAlarmStatus ShortAlarmStatus { get; set; } = ShortAlarmStatus.None;
        public UnitControl UnitControl { get; set; } = UnitControl.Unknown;
        public byte SpecialFunctionStatus { get; set; } = 0;

        public ushort AlarmGroup { get; set; } = 0;
        public byte CoordPattern { get; set; } = 0;
        public ulong DetectorActiveStatus { get; set; } = 0;
        public LocalFreeStatus LocalFreeStatus { get; set; } = 0;

        public ushort TspState { get; set; } = 0;
        public UnitAlarmStatus1 UnitAlarmStatus1 { get; set; } = 0;
        public UnitAlarmStatus2 UnitAlarmStatus2 { get; set; } = 0;
        public UnitFlashStatus UnitFlashStatus { get; set; } = UnitFlashStatus.None;
        public ImmutableArray<PreemptState> PreemptStates { get; set; } = EmptyPreemptStates;

        public ImmutableArray<RingStatus> RingStatuses { get; set; } = EmptyRingStatuses;
        public ImmutableArray<RingStatusTermination> RingStatusTerminations { get; set; } = EmptyRingStatusTerminations;
        public ImmutableArray<TspCallStatus2> TspCallStatus2s { get; set; } = EmptyTspCallStatus2s;
    }
}
