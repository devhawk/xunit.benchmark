// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Microsoft.Xunit
{
    class BenchmarkTestFrameworkDiscoverer : TestFrameworkDiscoverer
    {
        readonly CollectionPerClassTestCollectionFactory testCollectionFactory;

        public BenchmarkTestFrameworkDiscoverer(IAssemblyInfo assemblyInfo, ISourceInformationProvider sourceInformationProvider, IMessageSink diagnosticMessageSink)
            : base(assemblyInfo, sourceInformationProvider, diagnosticMessageSink)
        {
            var testAssembly = new TestAssembly(assemblyInfo, AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            testCollectionFactory = new CollectionPerClassTestCollectionFactory(testAssembly, DiagnosticMessageSink);
        }

        protected override ITestClass CreateTestClass(ITypeInfo @class)
        {
            return new TestClass(testCollectionFactory.Get(@class), @class);
        }

        protected override bool FindTestsForType(ITestClass testClass, bool includeSourceInformation, IMessageBus messageBus, ITestFrameworkDiscoveryOptions discoveryOptions)
        {
            foreach (var method in testClass.Class.GetMethods(false))
            {
                var benchmarkAttribute = method.GetCustomAttributes(typeof(BenchmarkAttribute)).SingleOrDefault();
                if (benchmarkAttribute == null)
                    continue;

                var iterations = benchmarkAttribute.GetNamedArgument<int>("Iterations");
                if (iterations <= 0)
                {
                    iterations = 50;
                }

                var collectGarbage = benchmarkAttribute.GetNamedArgument<bool>("CollectGarbage");

                var testCase = new BenchmarkTestCase(new TestMethod(testClass, method), iterations, collectGarbage);

                if (!ReportDiscoveredTestCase(testCase, includeSourceInformation, messageBus))
                    return false;
            }

            return true;
        }
    }
}
