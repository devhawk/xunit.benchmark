// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Microsoft.Xunit
{
    class BenchmarkTestAssemblyRunner : TestAssemblyRunner<BenchmarkTestCase>
    {
        public BenchmarkTestAssemblyRunner(ITestAssembly testAssembly, IEnumerable<BenchmarkTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageSink messageSink, ITestFrameworkExecutionOptions executionOptions)
            : base(testAssembly, testCases, diagnosticMessageSink, messageSink, executionOptions)
        {
        }

        protected override string GetTestFrameworkDisplayName()
        {
            return "Microsoft.Xunit.BenchmarkFramework";
        }

        protected override Task<RunSummary> RunTestCollectionAsync(IMessageBus messageBus, ITestCollection testCollection, IEnumerable<BenchmarkTestCase> testCases, CancellationTokenSource cancellationTokenSource)
        {
            return new BenchmarkTestCollectionRunner(testCollection, testCases, DiagnosticMessageSink, messageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), cancellationTokenSource).RunAsync();
        }
    }
}
