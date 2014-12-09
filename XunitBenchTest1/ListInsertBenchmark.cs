using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DevHawk.Xunit;

[assembly: TestFramework("DevHawk.Xunit.BenchmarkTestFramework", "XunitBenchTest1")]

namespace XunitBenchTest1
{
    public class ListIndexerBenchmark
    {
        const int Size = 64 * 1024;

        [Benchmark(Iterations=50)]
        public void ListIndexer()
        {
            // start iteration setup
            var a1 = new int[Size];
            var a2 = new int[Size];

            var l1 = new List<int>(a1);
            var l2 = new List<int>(a2);
            // end iteration setup

            // start iteration
            for (int i = 0; i < l1.Count; i++)
            {
                l1[i] = l2[l2.Count - i - i];
            }
            // end iteration
            

        }

    }
    public class ListInsertBenchmark
    {
        //eventually something like this:
        //  [BenchmarkTheory(Iterations = 1000)]
        //  [InlineData(1)]            
        //  [InlineData(50)]
        //  [InlineData(500)]
        void NoCapacityInsert(int count)
        {
            // start iteration setup
            var ls = new List<int>(0);
            // end iteration setup

            // start iteration
            for (int i = 0; i < count; i++)
            {
                ls.Add(i);
            }
            // end iteration 

            // no iteration cleanup in this example
        }

        void CapacityInsert(int count)
        {
            // start iteration setup
            var ls = new List<int>(count);
            // end iteration setup

            // start iteration
            for (int i = 0; i < count; i++)
            {
                ls.Add(i);
            }
            // end iteration 

            // no iteration cleanup in this example
        }

        [Benchmark(Iterations=1000)]
        public void NoCapacityInsertOne()
        {
            NoCapacityInsert(1);
        }

        [Benchmark(Iterations = 1000)]
        public void NoCapacityInsertFifty()
        {
            NoCapacityInsert(50);
        }

        [Benchmark(Iterations=1000)]
        public void NoCapacityInsertFiveHundred()
        {
            NoCapacityInsert(500);
        }

        [Benchmark(Iterations=1000)]
        public void CapacityInsertOne()
        {
            CapacityInsert(1);
        }

        [Benchmark(Iterations=1000)]
        public void CapacityInsertFifty()
        {
            CapacityInsert(50);
        }

        [Benchmark(Iterations=1000)]
        public void CapacityInsertFiveHundred()
        {
            CapacityInsert(500);
        }


    }
}
