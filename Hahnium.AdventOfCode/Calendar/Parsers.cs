﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Hahnium.AdventOfCode.Calendar
{
    public static class Parsers
    {
        public static Func<string, IEnumerable<string>> Lines() =>
            input => input.Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        public static Func<string, IEnumerable<T>> Lines<T>(
            Func<string, T> elementParser) =>
                input => input.Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(elementParser);

        public static Func<string, IEnumerable<string>> Array() =>
            input => input.Split(",");

        public static Func<string, IEnumerable<T>> Array<T>(
            Func<string, T> elementParser) =>
                input => input.Split(",").Select(elementParser);

        public static Func<string, IEnumerable<Point>> Direction(this Func<string, IEnumerable<string>> parser) =>
            input => parser(input).Select(item =>
            {
                int magnitude = int.Parse(item.Substring(1));

                return item[0] switch
                {
                    'R' => Point.Right(magnitude),
                    'U' => Point.Up(magnitude),
                    'L' => Point.Left(magnitude),
                    'D' => Point.Down(magnitude),
                    _ => throw new FormatException()
                };
            });

        public static Func<string, IEnumerable<OrthoLine>> ContinuousLine(this Func<string, IEnumerable<Point>> parser) =>
            input => parser(input).Scan(
                new OrthoLine(0, 0, 0, true),
                (line, direction) => OrthoLine.From(line.EndPoint, line.EndPoint + direction));

        public static Func<string, IList<TElement>> ToList<TElement>(this Func<string, IEnumerable<TElement>> parser) =>
            input => parser(input).ToList();

        public static Func<string, TElement[]> ToArray<TElement>(this Func<string, IEnumerable<TElement>> parser) =>
            input => parser(input).ToArray();

        public static Func<string, IEnumerable<int>> Int(this Func<string, IEnumerable<string>> parser) =>
            input => parser(input).Select(int.Parse);

        public static Func<string, ReadOnlyMemory<int>> Memory(this Func<string, IEnumerable<int>> parser) =>
            input => new ReadOnlyMemory<int>(parser(input).ToArray());
    }
}
