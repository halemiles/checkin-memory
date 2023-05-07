using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneyman.Services.Extensions;
using FluentAssertions;

namespace Checkin.Tests
{
    [TestClass]
    public class IsUpTests
    {
        [DataTestMethod]
        [DataRow(6)]
        [DataRow(10)]
        public void GetPrettyDate_WhenWithinRanges_ReturnsFalse(int minutes)
        {
            var date = DateTime.Now;
            var nowOverride = date.AddMinutes(minutes*-1);
            var result = nowOverride.IsUp();
            result.Should().BeFalse();
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(4)]
        [DataRow(5)]
        public void GetPrettyDate_WhenWithinRanges_ReturnsTrue(int minutes)
        {
            var date = DateTime.Now;
            var nowOverride = date.AddMinutes(minutes*-1);
            var result = nowOverride.IsUp();
            result.Should().BeTrue();
        }
    }
}