using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHawk.Xunit
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
