using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleXunitTest
{
    public class SampleAdditionTests
    {
        [Fact]
        public void Two_plus_two_equals_four()
        {
            Assert.Equal(4, 2 + 2);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(3, 4, 7)]
        [InlineData(5, 6, 11)]
        public void addition_theory(int a, int b, int total)
        {
            Assert.Equal(total, a + b);
        }

    }
}
