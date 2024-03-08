// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Status.Signal.Messaging
{
    public class PollingStatusMessage
    {
        public string Type { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
    }
}
