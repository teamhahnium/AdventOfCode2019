using System;
using System.Collections.Generic;
using System.Linq;

namespace Hahnium.AdventOfCode.Calendar
{
    public abstract class SolutionInt32 : IDay
    {
        public IEnumerable<int> ParseInput(string input) => input.Split(Environment.NewLine).Select(o => int.Parse(o));

        public string PartA(string input) => PartA(ParseInput(input));

        public abstract string PartA(IEnumerable<int> input);

        public string PartB(string input) => PartB(ParseInput(input));

        public abstract string PartB(IEnumerable<int> input);
    }
}
