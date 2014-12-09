using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Sdk;
using Xunit.Abstractions;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;

namespace DevHawk.Xunit
{
    class BenchmarkTestRunner : TestRunner<BenchmarkTestCase>
    {
        public BenchmarkTestRunner(ITest test, IMessageBus messageBus, Type testClass, object[] constructorArguments, MethodInfo testMethod, object[] testMethodArguments, string skipReason, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
            : base(test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, skipReason, aggregator, cancellationTokenSource)
        { }

        protected override async Task<Tuple<decimal, string>> InvokeTestAsync(ExceptionAggregator aggregator)
        {
            var executionTime = await new BenchmarkTestInvoker(Test, MessageBus, TestClass, ConstructorArguments, TestMethod, TestMethodArguments, aggregator, CancellationTokenSource).RunAsync();
            return Tuple.Create(executionTime, string.Empty);
        }
    }
}
