using Hahnium.AdventOfCode.Calendar.Day02;
using Hahnium.AdventOfCode.Tests;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hahnium.AdventOfCode.Calendar.Tests
{
    public class Day02
    {
        [Theory]
        [InlineData(9, 10, "1,9,10,3,2,3,11,0,99,30,40,50", 3500)]
        [InlineData(10, 11, "1,9,10,3,2,3,11,0,99,30,40,50", 4500)]
        [InlineData(0, 0, "1,0,0,0,99", 2)]
        [InlineData(4, 4, "1,0,0,0,99", 198)]
        [InlineData(3, 0, "2,3,0,3,99", 2)]
        [InlineData(4, 4, "2,3,0,3,99", 2)]
        [InlineData(2, 3, "2,4,4,5,99,0", 2)]
        [InlineData(4, 4, "2,4,4,5,99,0", 2)]
        [InlineData(1, 1, "1,1,1,4,99,5,6,0,99", 30)]
        public void ExecuteTest(int noun, int verb, string input, int expected)
        {
            var day = new Solution();
            day.Parse(input);
            Assert.Equal(expected, day.Execute(noun, verb));
            Assert.Equal(expected, day.FastExecute(noun, verb));
            Assert.Equal(expected, day.FunctionalExecute(noun, verb));
        }
    }
}
