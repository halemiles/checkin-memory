using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneyman.Models.Extensions;
using FluentAssertions;

namespace Checkin.Tests
{
    [TestClass]
    public class IsUpTests
    {
        [DataTestMethod]
        [DataRow(10)]
        [DataRow(11)]
        public void GetPrettyDate_WhenWithinRanges_ReturnsFalse(int minutes)
        {
            var date = DateTime.Now;
            var nowOverride = date.AddMinutes(minutes*-1);
            var result = nowOverride.IsUp();
            result.Should().BeFalse();
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(5)]
        [DataRow(9)]
        public void GetPrettyDate_WhenWithinRanges_ReturnsTrue(int minutes)
        {
            var date = DateTime.Now;
            var nowOverride = date.AddMinutes(minutes*-1);
            var result = nowOverride.IsUp();
            result.Should().BeTrue();
        }
    }
}