using Microsoft.Xunit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

public class DictionaryAddBenchmarks
{
    const int Size = 1024;

    void AddHelper<T>(ITracer tracer, T[] keys, bool setCapacity)
    {
        Debug.Assert(keys.Length == Size);

        Dictionary<T, int> dictionary = new Dictionary<T, int>(setCapacity ? Size : 0);

        using (tracer.Trace())
        {
            for (int i = 0; i < keys.Length; i++)
            {
                dictionary.Add(keys[i], i);
            }
        }
    }

    void AddWithCapacityHelper<T>(ITracer tracer, T[] keys)
    {
        AddHelper<T>(tracer, keys, true);
    }

    void AddNoCapacityHelper<T>(ITracer tracer, T[] keys)
    {
        AddHelper<T>(tracer, keys, false);
    }

    [Benchmark]
    public void Add_ints_without_capacity(ITracer tracer)
    {
        AddNoCapacityHelper<int>(tracer, DictionaryHelpers.GetIntKeyArray(Size));
    }

    [Benchmark]
    public void Add_strings_without_capacity(ITracer tracer)
    {
        AddNoCapacityHelper<string>(tracer, DictionaryHelpers.GetStringKeyArray(Size));
    }

    [Benchmark]
    public void Add_ints_with_capacity(ITracer tracer)
    {
        AddWithCapacityHelper<int>(tracer, DictionaryHelpers.GetIntKeyArray(Size));
    }

    [Benchmark]
    public void Add_strings_with_capacity(ITracer tracer)
    {
        AddWithCapacityHelper<string>(tracer, DictionaryHelpers.GetStringKeyArray(Size));
    }
}
