using Hahnium.AdventOfCode.Calendar.Day04;
using Hahnium.AdventOfCode.Tests;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xunit;

namespace Hahnium.AdventOfCode.Calendar.Tests
{
    public class Day04
    {
        [Theory]
        [InlineData(111111)]
        [InlineData(223455)]
        [InlineData(122789)]
        public void MeetsPasswordCriteria(int password)
        {
            var sln = new Solution();
            Assert.True(sln.IsValidPassword(password));
        }

        [Theory]
        [InlineData(111110)]
        [InlineData(223450)]
        [InlineData(123789)]
        public void DoesNotMeetPasswordCriteria(int password)
        {
            var sln = new Solution();
            Assert.False(sln.IsValidPassword(password));
        }

        [Theory]
        [InlineData(112233)]
        [InlineData(123344)]
        [InlineData(111223)]
        [InlineData(111122)]
        [InlineData(223333)]
        public void MeetsPasswordCriteriaB(int password)
        {
            var sln = new Solution();
            Assert.True(sln.IsValidPasswordB(password));
        }

        [Theory]
        [InlineData(111123)]
        [InlineData(156669)]
        [InlineData(123444)]
        [InlineData(111222)]
        [InlineData(148889)]
        public void DoesNotMeetPasswordCriteriaB(int password)
        {
            var sln = new Solution();
            Assert.False(sln.IsValidPasswordB(password));
        }
    }
}
