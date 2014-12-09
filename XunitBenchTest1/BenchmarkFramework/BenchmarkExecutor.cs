using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace DevHawk.Xunit
{
    class BenchmarkExecutor : TestFrameworkExecutor<BenchmarkTestCase>
    {
        public BenchmarkExecutor(AssemblyName assemblyName, ISourceInformationProvider sourceInformationProvider)
            : base(assemblyName, sourceInformationProvider)
        {
        }

        protected override ITestFrameworkDiscoverer CreateDiscoverer()
        {
            throw new NotImplementedException();
        }

        protected override void RunTestCases(IEnumerable<BenchmarkTestCase> testCases, IMessageSink messageSink, ITestFrameworkOptions executionOptions)
        {
            //throw new NotImplementedException();
        }
    }
}
