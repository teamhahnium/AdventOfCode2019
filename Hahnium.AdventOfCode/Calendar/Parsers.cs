using System;
using System.Collections.Generic;
using System.Linq;

namespace Hahnium.AdventOfCode.Calendar
{
    public static class Parsers
    {
        public static Func<string, IEnumerable<string>> Lines() =>
            input => input.Split(Environment.NewLine);

        public static Func<string, IEnumerable<string>> Array() =>
            input => input.Split(",");

        public static Func<string, IEnumerable<int>> Int(this Func<string, IEnumerable<string>> parser) =>
            input => parser(input).Select(int.Parse);

        public static Func<string, ReadOnlyMemory<int>> Memory(this Func<string, IEnumerable<int>> parser) =>
            input => new ReadOnlyMemory<int>(parser(input).ToArray());
    }
}
