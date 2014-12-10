// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Microsoft.Xunit
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
