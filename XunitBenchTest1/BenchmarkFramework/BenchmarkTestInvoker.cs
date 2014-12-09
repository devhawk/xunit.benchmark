using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace DevHawk.Xunit
{
    class BenchmarkTestInvoker : TestInvoker<BenchmarkTestCase>
    {
        public BenchmarkTestInvoker(ITest test, IMessageBus messageBus, Type testClass, object[] constructorArguments, MethodInfo testMethod, object[] testMethodArguments, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
            : base(test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, aggregator, cancellationTokenSource)
        {
        }
    }
}
