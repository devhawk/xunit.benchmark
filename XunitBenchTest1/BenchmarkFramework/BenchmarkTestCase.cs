using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace DevHawk.Xunit
{
    class BenchmarkTestCase : TestMethodTestCase
    {
        public BenchmarkTestCase(ITestMethod testMethod) : base(testMethod)
        {
        }
    }
}
