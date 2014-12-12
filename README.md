# Performance Testing with xUnit.benchmark

xUnit.benchmark is a custom test framework for [xUnit.net](http://xunit.github.io/).
It enables you to execute performance benchmarks using the xUnit.net test
runner(s).

## Quickstart

### Step One: Add TestFramework Attribute

In order to use a custom test framework in xUnit.net, you need to specify the
assembly level [TestFramework] attribute. The [TestFramework] attribute tells
the xUnit.net test runner that you are using a custom test framework in this
assembly. The parameters on the [TestFramework] specify the type and assembly
name for the custom test framework used by the assembly.

Add the following line to the AssemblyInfo.cs file in your test project to enable
xUnit.benchmark.

```csharp
[assembly: Xunit.TestFramework("Microsoft.Xunit.BenchmarkTestFramework", "Xunit.Benchmark")]
```

### Step Two: Add Benchmarks

xUnit.benchmark tests are indicated with the [Benchmark] attribute, similarly to
how traditional xUnit tests use [Fact] or [Theory]. The [Benchmark] attribute is
in the Microsoft.Xunit namespace, so you probably want to add a using statement
at the top of your each of your benchmark code files.

Note, you cannot mix [Benchmark] tests with [Fact] or [Theory] tests in the
same assembly.

```csharp
using Microsoft.Xunit;

[Benchmark]
public void SampleBenchmark()
{
    // code to benchmark here
}
```

### Step Three: Add Tracer Support (optional)

Benchmarks optionally support tracing to demark the specific part of the method
to time. This is useful if your test has time-consuming setup or teardown
operations that you don't want to include in the test execution time.

To add tracer support, declare a single parameter in your benchmark method of
type [ITracer](https://github.com/devhawk/xunit.benchmark/blob/master/xunit.benchmark/ITracer.cs).
ITracer has a single method Trace that you call right before the code you want
to benchmark. Trace returns an IDisposable which you dispose right after the
code you want to benchmark. This pattern makes it easy to wrap the code you want
to benchmark in a using(tracer.Trace()) block.

```csharp
[Benchmark]
public void SampleBenchmarkWithTracingSupport(ITracer tracer)
{
    // time consuming setup here

    using (tracer.Trace())
    {
        // code to benchmark here
    }

    // time consuming teardown here
}
```

### Step Four: Customize Benchmark Execution (optional)

[Benchmark] supports the following named parameters:
 * Iterations: Specifies the number of iterations of the benchmark to run.
 Defaults to 50 if unspecifed.
 * CollectGarbage: Specifies if garbage collector should be explicitly run
 before each benchmark iteration. Defaults to false if unspecified.

```csharp
[Benchmark(Iterations=100, CollectGarbage=true)]
public void SampleBenchmark()
{
}
```

### Step Five: Execute Benchmarks
Benchmarks are executed using the same test runner as normal xUnit.net tests.

```
> xunit.console MyBenchmarkAssembly.dll -XML benchmarkResults.xml
```

Note: There is a PR out for the main xUnit.net project to improve the precision
of execution time emitted in the test results file when using the -XML option.
Additionally, this PR updates the test result file to include custom information
about the benchmark run (iterations executed, was garbage collected, etc.)

### Step Six: ETW Support

In addition to typical stopwatch performance testing, xunit.benchmark raises
custom ETW events at the start/end of every benchmark, benchmark iteration and
when benchmark tracing starts and stops. This allows you to use tools such as
[PerfView](http://channel9.msdn.com/Series/PerfView-Tutorial) to get more
detailed performance information about the benchmarks, including context switches,
garbage collection and even hardware counters.

TODO: add PerfView benchmarking tutorial.
