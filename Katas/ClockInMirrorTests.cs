using System;
using System.Collections.Generic;
using System.Globalization;
using FluentAssertions;
using Xunit;

namespace Katas
{
    public class ClockInMirrorTests
    {
        [Theory]
        [InlineData("12:00", "12:00")]
        [InlineData("12:22", "11:38")]
        [InlineData("05:25", "06:35")]
        [InlineData("01:50", "10:10")]
        [InlineData("11:58", "12:02")]
        [InlineData("12:01", "11:59")]
        [InlineData("12:30", "11:30")]
        [InlineData("06:00", "06:00")]
        [InlineData("06:30", "05:30")]
        [InlineData("06:15", "05:45")]
        [InlineData("06:45", "05:15")]
        [InlineData("12:15", "11:45")]
        [InlineData("12:45", "11:15")]
        public void CheckTheTime(string currentTime, string expectedTime)
            => new ClockInMirror().WhatIsTheTime(currentTime).Should().Be(expectedTime);
    }

    public class ClockInMirror
    {
        private static readonly DateTime Midnight = new(2021, 1, 1, 12, 0, 0);

        public string WhatIsTheTime(string currentTime)
        {
            var time = DateTime.ParseExact(currentTime, @"hh\:mm", CultureInfo.CurrentCulture);

            return time.Add((Midnight - time) * 2).ToString(@"hh\:mm");
        }
    }
}