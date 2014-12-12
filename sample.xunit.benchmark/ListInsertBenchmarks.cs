using Microsoft.Xunit;
using System;
using System.Collections.Generic;

public class ListInsertBenchmarks
{
    //TODO: support data driven benchmarks - https://github.com/devhawk/xunit.benchmark/issues/1

    void InsertHelper(ITracer tracer, int count, bool setCapacity)
    {
        var ls = new List<int>(setCapacity ? count : 0);

        using (tracer.Trace())
        {
            for (int i = 0; i < count; i++)
            {
                ls.Add(i);
            }
        }
    }
    
    void InsertNoCapacityHelper(ITracer tracer, int count)
    {
        InsertHelper(tracer, count, false);
    }

    void InsertWithCapacityHelper(ITracer tracer, int count)
    {
        InsertHelper(tracer, count, true);
    }

    [Benchmark]
    public void Insert_1_element_without_capacity(ITracer tracer)
    {
        InsertNoCapacityHelper(tracer, 1);
    }

    [Benchmark]
    public void Insert_50_element_without_capacity(ITracer tracer)
    {
        InsertNoCapacityHelper(tracer, 50);
    }

    [Benchmark]
    public void Insert_500_element_without_capacity(ITracer tracer)
    {
        InsertNoCapacityHelper(tracer, 500);
    }

    [Benchmark]
    public void Insert_1_element_with_capacity(ITracer tracer)
    {
        InsertWithCapacityHelper(tracer, 1);
    }

    [Benchmark]
    public void Insert_50_element_with_capacity(ITracer tracer)
    {
        InsertWithCapacityHelper(tracer, 50);
    }

    [Benchmark]
    public void Insert_500_element_with_capacity(ITracer tracer)
    {
        InsertWithCapacityHelper(tracer, 500);
    }
}
