// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;

namespace Econolite.Ode.Status.Common.Compare
{
    public static class StatementExtensions
    {
        public static StatementStatus ToStatus(this Statement statement, Guid sourceId = default, bool triggered = false)
        {
            return new StatementStatus
            {
                Id = statement.Id,
                Type = statement.Type,
                Property = statement.Property,
                Entities = statement.Entities,
                Schedule = statement.Schedule,
                SourceId = sourceId,
                IsTriggered = triggered,
                Triggered = triggered ? new[] {DateTime.UtcNow} : Array.Empty<DateTime>(),
            };
        }
    }
}
