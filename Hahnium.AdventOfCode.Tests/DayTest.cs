using Hahnium.AdventOfCode.Calendar;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hahnium.AdventOfCode.Tests
{
    public abstract class DayTest<TTest, TDay>
        where TDay : IDay, new()
        where TTest : DayTest<TTest, TDay>, new()
    {
        public abstract IEnumerable<(string Input, string Expected)> PartATests { get; }
        public abstract IEnumerable<(string Input, string Expected)> PartBTests { get; }

        public string RunPartA(string input) => CreateDay(input).PartA().ToString();

        public string RunPartB(string input) => CreateDay(input).PartB().ToString();

        public string RunFunctionalPartA(string input) => CreateDay(input).FunctionalPartA().ToString();

        public string RunFunctionalPartB(string input) => CreateDay(input).FunctionalPartB().ToString();

        public string RunFastPartA(string input) => CreateDay(input).FastPartA().ToString();

        public string RunFastPartB(string input) => CreateDay(input).FastPartB().ToString();

        private TDay CreateDay(string input)
        {
            var day = new TDay();
            day.Parse(input);
            return day;
        }

        public static IEnumerable<object[]> PartATestData => new TTest().PartATests.Select(o => new object[] { o.Input, o.Expected });
        public static IEnumerable<object[]> PartBTestData => new TTest().PartBTests.Select(o => new object[] { o.Input, o.Expected });

        [Theory]
        [MemberData(nameof(PartATestData))]
        public void PartA(string input, string expected)
        {
            Assert.Equal(expected, new TTest().RunPartA(input));
        }

        [Theory]
        [MemberData(nameof(PartBTestData))]
        public void PartB(string input, string expected)
        {
            Assert.Equal(expected, new TTest().RunPartB(input));
        }

        [Theory]
        [MemberData(nameof(PartATestData))]
        public void FunctionalPartA(string input, string expected)
        {
            Assert.Equal(expected, new TTest().RunFunctionalPartA(input));
        }

        [Theory]
        [MemberData(nameof(PartBTestData))]
        public void FunctionalPartB(string input, string expected)
        {
            Assert.Equal(expected, new TTest().RunFunctionalPartB(input));
        }

        [Theory]
        [MemberData(nameof(PartATestData))]
        public void FastPartA(string input, string expected)
        {
            Assert.Equal(expected, new TTest().RunFastPartA(input));
        }

        [Theory]
        [MemberData(nameof(PartBTestData))]
        public void FastPartB(string input, string expected)
        {
            Assert.Equal(expected, new TTest().RunFastPartB(input));
        }
    }
}
