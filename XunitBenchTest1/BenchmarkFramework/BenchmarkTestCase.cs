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
            //run the test once to make sure it's been jitted
            RunSummary summary = await new BenchmarkTestCaseRunner(this, NullTracer.Instance, messageBus, new ExceptionAggregator(aggregator), cancellationTokenSource).RunAsync();

            TimeSpan totalTime = TimeSpan.Zero;

            for (int i = 0; i < Iterations; i++)
            {
                var tracer = new StopwatchTracer();
                summary.Aggregate(await new BenchmarkTestCaseRunner(this, tracer, messageBus, new ExceptionAggregator(aggregator), cancellationTokenSource).RunAsync());
                totalTime += tracer.GetElapsed();
            }

            //Console.WriteLine("{0} {1} {2}", this.DisplayName, this.iterations, totalTime);

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
