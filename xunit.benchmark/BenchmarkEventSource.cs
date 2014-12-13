using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xunit
{
    [EventSource(Name = "Microsoft-Xunit-Benchmark")]
    public sealed class BenchmarkEventSource : EventSource
    {
        public class Tasks
        {
            public const EventTask Benchmark = (EventTask)1;
            public const EventTask BenchmarkIteration = (EventTask)2;
            public const EventTask BenchmarkTrace = (EventTask)3;
        }

        [Event(1, Opcode = EventOpcode.Start, Task = Tasks.Benchmark)]
        public void BenchmarkStart(string BenchmarkName, int Iterations) { WriteEvent(1, BenchmarkName, Iterations); }

        [Event(2, Opcode = EventOpcode.Stop, Task = Tasks.Benchmark)]
        public void BenchmarkStop(string BenchmarkName, int Iterations) { WriteEvent(2, BenchmarkName, Iterations); }

        [Event(3, Opcode = EventOpcode.Start, Task = Tasks.BenchmarkIteration)]
        public void BenchmarkIterationStart(int Iteration) { WriteEvent(3, Iteration); }

        [Event(4, Opcode = EventOpcode.Stop, Task = Tasks.BenchmarkIteration)]
        public void BenchmarkIterationStop(int Iteration) { WriteEvent(4, Iteration); }

        [Event(5, Opcode = EventOpcode.Start, Task = Tasks.BenchmarkTrace)]
        public void BenchmarkTraceStart() { WriteEvent(5); }

        [Event(6, Opcode = EventOpcode.Stop, Task = Tasks.BenchmarkTrace)]
        public void BenchmarkTraceStop() { WriteEvent(6); }

        public static BenchmarkEventSource Log = new BenchmarkEventSource();
    }
}
