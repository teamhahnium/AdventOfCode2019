using System;
using System.Collections.Generic;

namespace Hahnium.AdventOfCode.Calendar.Day05
{
    public class Solution : SolutionBase<int[], IEnumerable<int>, ReadOnlyMemory<int>>
    {
        public Solution()
            : base(
                  Parsers.Array().Int().ToArray(),
                  Parsers.Array().Int(),
                  Parsers.Array().Int().Memory())
        {
        }

        public override object PartA() => throw new NotImplementedException();

        public override object PartB() => throw new NotImplementedException();

        public override object FunctionalPartA() => throw new NotImplementedException();

        public override object FunctionalPartB() => throw new NotImplementedException();

        public override object FastPartA() => throw new NotImplementedException();

        public override object FastPartB() => throw new NotImplementedException();
    }
}
