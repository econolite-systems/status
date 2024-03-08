// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Signal
{
    public enum PreemptState : byte
    {
        Unknown = 0,
        Other = 1,
        NotActive = 2,
        NotActiveWithCall = 3,
        EntryStarted = 4,
        TrackService = 5,
        Dwell = 6,
        LinkActive = 7,
        ExitStarted = 8,
        MaxPresence = 9,
    }
}
