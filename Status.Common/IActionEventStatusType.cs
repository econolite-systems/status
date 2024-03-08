// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System.Threading.Tasks;

namespace Econolite.Ode.Status.Common
{
    public interface IActionEventStatusType
    {
        string Type { get; }
        Task ProcessAsync(ActionEventStatus actionEventStatus);
    }
}
