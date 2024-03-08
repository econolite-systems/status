// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Serialization.Test;
using Econolite.Ode.Status.Common;
using Econolite.Ode.Status.Signal;
using FluentAssertions;
using System;
using System.Collections.Immutable;
using Xunit;

namespace Signal.Test
{
    public class SerializerTest
    {
        [Fact]
        void Deserialize()
        {
            var rawbuffer = ByteStreamHelper.Convert("01 7A 6D F5 F7 FD 75 85 46 93 51 6B DA FF E5 86 97 88 D6 3A 97 98 63 9B 7E 06 00 00 00 64 00 00 00 00 00 00 00 22 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 DD 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 88 00 00 00 00 00 00 00 00 00 00 00 00 00 00 03 00 00 00 FE 00 00 00 00 00 00 00 00 03 40 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");

            var testresults = Serializer.Deserialize(rawbuffer);

            testresults.DeviceId.Should().Be(Guid.Parse("f7f56d7a-75fd-4685-9351-6bdaffe58697"));
        }

        [Fact]
        void RoundTripEmpty()
        {
            var orginalstatus = new SignalStatus();
            var serialized = Serializer.Serialize(orginalstatus);

            var deserializedstatus = Serializer.Deserialize(serialized);

            deserializedstatus.Should().BeEquivalentTo(orginalstatus);
        }

        [Fact]
        void RoundTripWithData()
        {
            var orginalstatus = new SignalStatus
            {
                DeviceId = Guid.Parse("f7f56d7a-75fd-4685-9351-6bdaffe58697"),
                TimeStamp = DateTime.Parse("1/25/2019"),
                AlarmGroup = 0x00488,
                CommStatus = CommStatus.Good,
                CommSuccessRate = 37,
                CoordPattern = 12,
                DetectorActiveStatus = 0x6728959,
                DontWalk = 0x58762,
                LocalClock = 7,
                LocalFreeStatus = LocalFreeStatus.CoordFree,
                Offset = 18,
                OverlapDontWalk = 0x5762,
                OverlapFlash = 0x784623,
                OverlapGreen = 0x3456,
                OverlapPedClearance = 0x5763,
                OverlapRed = 0x4765,
                OverlapWalk = 0x72565,
                OverlapYellow = 0x583671,
                PedCalls = 0x348A,
                PedClearance = 0xffff,
                PhaseFlash = 0x1,
                PhaseGreen = 0x583B,
                PhaseNext = 0x22,
                PhaseRed = 0x58976,
                PhaseYellow = 0x57834,
                PollResponseTime = 1000,
                PreemptStates = (new PreemptState[]
                {
                    PreemptState.EntryStarted,
                    PreemptState.NotActive,
                    PreemptState.NotActiveWithCall,
                }).ToImmutableArray(),
                RingStatuses = (new RingStatus[]
                {
                    RingStatus.GreenRest,
                    RingStatus.MinGreen,
                    RingStatus.Undefined,
                    RingStatus.Undefined,
                }).ToImmutableArray(),
                RingStatusTerminations = (new RingStatusTermination[]
                {
                    RingStatusTermination.ForceOff,
                    RingStatusTermination.GapOut,
                    RingStatusTermination.MaxOut,
                    RingStatusTermination.Unknown,
                }).ToImmutableArray(),
                ShortAlarmStatus = ShortAlarmStatus.LocalZero,
                SpecialFunctionStatus = 0x20,
                SystemClock = 68,
                TspCallStatus2s = (new TspCallStatus2[]
                {
                    TspCallStatus2.Disabled,
                    TspCallStatus2.CallInhibited,
                    TspCallStatus2.CallBeingServed,
                }).ToImmutableArray(),
                TspState = 0x8674,
                UnitAlarmStatus1 = UnitAlarmStatus1.CoordActive,
                UnitAlarmStatus2 = UnitAlarmStatus2.LowBattery,
                UnitControl = UnitControl.BackupMode,
                UnitFlashStatus = UnitFlashStatus.Automatic,
                VehCalls = 0x54873,
                Walk = 0x58673F,
                SignalStatusSource = SignalStatusSource.DirectPolling,
            };
            var serialized = Serializer.Serialize(orginalstatus);

            serialized.Length.Should().Be(186);

            var deserializedstatus = Serializer.Deserialize(serialized);

            deserializedstatus.Should().BeEquivalentTo(orginalstatus);
        }
    }
}
