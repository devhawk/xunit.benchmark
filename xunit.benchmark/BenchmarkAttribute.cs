// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Microsoft.Xunit
{
    /// <summary>
    /// Attribute that is applied to a method to indicate that it is a benchmark that should be 
    /// run by the test runner. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class BenchmarkAttribute : Attribute
    {
        /// <summary>
        /// Specifies the number of iterations of the benchmark to run. 
        /// Defaults to 50 if unspecifed.
        /// </summary>
        public int Iterations { get; set; }

        /// <summary>
        /// Specifies if garbage collector should be explicitly run before each benchmark iteration. 
        /// Defaults to false if unspecified.
        /// </summary>
        public bool CollectGarbage { get; set; }
    }
}
