using Hahnium.AdventOfCode.Calendar.Day02;
using Hahnium.AdventOfCode.Tests;
using System.Collections.Generic;
using System.Linq;

namespace Hahnium.AdventOfCode.Calendar.Tests
{
    public class Day02 : DayTest<Day02, Solution>
    {
        public override IEnumerable<(string Input, string Expected)> PartATests => new[]
        {
            ("1,9,10,3,2,3,11,0,99,30,40,50", "3500"),
            ("1,0,0,0,99", "2"),
            ("2,3,0,3,99", "2"),
            ("2,4,4,5,99,0", "2"),
            ("1,1,1,4,99,5,6,0,99", "30"),
        };

        public override IEnumerable<(string Input, string Expected)> PartBTests =>
            Enumerable.Empty<(string, string)>();
    }
}
