using System;
using System.Collections.Generic;
using System.Linq;

namespace Hahnium.AdventOfCode.Calendar.Day01
{
    public class Solution : SolutionInt32
    {
        public override string PartA(IEnumerable<int> input)
        {
            return (
                from moduleMass in input
                let fuel = (moduleMass / 3) - 2
                select fuel).Sum().ToString();
        }

        public override string PartB(IEnumerable<int> input)
        {
            throw new NotImplementedException();
        }
    }
}
