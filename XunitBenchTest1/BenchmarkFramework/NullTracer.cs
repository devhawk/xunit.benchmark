using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHawk.Xunit
{
    public class NullTracer : ITracer
    {
        private NullTracer() { }

        class Disposer : IDisposable
        {
            public void Dispose()
            {
            }
        }

        static readonly Disposer nullDisposable = new Disposer();

        public IDisposable Trace()
        {
            return nullDisposable;
        }

        public static readonly NullTracer Instance = new NullTracer();
    }
}
