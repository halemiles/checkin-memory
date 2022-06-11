using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Checkin.Models;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Text;
using System.Linq;
using System.Linq;

namespace Checkin.Tests
{
    [TestClass]
    public class DistrubutedDeviceCacheRepositoryGetAllTests : DistributedDeviceCacheRepositorySetup
    {
        [TestMethod]
        public void GetAll_WhenNoDevicesExist_ReturnsEmptyList()
        {
            // ARRANGE
            var expected = new List<Device>(){};
            var mockJson = JsonSerializer.Serialize(expected);
            distributedCache.Set("Devices", Encoding.ASCII.GetBytes(mockJson));
            //mockDistributedCache.Setup(x => x.GetString(deviceCacheKey)).Returns(mockJson);

            var deviceCache = NewDistributedDeviceCacheRepository();

            // ACT
            var result = deviceCache.GetAll();

            // ASSERT
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetAll_WhenMultipleDevicesExist_ReturnsMultipleDevices()
        {
            // ARRANGE
            var fixture = new Fixture();
            var expected = new List<Device>(){
                fixture.Create<Device>(),
                fixture.Create<Device>(),
                fixture.Create<Device>()
            
            };
            var mockJson = JsonSerializer.Serialize(expected);
            distributedCache.Set("Devices", Encoding.ASCII.GetBytes(mockJson));
            //mockDistributedCache.Setup(x => x.GetString(deviceCacheKey)).Returns(mockJson);

            var deviceCache = NewDistributedDeviceCacheRepository();

            // ACT
            var result = deviceCache.GetAll();

            // ASSERT
            result.Should().NotBeNull();
            result.Count.Should().Be(expected.Count);
        }
    }
}