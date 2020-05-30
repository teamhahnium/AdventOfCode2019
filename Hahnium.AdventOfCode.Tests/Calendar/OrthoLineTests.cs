using Hahnium.AdventOfCode.Calendar;
using Xunit;

namespace Hahnium.AdventOfCode.Calendar.Tests
{
    public class OrthoLineTests
    {
        [Theory]
        [InlineData(-1, 1, -1, 1)] // Equal
        [InlineData(-1, 1, -2, 2)] // Contains
        [InlineData(-1, 1, -2, 0)] // Overlaps
        [InlineData(-1, 1, 0, 2)] // Overlaps
        [InlineData(-1, 0, 0, 1)] // Touching
        [InlineData(0, 1, 1, 0)] // Touching
        public void CoincidentalShouldIntersect(int a0, int a1, int b0, int b1)
        {
            // Horizontal
            var a = new OrthoLine(a0, a1, 0, true);
            var b = new OrthoLine(b0, b1, 0, true);
            Assert.True(a.Intersects(b, out _));
            Assert.True(b.Intersects(a, out _));

            a = new OrthoLine(-a0, -a1, 0, false);
            b = new OrthoLine(-b0, -b1, 0, false);
            Assert.True(a.Intersects(b, out _));
            Assert.True(b.Intersects(a, out _));

            // Vertical
            a = new OrthoLine(a0, a1, 0, false);
            b = new OrthoLine(b0, b1, 0, false);
            Assert.True(a.Intersects(b, out _));
            Assert.True(b.Intersects(a, out _));

            a = new OrthoLine(-a0, -a1, 0, false);
            b = new OrthoLine(-b0, -b1, 0, false);
            Assert.True(a.Intersects(b, out _));
            Assert.True(b.Intersects(a, out _));
        }

        [Fact]
        public void ParallelShouldNotIntersect()
        {
            // Horizontal
            var a = new OrthoLine(-1, 1, 0, true);
            var b = new OrthoLine(-1, 1, 1, true);
            Assert.False(a.Intersects(b, out _));
            Assert.False(b.Intersects(a, out _));

            a = new OrthoLine(1, -1, 0, true);
            b = new OrthoLine(1, -1, 1, true);
            Assert.False(a.Intersects(b, out _));
            Assert.False(b.Intersects(a, out _));

            // Vertical
            a = new OrthoLine(-1, 1, 0, false);
            b = new OrthoLine(-1, 1, 1, false);
            Assert.False(a.Intersects(b, out _));
            Assert.False(b.Intersects(a, out _));

            a = new OrthoLine(1, -1, 0, false);
            b = new OrthoLine(1, -1, 1, false);
            Assert.False(a.Intersects(b, out _));
            Assert.False(b.Intersects(a, out _));
        }

        [Theory]
        [InlineData(-1, 1, 0, -1, 1, 0, 0, 0)] // Midpoint
        [InlineData(-1, 1, -1, -1, 1, 0, -1, 0)] // Segment intersects start
        [InlineData(-1, 1, 1, -1, 1, 0, 1, 0)] // Segment intersects end
        [InlineData(-1, 1, 0, -1, 1, -1, 0, -1)] // Starts intersect
        [InlineData(-1, 1, 0, -1, 1, 1, 0, 1)] // Ends intersect
        [InlineData(-1, 1, 1, -1, 1, 1, 1, 1)] // Starts intersect
        [InlineData(-1, 1, -1, -1, 1, -1, -1, -1)] // Ends intersect
        [InlineData(-1, 1, 1, -1, 1, -1, 1, -1)] // Ends intersect
        [InlineData(146, 217, 4, 46, -12, 155, 4, 155)]
        public void PerpendicularShouldIntersect(int a0, int a1, int ao, int b0, int b1, int bo, int x, int y)
        {
            Point intersection;
            var a = new OrthoLine(a0, a1, ao, false);
            var b = new OrthoLine(b0, b1, bo, true);
            Assert.True(a.Intersects(b, out intersection));
            Assert.Equal(x, intersection.X);
            Assert.Equal(y, intersection.Y);
            Assert.True(b.Intersects(a, out intersection));
            Assert.Equal(x, intersection.X);
            Assert.Equal(y, intersection.Y);

            a = new OrthoLine(-a0, -a1, -ao, false);
            b = new OrthoLine(-b0, -b1, -bo, true);
            Assert.True(a.Intersects(b, out intersection));
            Assert.Equal(-x, intersection.X);
            Assert.Equal(-y, intersection.Y);
            Assert.True(b.Intersects(a, out intersection));
            Assert.Equal(-x, intersection.X);
            Assert.Equal(-y, intersection.Y);
        }

        [Theory]
        [InlineData(-1, 1, 0, 2, 3, 0)]
        [InlineData(-1, 1, 0, -3, -2, 0)]
        [InlineData(-1, 1, 0, -1, 1, 2)]
        [InlineData(-1, 1, 0, -1, 1, -2)]
        public void PerpendicularShouldNotIntersect(int a0, int a1, int ao, int b0, int b1, int bo)
        {
            var a = new OrthoLine(a0, a1, ao, false);
            var b = new OrthoLine(b0, b1, bo, true);
            Point intersection;
            Assert.False(a.Intersects(b, out intersection));
            Assert.False(b.Intersects(a, out intersection));
        }
    }
}
