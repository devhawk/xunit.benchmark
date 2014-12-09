using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace DevHawk.Xunit
{
    public class BenchmarkDiscoverer : TestFrameworkDiscoverer
    {
        readonly CollectionPerClassTestCollectionFactory testCollectionFactory;

        public BenchmarkDiscoverer(IAssemblyInfo assemblyInfo, ISourceInformationProvider sourceInformationProvider)
            : base(assemblyInfo, sourceInformationProvider, null)
        {
            var testAssembly = new TestAssembly(assemblyInfo, AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            testCollectionFactory = new CollectionPerClassTestCollectionFactory(testAssembly);
        }

        protected override ITestClass CreateTestClass(ITypeInfo @class)
        {
            return new TestClass(testCollectionFactory.Get(@class), @class);
        }

        protected override bool FindTestsForType(ITestClass testClass, bool includeSourceInformation, IMessageBus messageBus)
        {
            foreach (var method in testClass.Class.GetMethods(false))
            {
                var benchmarkAttribute = method.GetCustomAttributes(typeof(BenchmarkAttribute)).FirstOrDefault();
                if (benchmarkAttribute == null)
                    continue;

                var testMethod = new TestMethod(testClass, method);
                var testCase = new BenchmarkTestCase(testMethod);

                if (!ReportDiscoveredTestCase(testCase, includeSourceInformation, messageBus))
                    return false;
            }

            return true;
        }
    }
}
