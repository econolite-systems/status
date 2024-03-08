// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;

namespace Econolite.Ode.Status.Signal.Cache
{
    public class SignalStatusCacheOptions
    {
        /// <summary>
        /// At present the StatusTimeout is not used due to the manner that
        /// data is stored in the redis cache. Certainly this is an implementation
        /// detail that is sort of leaking out of the interface.
        /// Leaving it here until we can decided if we want to change the
        /// manner of storing the signal status in the redis cache.
        /// </summary>
        public TimeSpan StatusTimeout { get; set; } = TimeSpan.FromMinutes(15);
    }
}
