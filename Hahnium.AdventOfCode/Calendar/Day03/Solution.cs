using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;

namespace Hahnium.AdventOfCode.Calendar.Day03
{
    public class Solution : SolutionBase<IList<IList<OrthoLine>>, IEnumerable<IEnumerable<OrthoLine>>, IEnumerable<IEnumerable<OrthoLine>>>
    {
        private const int TargetOutput = 19690720;

        public Solution()
            : base(
                  Parsers.Lines(Parsers.Array().Direction().ContinuousLine().ToList()).ToList(),
                  Parsers.Lines(Parsers.Array().Direction().ContinuousLine()),
                  Parsers.Lines(Parsers.Array().Direction().ContinuousLine()))
        {
        }

        public override object PartA()
        {
            int closestPoint = int.MaxValue;

            foreach (var segment in this.imperativeInput[0])
            {
                foreach (var checkSegment in this.imperativeInput[1])
                {
                    if (segment.Intersects(checkSegment, out Point intersection))
                    {
                        if (intersection != Point.Origin)
                        {
                            closestPoint = Math.Min(closestPoint, intersection.ManhattanDistance);
                        }
                    }
                }
            }

            return closestPoint;
        }

        public override object PartB()
        {
            int minSteps = int.MaxValue;
            int lineASteps = 0;

            foreach (var segment in this.imperativeInput[0])
            {
                int lineBSteps = 0;

                foreach (var checkSegment in this.imperativeInput[1])
                {
                    if (segment.Intersects(checkSegment, out Point intersection))
                    {
                        if (intersection != Point.Origin)
                        {
                            int intersectionLength;

                            if (segment.IsHorizontal)
                            {
                                intersectionLength = Math.Abs(segment.Start - intersection.X) + Math.Abs(checkSegment.Start - intersection.Y);
                            }
                            else
                            {
                                intersectionLength = Math.Abs(segment.Start - intersection.Y) + Math.Abs(checkSegment.Start - intersection.X);
                            }

                            minSteps = Math.Min(minSteps, lineASteps + lineBSteps + intersectionLength);
                        }
                    }

                    lineBSteps += checkSegment.Length;
                }

                lineASteps += segment.Length;
            }

            return minSteps;
        }

        public override object FunctionalPartA() =>
            (from segmentPair in
                (from segment in this.functionalInput.First()
                 from checkSegment in this.functionalInput.Last()
                 select (segment, checkSegment))
             let intersects = (Intersects: segmentPair.segment.Intersects(segmentPair.checkSegment, out Point intersection), intersection)
             where intersects.Intersects && intersects.intersection != Point.Origin
             select intersects.intersection.ManhattanDistance).Min();

        public override object FunctionalPartB() => throw new NotImplementedException();

        public override object FastPartA() => PartA();

        public override object FastPartB() => throw new NotImplementedException();
    }
}
