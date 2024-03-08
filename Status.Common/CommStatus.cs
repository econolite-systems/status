// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Common
{
    public enum CommStatus : byte
    {
        Unknown,
        Offline,
        Standby,
        Bad,
        BadContent,
        Marginal,
        Good,
    }
}
