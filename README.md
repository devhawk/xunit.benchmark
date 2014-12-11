# xunit.benchmark

Benchmarking extension for [xunit.net](http://xunit.github.io/)

## Example

```csharp
using Xunit;
using Microsoft.Xunit;

// xunit.benchmark is implemented as an xunit.net 2.0 custom test frameworks. 
// This assembly attribute enables xunit.benchmark for this assembly
[assembly: TestFramework("Microsoft.Xunit.BenchmarkTestFramework", "Microsoft.Xunit.Benchmark")]

public class ListInsertBenchmark
{
    // xunit.benchmark uses [Benchmark] instead of xunit's typical [Fact]
    // [Benchmark] supports the following named parameters:
    //   Iterations -     Specifies the number of iterations of the benchmark to run. 
    //                    Defaults to 50 if unspecifed.
    //   CollectGarbage - Specifies if garbage collector should be explicitly run before
    //                    each benchmark iteration. Defaults to false if unspecified.
    [Benchmark]
    public void CapacityInsertFiveHundred(ITracer tracer) // Benchmarks optionally take an 
                                                          // ITracer parameter to demark the 
                                                          // specific part of the method to test
    {
        // setup all instances needed as part of the benchmark
        const int count = 500;
        var ls = new List<int>(count);

        // Call ITracer.Trace to indicate the portion of the method to benchmark.
        // ITracer.Trace returns an IDisposable so you can put the portion of the 
        // method to benchmark inside a using statement
        using (tracer.Trace())
        {
            for (int i = 0; i < count; i++)
            {
                ls.Add(i);
            }
        }

        // if there is any cleanup for the benchmark, it goes after the Trace using statement
    }
}

```
