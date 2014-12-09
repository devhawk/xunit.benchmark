using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using XunitBenchTest1;

namespace DevHawk.Xunit
{
    class BenchmarkTestFramework : TestFramework
    {
        protected override ITestFrameworkDiscoverer CreateDiscoverer(IAssemblyInfo assemblyInfo)
        {
            return new BenchmarkDiscoverer(assemblyInfo, SourceInformationProvider);
        }

        protected override ITestFrameworkExecutor CreateExecutor(System.Reflection.AssemblyName assemblyName)
        {
            return new BenchmarkExecutor(assemblyName, SourceInformationProvider);
        }
    }
}
