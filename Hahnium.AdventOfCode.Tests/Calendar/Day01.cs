using Hahnium.AdventOfCode.Calendar.Day01;
using Hahnium.AdventOfCode.Tests;
using System.Collections.Generic;

namespace Hahnium.AdventOfCode.Calendar.Tests
{
    public class Day01 : DayTest<Day01, Solution>
    {
        public override IEnumerable<(string Input, string Expected)> PartATests => new[]
        {
            ("12", "2"),
            ("14", "2"),
            ("1969", "654"),
            ("100756", "33583"),
        };

        public override IEnumerable<(string Input, string Expected)> PartBTests => new[]
        {
            ("", ""),
        };
    }
}
