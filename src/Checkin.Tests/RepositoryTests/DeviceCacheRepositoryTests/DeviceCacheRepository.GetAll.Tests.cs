using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Checkin.Models;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;

namespace Checkin.Tests
{
    public partial class DeviceCacheRepositoryTests
    {
        [TestMethod]
        public void GetAll_WhenNoDevicesExist_ReturnsEmptyList()
        {
            // ARRANGE
            var fixture = new Fixture();
            var expected = new List<Device>(){};
            cache.Set(deviceCacheKey, expected);

            var deviceCache = NewDeviceCacheRepository();

            // ACT
            var result = deviceCache.GetAll();

            // ASSERT
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetAll_WhenMultipleDevicesAreCreated_ReturnsMultipleDevices()
        {
            // ARRANGE
            var fixture = new Fixture();
            var expected = new List<Device>(){
                fixture.Create<Device>(),
                fixture.Create<Device>()
            };
            cache.Set(deviceCacheKey, expected);

            var deviceCache = NewDeviceCacheRepository();

            // ACT
            var result = deviceCache.GetAll();

            // ASSERT
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
        }
    }
}