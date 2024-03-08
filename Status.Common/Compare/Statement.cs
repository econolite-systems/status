// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;
using System.Collections.Generic;

namespace Econolite.Ode.Status.Common.Compare
{
    public class Statement
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Type { get; set; } = string.Empty;
        public StatementProperty Property { get; set; } = new StatementProperty();
        public IEnumerable<string> Entities { get; set; } = Array.Empty<string>();
        public StatementSchedule Schedule { get; set; } = new StatementSchedule();
    }
}
