using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace DevHawk.Xunit
{
    [DebuggerDisplay(@"\{ class = {TestMethod.TestClass.Class.Name}, method = {TestMethod.Method.Name}, display = {DisplayName}, skip = {SkipReason} \}")]
    class BenchmarkTestCase : TestMethodTestCase
    {
        int iterations;

        public BenchmarkTestCase(ITestMethod testMethod, int iterations)
            : base(TestMethodDisplay.ClassAndMethod, testMethod)
        {
            this.iterations = iterations;
        }

        public int Iterations { get { return iterations; } }

        public async Task<RunSummary> RunAsync(IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
        {
            RunSummary summary = new RunSummary();
            for (int i = 0; i < Iterations; i++)
                summary.Aggregate(await new BenchmarkTestCaseRunner(this, messageBus, new ExceptionAggregator(aggregator), cancellationTokenSource).RunAsync());

            return new RunSummary()
            {
                Failed = (summary.Failed > 0 || summary.Total != Iterations) ? 1 : 0,
                Skipped = (summary.Skipped > 0) ? 1 : 0,
                Time = summary.Time,
                Total = 1,
            };
        }
    }
}
