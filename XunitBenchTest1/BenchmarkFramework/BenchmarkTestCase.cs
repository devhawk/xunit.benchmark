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
    class BenchmarkTestCase : TestMethodTestCase
    {
        public BenchmarkTestCase(ITestMethod testMethod) : base(testMethod)
        {
        }

        public Task<RunSummary> RunAsync(IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
        {
            throw new NotImplementedException();
        }

    }
}
