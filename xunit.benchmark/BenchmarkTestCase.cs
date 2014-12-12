// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Microsoft.Xunit
{
    [DebuggerDisplay(@"\{ class = {TestMethod.TestClass.Class.Name}, method = {TestMethod.Method.Name}, display = {DisplayName}, skip = {SkipReason} \}")]
    class BenchmarkTestCase : TestMethodTestCase
    {
        public BenchmarkTestCase(ITestMethod testMethod)
            //: base(TestMethodDisplay.ClassAndMethod, testMethod)
            : base(testMethod)
        {
        }
    }
}
