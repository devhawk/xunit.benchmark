using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;
using Xunit.Abstractions;
using System.Threading;

namespace DevHawk.Xunit
{
    class BenchmarkTestMethodRunner : TestMethodRunner<BenchmarkTestCase>
    {
        public BenchmarkTestMethodRunner(ITestMethod testMethod, IReflectionTypeInfo @class, IReflectionMethodInfo method, IEnumerable<BenchmarkTestCase> testCases, IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
            : base(testMethod, @class, method, testCases, messageBus, aggregator, cancellationTokenSource)
        {
        }

        protected override Task<RunSummary> RunTestCaseAsync(BenchmarkTestCase testCase)
        {
            return new BenchmarkTestCaseRunner(testCase, MessageBus, new ExceptionAggregator(Aggregator), CancellationTokenSource).RunAsync();
            //return testCase.RunAsync(MessageBus, new ExceptionAggregator(Aggregator), CancellationTokenSource);
        }
    }
}
