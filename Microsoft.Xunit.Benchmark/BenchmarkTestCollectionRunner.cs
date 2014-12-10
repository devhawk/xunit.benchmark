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
    class BenchmarkTestCollectionRunner : TestCollectionRunner<BenchmarkTestCase>
    {
        public BenchmarkTestCollectionRunner(ITestCollection testCollection, IEnumerable<BenchmarkTestCase> testCases, IMessageBus messageBus, ITestCaseOrderer testCaseOrderer, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
            :base(testCollection, testCases, messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
        {
        }

        protected override Task<RunSummary> RunTestClassAsync(ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<BenchmarkTestCase> testCases)
        {
            return new BenchmarkTestClassRunner(testClass, @class, testCases, MessageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), CancellationTokenSource).RunAsync();
        }
    }
}
