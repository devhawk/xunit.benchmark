using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace DevHawk.Xunit
{
    class BenchmarkTest : LongLivedMarshalByRefObject, ITest
    {
        BenchmarkTestCase testCase;

        public BenchmarkTest(BenchmarkTestCase testCase)
        {
            this.testCase = testCase;
        }

        public string DisplayName
        {
            get { return testCase.DisplayName; }
        }

        public ITestCase TestCase
        {
            get { return testCase; }
        }
    }
}
