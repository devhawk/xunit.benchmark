using Microsoft.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ListInsertBenchmark
{
    //TODO: support data driven benchmarks - https://github.com/devhawk/xunit.benchmark/issues/1
    void Insert_without_capacity_helper(ITracer tracer, int count)
    {
        var ls = new List<int>(0);

        using (tracer.Trace())
        {
            for (int i = 0; i < count; i++)
            {
                ls.Add(i);
            }
        }
    }

    void Insert_with_capacity_helper(ITracer tracer, int count)
    {
        var ls = new List<int>(count);

        using (tracer.Trace())
        {
            for (int i = 0; i < count; i++)
            {
                ls.Add(i);
            }
        }
    }

    [Benchmark]
    public void Insert_1_element_without_capacity(ITracer tracer)
    {
        Insert_without_capacity_helper(tracer, 1);
    }

    [Benchmark]
    public void Insert_50_element_without_capacity(ITracer tracer)
    {
        Insert_without_capacity_helper(tracer, 50);
    }

    [Benchmark]
    public void Insert_500_element_without_capacity(ITracer tracer)
    {
        Insert_without_capacity_helper(tracer, 500);
    }

    [Benchmark]
    public void Insert_1_element_with_capacity(ITracer tracer)
    {
        Insert_with_capacity_helper(tracer, 1);
    }

    [Benchmark]
    public void Insert_50_element_with_capacity(ITracer tracer)
    {
        Insert_with_capacity_helper(tracer, 50);
    }

    [Benchmark]
    public void Insert_500_element_with_capacity(ITracer tracer)
    {
        Insert_with_capacity_helper(tracer, 500);
    }
}
