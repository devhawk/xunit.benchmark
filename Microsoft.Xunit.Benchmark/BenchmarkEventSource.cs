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
        public const EventTask Benchmark = (EventTask)1;
        public const EventTask BenchmarkIteration = (EventTask)2;
        public const EventTask BenchmarkTracer = (EventTask)3;

        [Event(1, Opcode = EventOpcode.Start, Task = Benchmark)]
        public void StartBenchmark(string benchmarkName, int iterations) { WriteEvent(1, benchmarkName, iterations); }

        [Event(2, Opcode = EventOpcode.Stop, Task = Benchmark)]
        public void EndBenchmark(string benchmarkName, int iterations) { WriteEvent(2, benchmarkName, iterations); }

        [Event(3, Opcode = EventOpcode.Start, Task = BenchmarkIteration)]
        public void StartBenchmarkIteration(int iteration) { WriteEvent(3, iteration); }

        [Event(4, Opcode = EventOpcode.Stop, Task = BenchmarkIteration)]
        public void EndBenchmarkIteration(int iteration) { WriteEvent(4, iteration); }

        [Event(5, Opcode = EventOpcode.Start, Task = BenchmarkTracer)]
        public void StartBenchmarkTracer() { WriteEvent(5); }

        [Event(6, Opcode = EventOpcode.Stop, Task = BenchmarkTracer)]
        public void EndBenchmarkTracer() { WriteEvent(6); }

        public static BenchmarkEventSource Log = new BenchmarkEventSource();
    }
}
