// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Microsoft.Xunit
{
    class BenchmarkTestRunner : TestRunner<BenchmarkTestCase>
    {
        int iterations;
        bool collectGarbage;

        public BenchmarkTestRunner(ITest test, IMessageBus messageBus, Type testClass, object[] constructorArguments, MethodInfo testMethod, object[] testMethodArguments, string skipReason, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
            : base(test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, skipReason, aggregator, cancellationTokenSource)
        {
            //retrieve additional info from benchmark attribute
            var benchmarkAttribute = Test.TestCase.TestMethod.Method.GetCustomAttributes(typeof(BenchmarkAttribute)).Single();

            iterations = benchmarkAttribute.GetNamedArgument<int>("Iterations");
            if (iterations <= 0)
            {
                iterations = 50;
            }

            collectGarbage = benchmarkAttribute.GetNamedArgument<bool>("CollectGarbage");
        }

        public int Iterations { get { return iterations; } }

        public bool CollectGargage { get { return collectGarbage; } }

        protected override async Task<Tuple<decimal, string>> InvokeTestAsync(ExceptionAggregator aggregator)
        {
            BenchmarkEventSource.Log.BenchmarkStart(this.DisplayName, Iterations);

            //inject ITracer instance if test method defines single parameter of type ITracer
            var testMethodParams = TestMethod.GetParameters();
            var hasTracerParam = (testMethodParams.Length == 1 && testMethodParams[0].ParameterType == typeof(ITracer));
            var args = hasTracerParam  ? new object[] { NullTracer.Instance } : null;

            //run the test once to make sure it's been jitted
            await new BenchmarkTestInvoker(Test, MessageBus, TestClass, ConstructorArguments, TestMethod, args, aggregator, CancellationTokenSource).RunAsync();

            decimal executionTime = 0;

            //starting from 1 so that iteration number appears correctly in ETW log
            for (int i = 1; i <= Iterations; i++)
            {
                if (CollectGargage)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }

                var stopwatchTracer = new StopwatchTracer();

                BenchmarkEventSource.Log.BenchmarkIterationStart(i);
                var invokerTime = await new BenchmarkTestInvoker(Test, MessageBus, TestClass, ConstructorArguments, TestMethod, hasTracerParam ? new object[] { stopwatchTracer } : null, aggregator, CancellationTokenSource).RunAsync();
                BenchmarkEventSource.Log.BenchmarkIterationStop(i);

                var stopwatchTime = stopwatchTracer.GetElapsed();

                if (stopwatchTime.HasValue)
                {
                    executionTime += stopwatchTime.Value;
                }
                else
                {
                    executionTime += invokerTime;
                }
            }

            //Console.WriteLine("{0} {1} {2}", this.DisplayName, this.iterations, executionTime);

            BenchmarkEventSource.Log.BenchmarkStop(this.DisplayName, iterations);
            return Tuple.Create(executionTime, string.Format("Iterations = {0}, CollectGarbage = {1}", iterations, collectGarbage));
        }
    }
}
