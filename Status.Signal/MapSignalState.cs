// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;

namespace Econolite.Ode.Status.Signal
{
    public class MapSignalState
    {
        public Guid Id { get; set; }
        public SignalState State { get; set; }
        public DateTime Time { get; set; }
        public SignalStatusSource SignalStatusSource { get; set; }
    }
}
