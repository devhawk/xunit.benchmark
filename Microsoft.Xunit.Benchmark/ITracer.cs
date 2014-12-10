// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Microsoft.Xunit
{
    /// <summary>
    /// Represents a benchmark tracer. 
    /// </summary>
    public interface ITracer
    {
        /// <summary>
        /// Call at the start of the portion of the benchmark method under test.
        /// </summary>
        /// <returns>
        /// IDisposable object to dispose when the portion of the benchmark under test is complete
        /// </returns>
        IDisposable Trace();
    }

}
