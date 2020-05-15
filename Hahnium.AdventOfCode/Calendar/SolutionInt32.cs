using System;
using System.Collections.Generic;
using System.Linq;

namespace Hahnium.AdventOfCode.Calendar
{
    public abstract class SolutionInt32 : IDay
    {
        public IEnumerable<int> ParseInput(string input) => input.Split(Environment.NewLine).Select(o => int.Parse(o));

        public ReadOnlyMemory<int> TransformInput(string input)
        {
            // TODO: Optimize and precache this
            var data = ParseInput(input).ToArray();
            return data.AsMemory();
        }

        public string PartA(string input) => PartA(ParseInput(input).ToList());

        public abstract string PartA(IList<int> input);

        public string PartB(string input) => PartB(ParseInput(input).ToList());

        public abstract string PartB(IList<int> input);

        public string FunctionalPartA(string input) => FunctionalPartA(ParseInput(input));

        public abstract string FunctionalPartA(IEnumerable<int> input);

        public string FunctionalPartB(string input) => FunctionalPartB(ParseInput(input));

        public abstract string FunctionalPartB(IEnumerable<int> input);

        public string FastPartA(string input) => FastPartA(TransformInput(input));

        public abstract string FastPartA(ReadOnlyMemory<int> input);

        public string FastPartB(string input) => FastPartB(TransformInput(input));

        public abstract string FastPartB(ReadOnlyMemory<int> input);
    }
}
