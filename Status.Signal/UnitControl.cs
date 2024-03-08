// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Signal
{
    public enum UnitControl : byte
    {
        Unknown,
        Other = 1,
        SystemControl = 2,
        SystemStandby = 3,
        BackupMode = 4,
        Manual = 5,
        TimeBase = 6,
        Interconnect = 7,
        InterconnectBackup = 8,
    }
}
