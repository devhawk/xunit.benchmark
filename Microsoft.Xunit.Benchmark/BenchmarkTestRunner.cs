// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Microsoft.Xunit
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
