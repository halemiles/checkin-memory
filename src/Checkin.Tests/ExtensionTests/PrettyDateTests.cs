using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkin.Models.Extensions;

namespace Checkin.Tests
{
    [TestClass]
    public class PrettyDateTests
    {
        [DataTestMethod]
        [DataRow(0,"just now")]
        [DataRow(10,"30 seconds ago")]
        [DataRow(30,"1 minute ago")]
        [DataRow(120,"2 minutes ago")]
        [DataRow(3600,"1 hour ago")]
        [DataRow(7200,"2 hours ago")]
        [DataRow(86400,"yesterday")]
        [DataRow(172800,"2 days ago")]
        [DataRow(604800,"1 weeks ago")]
        public void GetPrettyDate_WhenWithinRanges_ReturnsValid(int secondsOffset, string expectedText)
        {
            var date = DateTime.Now;
            var nowOverride = date.AddSeconds(secondsOffset);
            var prettyDate = date.GetPrettyDate(nowOverride);
            Assert.AreEqual(expectedText, prettyDate);
        }

        [TestMethod]
        public void GetPrettyDate_WhenGreaterThanOneMonth_ReturnsNull()
        {
            var date = DateTime.Now;
            var nowOverride = date.AddDays(31);
            var prettyDate = date.GetPrettyDate(nowOverride);
            Assert.IsNull(prettyDate);
        }

        [TestMethod]
        public void GetPrettyDate_WhenLessthanZeroSeconds_ReturnsNull()
        {
            var date = DateTime.Now;
            var nowOverride = date.AddDays(-1);
            var prettyDate = date.GetPrettyDate(nowOverride);
            Assert.IsNull(prettyDate);
        }
    }
}