using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Checkin.Models;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;

namespace Checkin.Tests
{
    [TestClass]
    public class DeviceCacheRepositorySearchTests : DeviceCacheRepositorySetup
    {
        [TestMethod]
        public void Search_WhenNoDevicesExist_ReturnsEmptyList()
        {
            // ARRANGE
            var fixture = new Fixture();
            var expected = new List<Device>(){};
            cache.Set(deviceCacheKey, expected);

            var deviceCache = NewDeviceCacheRepository();

            // ACT
            var result = deviceCache.Search(999, "192.168.0.1");

            // ASSERT
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void Search_WhenMultipleDevicesAreCreated_ReturnsMultipleDevices()
        {
            // ARRANGE
            var fixture = new Fixture();
            var expected = new List<Device>(){
                fixture.Build<Device>().With(x => x.Id, 999).Create()
            };
            cache.Set(deviceCacheKey, expected);

            var deviceCache = NewDeviceCacheRepository();

            // ACT
            var result = deviceCache.Search(999, string.Empty);

            // ASSERT
            result.Should().NotBeNull();
            result.Count.Should().Be(1);
        }
    }
}