using Microsoft.Xunit;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class DictionaryTryGetBenchmarks
{
    const int Size = 1024;

    void TryGetHelper<T>(ITracer tracer, T[] keys)
    {
        Debug.Assert(keys.Length == Size);
        Dictionary<T, int> dict = new Dictionary<T, int>(Size);
        foreach (T key in keys)
        {
            dict[key] = 0;
        }

        using (tracer.Trace())
        {
            for (int i = 0; i < keys.Length; i++)
            {
                int result;
                dict.TryGetValue(keys[i], out result);
            }
        }
    }

    [Benchmark]
    public void Try_get_ints(ITracer tracer)
    {
        TryGetHelper<int>(tracer, DictionaryHelpers.GetIntKeyArray(Size));
    }


    [Benchmark]
    public void Try_get_strings(ITracer tracer)
    {
        TryGetHelper<string>(tracer, DictionaryHelpers.GetStringKeyArray(Size));
    }
}
