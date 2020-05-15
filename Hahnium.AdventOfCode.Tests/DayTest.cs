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

        public string RunPartA(string input) => new TDay().PartA(input);

        public string RunPartB(string input) => new TDay().PartB(input);

        public string RunFunctionalPartA(string input) => new TDay().FunctionalPartA(input);

        public string RunFunctionalPartB(string input) => new TDay().FunctionalPartB(input);

        public string RunFastPartA(string input) => new TDay().FastPartA(input);

        public string RunFastPartB(string input) => new TDay().FastPartB(input);

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
