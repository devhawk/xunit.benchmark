using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevHawk.Xunit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    class BenchmarkAttribute : Attribute
    {
        public int Iterations { get; set; }
        public bool CollectGarbage { get; set; }
    }
}
