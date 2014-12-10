// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Microsoft.Xunit
{
    class BenchmarkTestFrameworkExecutor : TestFrameworkExecutor<BenchmarkTestCase>
    {
        public BenchmarkTestFrameworkExecutor(AssemblyName assemblyName, ISourceInformationProvider sourceInformationProvider)
            : base(assemblyName, sourceInformationProvider)
        {
        }

        protected override ITestFrameworkDiscoverer CreateDiscoverer()
        {
            return new BenchmarkTestFrameworkDiscoverer(AssemblyInfo, SourceInformationProvider);
        }

        protected override async void RunTestCases(IEnumerable<BenchmarkTestCase> testCases, IMessageSink messageSink, ITestFrameworkOptions executionOptions)
        {
            var testAssembly = new TestAssembly(AssemblyInfo, AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            using (var assemblyRunner = new BenchmarkTestAssemblyRunner(testAssembly, testCases, messageSink, executionOptions))
            {
                await assemblyRunner.RunAsync();
            }
        }

    }
}
