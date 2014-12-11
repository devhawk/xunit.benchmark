// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Microsoft.Xunit
{
    public class BenchmarkTestFramework : TestFramework
    {
        protected override ITestFrameworkDiscoverer CreateDiscoverer(IAssemblyInfo assemblyInfo)
        {
            return new BenchmarkTestFrameworkDiscoverer(assemblyInfo, SourceInformationProvider);
        }

        protected override ITestFrameworkExecutor CreateExecutor(System.Reflection.AssemblyName assemblyName)
        {
            return new BenchmarkTestFrameworkExecutor(assemblyName, SourceInformationProvider);
        }
    }
}
