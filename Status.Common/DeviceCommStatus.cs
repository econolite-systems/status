// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Status.Common
{
    public class DeviceCommStatus : DeviceStatus
    {
        public CommStatus CommStatus { get; set; } = CommStatus.Unknown;
        public int CommSuccessRate { get; set; } = 0;
    }
}
