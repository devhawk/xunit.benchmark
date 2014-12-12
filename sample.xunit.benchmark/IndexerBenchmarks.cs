using Microsoft.Xunit;
using System;
using System.Collections.Generic;

public class IndexerBenchmarks
{
    const int Size = 64 * 1024;

    // TODO: Support generic benchmarks https://github.com/devhawk/xunit.benchmark/issues/2

    void ListIndexerHelper<T>(ITracer tracer)
    {
        T[] a1 = new T[Size];
        T[] a2 = new T[Size];
        List<T> l1 = new List<T>(a1);
        List<T> l2 = new List<T>(a2);

        using (tracer.Trace())
        {
            for (int i = 0; i < l1.Count; i++)
            {
                l1[i] = l2[l2.Count - i - 1];
            }
        }
    }

    void ArrayIndexerHelper<T>(ITracer tracer)
    {
        T[] a1 = new T[Size];
        T[] a2 = new T[Size];

        using (tracer.Trace())
        {
            for (int i = 0; i < a1.Length; i++)
            {
                a1[i] = a2[a2.Length - i - 1];
            }
        }
    }

    [Benchmark]
    public void List_indexer_of_int(ITracer tracer)
    {
        ListIndexerHelper<int>(tracer);
    }

    [Benchmark]
    public void List_indexer_of_string(ITracer tracer)
    {
        ListIndexerHelper<string>(tracer);
    }

    [Benchmark]
    public void Array_indexer_of_int(ITracer tracer)
    {
        ArrayIndexerHelper<int>(tracer);
    }

    [Benchmark]
    public void Array_indexer_of_string(ITracer tracer)
    {
        ArrayIndexerHelper<string>(tracer);
    }
}
