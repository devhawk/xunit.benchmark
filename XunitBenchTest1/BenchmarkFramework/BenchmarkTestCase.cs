using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace DevHawk.Xunit
{
    [DebuggerDisplay(@"\{ class = {TestMethod.TestClass.Class.Name}, method = {TestMethod.Method.Name}, display = {DisplayName}, skip = {SkipReason} \}")]
    class BenchmarkTestCase : TestMethodTestCase
    {
        int iterations;

        public BenchmarkTestCase(ITestMethod testMethod, int iterations)
            : base(TestMethodDisplay.ClassAndMethod, testMethod)
        {
            this.iterations = iterations;
        }

        public int Iterations { get { return iterations; } }
    }
}
