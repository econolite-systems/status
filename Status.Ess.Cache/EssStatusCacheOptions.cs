// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Status.Ess.Cache
{
    public class EssStatusCacheOptions
    {
        public TimeSpan StatusTimeout { get; set; } = TimeSpan.FromMinutes(15);
    }
}
