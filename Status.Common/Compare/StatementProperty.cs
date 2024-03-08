// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Common.Compare
{
    public class StatementProperty
    {
        public string Name { get; set; } = string.Empty;
        public string Comparator { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Cast { get; set; } = "string";
    }
}

