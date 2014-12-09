using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace DevHawk.Xunit
{
    class BenchmarkTestFrameworkExecutor : TestFrameworkExecutor<BenchmarkTestCase>
    {
        public BenchmarkTestFrameworkExecutor(AssemblyName assemblyName, ISourceInformationProvider sourceInformationProvider)
            : base(assemblyName, sourceInformationProvider)
        {
        }

        protected override ITestFrameworkDiscoverer CreateDiscoverer()
        {
            return new BenchmarkTestFrameworkDiscoverer(AssemblyInfo, SourceInformationProvider);
        }

        protected override async void RunTestCases(IEnumerable<BenchmarkTestCase> testCases, IMessageSink messageSink, ITestFrameworkOptions executionOptions)
        {
            var testAssembly = new TestAssembly(AssemblyInfo, AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            using (var assemblyRunner = new BenchmarkTestAssemblyRunner(testAssembly, testCases, messageSink, executionOptions))
            {
                await assemblyRunner.RunAsync();
            }
        }

    }
}
