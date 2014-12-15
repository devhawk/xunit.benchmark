# Performance Testing with xUnit.benchmark

xUnit.benchmark is a custom test framework for 
[xUnit.net](http://xunit.github.io/).
It enables you to execute performance benchmarks using the xUnit.net test
runner(s).

## Quickstart

### Step One: Add TestFramework Attribute

In order to use a custom test framework in xUnit.net, you need to specify the
assembly level [TestFramework] attribute. The [TestFramework] attribute tells
the xUnit.net test runner that you are using a custom test framework in this
assembly. The parameters on the [TestFramework] specify the type and assembly
name for the custom test framework used by the assembly.

Add the following line to the AssemblyInfo.cs file in your test project to 
enable xUnit.benchmark.

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
type 
[ITracer](https://github.com/devhawk/xunit.benchmark/blob/master/xunit.benchmark/ITracer.cs).
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

### Step Five: Build Benchmarks

For best results, benchmarks should be built in release configuration. The
xUnit.benchmark repo includes a batch file named build.cmd that builds both the
main xUnit.benchmark assembly as well as an included sample benchmark project in
release mode.

Build.cmd also does a NuGet restore on the solution file. [MsBuild-Integrated
Package Restore](http://docs.nuget.org/docs/reference/package-restore) is
enabled for the project, but that only restores project-level dependencies. In
order to actually run the benchmarks, you also need to restore the 
solution-level dependency on the [xunit.runners
package](https://www.nuget.org/packages/xunit.runners/2.0.0-beta5-build2785).

### Step Six: Execute Benchmarks
Benchmarks are executed using the same test runner as normal xUnit.net tests.
In order to get detailed information about the benchmark test run, use the
-XML option to save the test run output results as an XML file.

```
> xunit.console MyBenchmarkAssembly.dll -XML benchmarkResults.xml
```

Note: The master branch of xUnit.benchmark is currently built against 
[xUnit.net 2.0 beta 5](https://github.com/xunit/xunit/releases/tag/2.0-beta-5).
This version of xUnit.net rounds test execution time to three decimal places in 
the XML test results file. For some benchmarks, this level of precision may not 
be sufficient for accurate measurement. 

xUnit.net has been updated [post beta 5](https://github.com/xunit/xunit/pull/230) 
to emit the benchmark execution time without rounding in the XML test results 
file. To get full execuiton time precision in the XML results file today, use the
[postBeta5 branch](https://github.com/devhawk/xunit.benchmark/tree/postBeta5)
of xUnit.benchmark. This branch is built against the latest CI build of 
xUnit.net from the [xUnit.net MyGet feed](https://www.myget.org/F/xunit).

## ETW Support

In addition to typical stopwatch performance testing, xunit.benchmark raises
custom ETW events at the start/end of every benchmark, benchmark iteration and
when benchmark tracing starts and stops. This allows you to use tools such as
[PerfView](http://www.microsoft.com/en-us/download/details.aspx?id=28567) to get 
more detailed performance information about the benchmarks, including context 
switches, garbage collection and even hardware counters.

TODO: add PerfView benchmarking tutorial.

You can collect ETW traces on the command line with PerfView. Execute the 
following from an elevated command prompt. Note, this example assumes you have 
PerfView, xunit.console.exe and your benchmark assembly 
(sample.xunit.benchmark.dll in this example) in the same directory. 

```
> PerfView.exe -nogui -providers:*Microsoft-Xunit-Benchmark "-KernelEvents:Default,ContextSwitch" run xunit.console.exe sample.xunit.benchmark.dll

```

