// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;

namespace Microsoft.Xunit
{
    class StopwatchTracer : ITracer
    {
        Stopwatch stopwatch;

        class Disposer : IDisposable
        {
            StopwatchTracer tracer;

            public Disposer(StopwatchTracer tracer)
            {
                this.tracer = tracer;
            }

            public void Dispose()
            {
                this.tracer.stopwatch.Stop();
            }
        }

        public IDisposable Trace()
        {
            var disposer = new Disposer(this);
            stopwatch = Stopwatch.StartNew();
            return disposer;
        }

        public TimeSpan GetElapsed()
        {
            if (stopwatch == null)
                return TimeSpan.Zero;

            Debug.Assert(!stopwatch.IsRunning);
            return stopwatch.Elapsed;
        }
    }
}
