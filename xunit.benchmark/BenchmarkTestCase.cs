// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Microsoft.Xunit
{
    [DebuggerDisplay(@"\{ class = {TestMethod.TestClass.Class.Name}, method = {TestMethod.Method.Name}, display = {DisplayName}, skip = {SkipReason} \}")]
    class BenchmarkTestCase : TestMethodTestCase
    {
        public int Iterations { get; private set; }
        public bool CollectGargage { get; private set; }

        public BenchmarkTestCase()
        {
        }

        public BenchmarkTestCase(ITestMethod testMethod, int iterations, bool collectGarbage)
            : base(TestMethodDisplay.ClassAndMethod, testMethod)
        {
            Iterations = iterations;
            CollectGargage = collectGarbage;

            Traits.Add("Iterations", new List<string>() { Iterations.ToString() });
            Traits.Add("CollectGargage", new List<string>() { CollectGargage.ToString() });
        }
    }
}
