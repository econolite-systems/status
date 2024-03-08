// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;
using System.Collections.Generic;

namespace Econolite.Ode.Status.Common.Compare
{
    public class StatementStatus : Statement
    {
        public Guid SourceId { get; set; }
        public bool IsTriggered { get; set; }
        public IEnumerable<DateTime> Triggered { get; set; } = new List<DateTime>();
    }
}
