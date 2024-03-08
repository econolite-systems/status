// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Serialization;
using Econolite.Ode.Status.Common;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;

namespace Econolite.Ode.Status.Signal
{
    static public class Serializer
    {
        // For the time being we are going to include a encoding type
        // a type/version of 3 indicates we are using a simple encoding
        // Note if for some reason we get to 127 we are going to need
        // encode an extended versioning scheme something a kin to
        // the length encoding in NTCIP
        readonly static byte VERSION = 0x4;
        static public byte[] Serialize(SignalStatus signalStatus)
        {
            var result = new MemoryStream();
            using (var writer = new BinaryWriter(result))
            {
                writer.Write(VERSION);

                writer.Write(Serializers.Guid(signalStatus.DeviceId));
                writer.Write(Serializers.DateTime(signalStatus.TimeStamp));
                writer.Write((byte)signalStatus.CommStatus);
                writer.Write(Serializers.Int32(signalStatus.CommSuccessRate));

                writer.Write(Serializers.ULong(signalStatus.PhaseGreen));
                writer.Write(Serializers.ULong(signalStatus.PhaseYellow));
                writer.Write(Serializers.ULong(signalStatus.PhaseRed));
                writer.Write(Serializers.ULong(signalStatus.PhaseFlash));
                writer.Write(Serializers.ULong(signalStatus.PhaseNext));
                writer.Write(Serializers.ULong(signalStatus.Walk));
                writer.Write(Serializers.ULong(signalStatus.PedClearance));
                writer.Write(Serializers.ULong(signalStatus.DontWalk));

                writer.Write(Serializers.UInt32(signalStatus.OverlapGreen));
                writer.Write(Serializers.UInt32(signalStatus.OverlapYellow));
                writer.Write(Serializers.UInt32(signalStatus.OverlapRed));
                writer.Write(Serializers.UInt32(signalStatus.OverlapFlash));
                writer.Write(Serializers.UInt32(signalStatus.OverlapWalk));
                writer.Write(Serializers.UInt32(signalStatus.OverlapPedClearance));
                writer.Write(Serializers.UInt32(signalStatus.OverlapDontWalk));

                writer.Write(Serializers.ULong(signalStatus.VehCalls));
                writer.Write(Serializers.ULong(signalStatus.PedCalls));

                writer.Write(Serializers.UShort(signalStatus.SystemClock));
                writer.Write(Serializers.UShort(signalStatus.LocalClock));
                writer.Write(signalStatus.Offset);
                writer.Write((byte)signalStatus.ShortAlarmStatus);
                writer.Write((byte)signalStatus.UnitControl);
                writer.Write(signalStatus.SpecialFunctionStatus);

                writer.Write(Serializers.UShort(signalStatus.AlarmGroup));
                writer.Write(signalStatus.CoordPattern);
                writer.Write(Serializers.ULong(signalStatus.DetectorActiveStatus));
                writer.Write((byte)signalStatus.LocalFreeStatus);

                writer.Write(Serializers.UShort(signalStatus.TspState));
                writer.Write((byte)signalStatus.UnitAlarmStatus1);
                writer.Write((byte)signalStatus.UnitAlarmStatus2);
                writer.Write((byte)signalStatus.UnitFlashStatus);

                var numpreemptstates = (byte)(signalStatus?.PreemptStates.Length ?? 0);
                writer.Write(numpreemptstates);
                for (int i = 0; i < numpreemptstates; i++)
                {
                    writer.Write((byte)signalStatus.PreemptStates[i]);
                }

                var numringstatuses = (byte)(signalStatus?.RingStatuses.Length ?? 0);
                writer.Write(numringstatuses);
                for (int i = 0; i < numringstatuses; i++)
                {
                    writer.Write((byte)signalStatus.RingStatuses[i]);
                }

                var numringstatusterminations = (byte)(signalStatus?.RingStatusTerminations.Length ?? 0);
                writer.Write(numringstatusterminations);
                for (int i = 0; i < numringstatusterminations; i++)
                {
                    writer.Write((byte)signalStatus.RingStatusTerminations[i]);
                }

                var numtspcallstatus2s = (byte)(signalStatus?.TspCallStatus2s.Length ?? 0);
                writer.Write(numtspcallstatus2s);
                for (int i = 0; i < numtspcallstatus2s; i++)
                {
                    writer.Write((byte)signalStatus.TspCallStatus2s[i]);
                }

                // Version 3
                writer.Write(Serializers.UInt32(signalStatus.PollResponseTime));

                // Version 4
                writer.Write((byte)signalStatus.SignalStatusSource);

                // Do this last to "fix" the to capacity to the length of the data.
                result.Capacity = (int)result.Length;
            }
            return result.GetBuffer();
        }

        public static byte[] SerializeCollection(SignalStatus[] status)
        {
            return status.SelectMany(s => Serialize(s)).ToArray();
        }

        static public SignalStatus Deserialize(ReadOnlySpan<byte> data)
        {
            SignalStatus result = null;
            var buffer = data.ToArray();
            var bufferstream = new MemoryStream(buffer, false);
            using (var reader = new BinaryReader(bufferstream, Encoding.ASCII))
            {
                var version = reader.ReadByte();
                switch (version)
                {
                    case 1:
                        result = DeserializeV1(reader);
                        break;
                    case 2:
                        result = DeserializeV2(reader);
                        break;
                    case 3:
                        result = DeserializeV3(reader);
                        break;
                    case 4:
                        result = DeserializeV4(reader);
                        break;
                }
            }

            return result;
        }

        public static SignalStatus[] DeserializeCollection(ReadOnlySpan<byte> data)
        {
            var result = new List<SignalStatus>();
            var buffer = data.ToArray();
            var bufferstream = new MemoryStream(buffer, false);
            using (var reader = new BinaryReader(bufferstream, Encoding.ASCII))
            {
                while (bufferstream.Position < bufferstream.Length)
                {
                    if (buffer.Length - bufferstream.Position > 80)
                    {
                        var version = reader.ReadByte();
                        switch (version)
                        {
                            case 1:
                                result.Add(DeserializeV1(reader));
                                break;
                            case 2:
                                result.Add(DeserializeV2(reader));
                                break;
                            case 3:
                                result.Add(DeserializeV3(reader));
                                break;
                            case 4:
                                result.Add(DeserializeV4(reader));
                                break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return result.ToArray();
        }

        private static SignalStatus DeserializeV4(BinaryReader reader)
        {
            var result = DeserializeV3(reader);

            result.SignalStatusSource = (SignalStatusSource)reader.ReadByte();

            return result;
        }

        private static SignalStatus DeserializeV3(BinaryReader reader)
        {
            var result = DeserializeV2(reader);

            result.PollResponseTime = Deserializers.UInt32(reader.ReadBytes(4));

            return result;
        }

        private static SignalStatus DeserializeV2(BinaryReader reader)
        {
            SignalStatus result = new SignalStatus();
            result.DeviceId = Deserializers.Guid(reader.ReadBytes(16));
            result.TimeStamp = Deserializers.DateTime(reader.ReadBytes(8));

            result.CommStatus = (CommStatus)reader.ReadByte();
            result.CommSuccessRate = Deserializers.Int32(reader.ReadBytes(4));

            result.PhaseGreen = Deserializers.ULong(reader.ReadBytes(8));
            result.PhaseYellow = Deserializers.ULong(reader.ReadBytes(8));
            result.PhaseRed = Deserializers.ULong(reader.ReadBytes(8));
            result.PhaseFlash = Deserializers.ULong(reader.ReadBytes(8));
            result.PhaseNext = Deserializers.ULong(reader.ReadBytes(8));
            result.Walk = Deserializers.ULong(reader.ReadBytes(8));
            result.PedClearance = Deserializers.ULong(reader.ReadBytes(8));
            result.DontWalk = Deserializers.ULong(reader.ReadBytes(8));

            result.OverlapGreen = Deserializers.UInt32(reader.ReadBytes(4));
            result.OverlapYellow = Deserializers.UInt32(reader.ReadBytes(4));
            result.OverlapRed = Deserializers.UInt32(reader.ReadBytes(4));
            result.OverlapFlash = Deserializers.UInt32(reader.ReadBytes(4));
            result.OverlapWalk = Deserializers.UInt32(reader.ReadBytes(4));
            result.OverlapPedClearance = Deserializers.UInt32(reader.ReadBytes(4));
            result.OverlapDontWalk = Deserializers.UInt32(reader.ReadBytes(4));

            result.VehCalls = Deserializers.ULong(reader.ReadBytes(8));
            result.PedCalls = Deserializers.ULong(reader.ReadBytes(8));

            result.SystemClock = Deserializers.UShort(reader.ReadBytes(2));
            result.LocalClock = Deserializers.UShort(reader.ReadBytes(2));
            result.Offset = reader.ReadByte();
            result.ShortAlarmStatus = (ShortAlarmStatus)reader.ReadByte();
            result.UnitControl = (UnitControl)reader.ReadByte();
            result.SpecialFunctionStatus = reader.ReadByte();

            result.AlarmGroup = Deserializers.UShort(reader.ReadBytes(2));
            result.CoordPattern = reader.ReadByte();
            result.DetectorActiveStatus = Deserializers.ULong(reader.ReadBytes(8));
            result.LocalFreeStatus = (LocalFreeStatus)reader.ReadByte();

            result.TspState = Deserializers.UShort(reader.ReadBytes(2));
            result.UnitAlarmStatus1 = (UnitAlarmStatus1)reader.ReadByte();
            result.UnitAlarmStatus2 = (UnitAlarmStatus2)reader.ReadByte();
            result.UnitFlashStatus = (UnitFlashStatus)reader.ReadByte();

            var numpreemptstates = reader.ReadByte();
            var preempestates = new PreemptState[numpreemptstates];
            for (int i = 0; i < numpreemptstates; i++)
            {
                preempestates[i] = (PreemptState)reader.ReadByte();
            }
            result.PreemptStates = preempestates.ToImmutableArray();


            var numringstatuses = reader.ReadByte();
            var ringstatuses = new RingStatus[numringstatuses];
            for (int i = 0; i < numringstatuses; i++)
            {
                ringstatuses[i] = (RingStatus)reader.ReadByte();
            }
            result.RingStatuses = ringstatuses.ToImmutableArray();

            var numringstatusterminations = reader.ReadByte();
            var ringstatusterminations = new RingStatusTermination[numringstatusterminations];
            for (int i = 0; i < numringstatusterminations; i++)
            {
                ringstatusterminations[i] = (RingStatusTermination)reader.ReadByte();
            }
            result.RingStatusTerminations = ringstatusterminations.ToImmutableArray();

            var numtspcallstatus2s = reader.ReadByte();
            var tspcallstatus2s = new TspCallStatus2[numtspcallstatus2s];
            for (int i = 0; i < numtspcallstatus2s; i++)
            {
                tspcallstatus2s[i] = (TspCallStatus2)reader.ReadByte();
            }
            result.TspCallStatus2s = tspcallstatus2s.ToImmutableArray();

            return result;
        }

        private static SignalStatus DeserializeV1(BinaryReader reader)
        {
            SignalStatus result = new SignalStatus();
            result.DeviceId = Deserializers.Guid(reader.ReadBytes(16));
            result.TimeStamp = Deserializers.DateTime(reader.ReadBytes(8));

            result.CommStatus = (CommStatus)reader.ReadByte();
            result.CommSuccessRate = Deserializers.Int32(reader.ReadBytes(4));

            result.PhaseGreen = Deserializers.ULong(reader.ReadBytes(8));
            result.PhaseYellow = Deserializers.ULong(reader.ReadBytes(8));
            result.PhaseRed = Deserializers.ULong(reader.ReadBytes(8));
            result.PhaseFlash = Deserializers.ULong(reader.ReadBytes(8));
            result.PhaseNext = Deserializers.ULong(reader.ReadBytes(8));
            result.Walk = Deserializers.ULong(reader.ReadBytes(8));
            result.PedClearance = Deserializers.ULong(reader.ReadBytes(8));
            result.DontWalk = Deserializers.ULong(reader.ReadBytes(8));

            result.OverlapGreen = Deserializers.UInt32(reader.ReadBytes(4));
            result.OverlapYellow = Deserializers.UInt32(reader.ReadBytes(4));
            result.OverlapRed = Deserializers.UInt32(reader.ReadBytes(4));
            result.OverlapFlash = Deserializers.UInt32(reader.ReadBytes(4));
            result.OverlapWalk = Deserializers.UInt32(reader.ReadBytes(4));
            result.OverlapPedClearance = Deserializers.UInt32(reader.ReadBytes(4));
            result.OverlapDontWalk = Deserializers.UInt32(reader.ReadBytes(4));

            result.VehCalls = Deserializers.ULong(reader.ReadBytes(8));
            result.PedCalls = Deserializers.ULong(reader.ReadBytes(8));

            result.SystemClock = Deserializers.UShort(reader.ReadBytes(2));
            result.LocalClock = Deserializers.UShort(reader.ReadBytes(2));
            result.Offset = reader.ReadByte();
            result.ShortAlarmStatus = (ShortAlarmStatus)reader.ReadByte();
            result.UnitControl = (UnitControl)reader.ReadByte();
            result.SpecialFunctionStatus = reader.ReadByte();

            result.AlarmGroup = Deserializers.UShort(reader.ReadBytes(2));
            result.CoordPattern = reader.ReadByte();
            result.DetectorActiveStatus = Deserializers.ULong(reader.ReadBytes(8));
            result.LocalFreeStatus = (LocalFreeStatus)reader.ReadByte();

            //result.TspState = Deserializers.UShort(reader.ReadBytes(2));
            result.UnitAlarmStatus1 = (UnitAlarmStatus1)reader.ReadByte();
            result.UnitAlarmStatus2 = (UnitAlarmStatus2)reader.ReadByte();
            result.UnitFlashStatus = (UnitFlashStatus)reader.ReadByte();

            return result;
        }
    }
}
