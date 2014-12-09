using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Sdk;
using Xunit.Abstractions;
using System.Threading.Tasks;
using System.Threading;

namespace DevHawk.Xunit
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
