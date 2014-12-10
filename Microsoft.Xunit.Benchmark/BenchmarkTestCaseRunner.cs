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
        ITracer tracer;

        public BenchmarkTestCaseRunner(BenchmarkTestCase testCase, ITracer tracer, IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
            : base(testCase, messageBus, aggregator, cancellationTokenSource)
        {
            this.tracer = tracer;
        }

        protected override Task<RunSummary> RunTestAsync()
        {
            var test = new BenchmarkTest(TestCase);
            var testClass = TestCase.TestMethod.TestClass.Class.ToRuntimeType();
            var testMethod = TestCase.TestMethod.Method.ToRuntimeMethod();

            //inject ITracer instance if test method defines single parameter of type ITracer
            var testMethodParams = testMethod.GetParameters();
            object[] testMethodArgs = (testMethodParams.Length == 1 && testMethodParams[0].ParameterType == typeof(ITracer)) 
                ? new object[] { tracer } : null;

            return new BenchmarkTestRunner(test, MessageBus, testClass, null, testMethod, testMethodArgs, null, Aggregator, CancellationTokenSource).RunAsync();
        }
    }
}
