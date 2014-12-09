using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace DevHawk.Xunit
{
    class BenchmarkTestAssemblyRunner : TestAssemblyRunner<BenchmarkTestCase>
    {
        public BenchmarkTestAssemblyRunner(ITestAssembly testAssembly, IEnumerable<BenchmarkTestCase> testCases, IMessageSink messageSink, ITestFrameworkOptions executionOptions)
            : base(testAssembly, testCases, messageSink, executionOptions)
        {
        }

        protected override string GetTestFrameworkDisplayName()
        {
            return "DevHawk.Xunit.BenchmarkFramework";
        }

        protected override Task<RunSummary> RunTestCollectionAsync(IMessageBus messageBus, ITestCollection testCollection, IEnumerable<BenchmarkTestCase> testCases, CancellationTokenSource cancellationTokenSource)
        {
            return new BenchmarkTestCollectionRunner(testCollection, testCases, messageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), cancellationTokenSource).RunAsync();
        }
    }
}
