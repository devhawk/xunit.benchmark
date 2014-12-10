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

        class NullDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }

        static readonly NullDisposable nullDisposable = new NullDisposable();

        public IDisposable Trace()
        {
            return nullDisposable;
        }

        public static readonly NullTracer Instance = new NullTracer();
    }
}
