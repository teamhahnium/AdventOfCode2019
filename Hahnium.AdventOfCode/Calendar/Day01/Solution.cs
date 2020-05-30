using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Hahnium.AdventOfCode.Calendar.Day01
{
    public class Solution : SolutionBase<IEnumerable<int>, IEnumerable<int>, ReadOnlyMemory<int>>
    {
        public Solution()
            : base(
                  Parsers.Lines().Int(),
                  Parsers.Lines().Int(),
                  Parsers.Lines().Int().Memory())
        {
        }

        public override object PartA()
        {
            int fuelMass = 0;

            foreach (var moduleMass in this.imperativeInput)
            {
                fuelMass += (moduleMass / 3) - 2;
            }

            return fuelMass;
        }

        public override object PartB()
        {
            int fuelMass = 0;

            foreach (var moduleMass in this.imperativeInput)
            {
                var moduleFuel = (moduleMass / 3) - 2;

                while (moduleFuel > 0)
                {
                    fuelMass += moduleFuel;
                    moduleFuel = (moduleFuel / 3) - 2;
                }
            }

            return fuelMass;
        }

        public override object FunctionalPartA() =>
        (
            from moduleMass in this.functionalInput
            let fuel = (moduleMass / 3) - 2
            select fuel
        ).Sum();

        public override object FunctionalPartB() =>
        (
            from moduleMass in this.functionalInput
            let initialFueld = (moduleMass / 3) - 2
            let fuel = EnumerableEx.Generate(initialFueld, mass => mass > 0, mass => (mass / 3) - 2, FuncEx.Identity).Sum()
            select fuel
        ).Sum();

        public override object FastPartA()
        {
            var three = new Vector<int>(3);
            var two = new Vector<int>(2);
            var sum = Vector<int>.Zero;
            var inputSpan = this.pointerInput.Span;

            do
            {
                if (inputSpan.Length < Vector<int>.Count)
                {
                    var tempSpan = new Span<int>(new int[Vector<int>.Count]);
                    inputSpan.CopyTo(tempSpan);
                    inputSpan = tempSpan;
                }

                var a = new Vector<int>(inputSpan);
                var result = a / three - two;
                sum += Vector.Max(result, Vector<int>.Zero);
                inputSpan = inputSpan.Slice(Vector<int>.Count);
            }
            while (inputSpan.Length > 0);

            var elements = new int[Vector<int>.Count];
            sum.CopyTo(elements);
            int fuelMass = elements.Sum();

            return fuelMass;
        }

        public override object FastPartB()
        {
            var three = new Vector<int>(3);
            var two = new Vector<int>(2);
            var fuelMass = Vector<int>.Zero;
            var inputSpan = this.pointerInput.Span;

            do
            {
                if (inputSpan.Length < Vector<int>.Count)
                {
                    var tempSpan = new Span<int>(new int[Vector<int>.Count]);
                    inputSpan.CopyTo(tempSpan);
                    inputSpan = tempSpan;
                }

                var moduleMass = new Vector<int>(inputSpan);
                var moduleFuel = moduleMass / three - two;

                while (Vector.GreaterThanAny(moduleFuel, Vector<int>.Zero))
                {
                    fuelMass += Vector.Max(moduleFuel, Vector<int>.Zero);
                    moduleFuel = (moduleFuel / three) - two;
                }

                inputSpan = inputSpan.Slice(Vector<int>.Count);
            }
            while (inputSpan.Length > 0);

            var elements = new int[Vector<int>.Count];
            fuelMass.CopyTo(elements);

            return elements.Sum();
        }
    }
}
