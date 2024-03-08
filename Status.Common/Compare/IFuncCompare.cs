// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using System;
namespace Econolite.Ode.Status.Common.Compare
{
    public interface IFuncCompare
    {
        Func<bool> CompareTo(object obj);
    }
}

