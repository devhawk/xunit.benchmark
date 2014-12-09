using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Abstractions;
using Xunit.Sdk;
using System.Threading.Tasks;
using System.Threading;

namespace DevHawk.Xunit
{
    class BenchmarkTestClassRunner : TestClassRunner<BenchmarkTestCase>
    {
        public BenchmarkTestClassRunner(ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<BenchmarkTestCase> testCases, IMessageBus messageBus, ITestCaseOrderer testCaseOrderer, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
            : base(testClass, @class, testCases, messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
        {
        }

        protected override Task<RunSummary> RunTestMethodAsync(ITestMethod testMethod, IReflectionMethodInfo method, IEnumerable<BenchmarkTestCase> testCases, object[] constructorArguments)
        {
            return new BenchmarkTestMethodRunner(testMethod, Class, method, testCases, MessageBus, new ExceptionAggregator(Aggregator), CancellationTokenSource).RunAsync();
        }
    }
}
