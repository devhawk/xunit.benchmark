// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Microsoft.Xunit
{
    class BenchmarkTestCaseRunner : TestCaseRunner<BenchmarkTestCase>
    {
        public BenchmarkTestCaseRunner(BenchmarkTestCase testCase, IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
            : base(testCase, messageBus, aggregator, cancellationTokenSource)
        {
        }

        protected override Task<RunSummary> RunTestAsync()
        {
            var test = new BenchmarkTest(TestCase);
            var testClass = TestCase.TestMethod.TestClass.Class.ToRuntimeType();
            var testMethod = TestCase.TestMethod.Method.ToRuntimeMethod();

            return new BenchmarkTestRunner(test, MessageBus, testClass, null, testMethod, null, null, Aggregator, CancellationTokenSource).RunAsync();
        }
    }
}
