// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Microsoft.Xunit
{
    class NullTracer : ITracer
    {
        private NullTracer() { }

        class Disposer : IDisposable
        {
            public void Dispose()
            {
            }
        }

        static readonly Disposer nullDisposable = new Disposer();

        public IDisposable Trace()
        {
            return nullDisposable;
        }

        public static readonly NullTracer Instance = new NullTracer();
    }
}
