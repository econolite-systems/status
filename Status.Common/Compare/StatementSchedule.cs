// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Common.Compare
{
    public class StatementSchedule
    {
        public string Type { get; set; } = string.Empty;
        public int Times { get; set; } = 0;
        public ScheduleTime In { get; set; } = new ScheduleTime();
    }
}
