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
        [InlineData("12:30", "12:30")]
        [InlineData("06:00", "06:00")]
        [InlineData("06:30", "06:30")]
        [InlineData("06:15", "06:45")]
        [InlineData("06:45", "06:15")]
        [InlineData("12:15", "12:45")]
        [InlineData("12:45", "12:15")]
        public void CheckTheTime(string currentTime, string expectedTime)
            => new ClockInMirror().WhatIsTheTime(currentTime).Should().Be(expectedTime);
    }

    public class ClockInMirror
    {
        private static readonly DateTime Midnight = new(2021, 1, 1, 12, 0, 0);

        private readonly List<int> _mirroredHours = new() { 0, 6 };
        private readonly List<int> _mirroredMinutes = new() { 0, 15, 30, 45 };

        public string WhatIsTheTime(string currentTime)
        {
            var time = DateTime.ParseExact(currentTime, @"hh\:mm", CultureInfo.CurrentCulture);

            var mirroredTime = time.Add((Midnight - time) * 2);

            if (_mirroredHours.Contains(time.Hour) && _mirroredMinutes.Contains(time.Minute))
                mirroredTime = new DateTime(2021, 1, 1, time.Hour, mirroredTime.Minute, 0);

            return mirroredTime.ToString(@"hh\:mm");
        }
    }
}