using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkin.Repositories;
using Moq;
using Checkin.Services;
using System.Collections.Generic;
using Checkin.Models;
using System;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Threading;

namespace Checkin.Tests
{
    [TestClass]
    public class DeviceRepositoryTests
    {
        private Mock<ICacheRepository<List<Device>>> mockCacheRepository;
        private DeviceRepository NewDeviceRepository() => 
            new DeviceRepository(mockCacheRepository.Object);

        private string expectedDeviceCacheKey = "Devices";
        private List<Device> defaultDevices;

        [TestInitialize]
        public void SetUp()
        {
            mockCacheRepository = new Mock<ICacheRepository<List<Device>>>();

            List<Device> defaultDevices = new()
            {
                new Device()
                {
                    Id = 0,
                    CreatedDate = DateTime.Now,
                    IpAddress = "127.0.0.1"
                }
            };
        }

        [TestMethod]
        public void Create_WithSingleDevice_VerifySetIsCalledOnce()
        {
            // Arrange                   
            var sut = NewDeviceRepository();
            // Act
            sut.Create(defaultDevices);

            //Assert
            mockCacheRepository.Verify(x => x.Set(
                    expectedDeviceCacheKey, 
                    It.IsAny<List<Device>>()
                ), Times.Once);
        }

        [TestMethod]
        public void Create_WithMultipleDevices_VerifySetIsCalledOnce()
        {
            // Arrange                   
            var sut = NewDeviceRepository();
            // Act
            sut.Create(GenerateMultiple());

            //Assert            
            mockCacheRepository.Verify(x => x.Set(
                    expectedDeviceCacheKey, 
                    It.IsAny<List<Device>>()
                ), Times.Once);
        }

        [TestMethod]
        public void Update_WithExistingDevice_VerifyCacheUpdated()
        {
            // Arrange  
            var sut = NewDeviceRepository();
            
            var updatedDevice = 
                new Device()
                {
                    Id = 0,
                    CreatedDate = DateTime.Now,
                    Name = "Updated Name",
                    IpAddress = "192.168.0.11"
                
            };  
            var expectedDevicesToBeWritten = new List<Device>()
            {
                updatedDevice,
                new Device(){Id = 1,IpAddress = "127.0.0.1"},
                new Device(){Id = 2,IpAddress = "127.0.0.1"},
                new Device(){Id = 3,IpAddress = "127.0.0.1"},
                new Device(){Id = 4,IpAddress = "127.0.0.1"},
            };
            mockCacheRepository.Setup(x => x.Get(expectedDeviceCacheKey)).Returns(GenerateMultiple());
            
            // Act
            sut.Update(updatedDevice);
            //Assert
            mockCacheRepository.Verify(x => x.Get(expectedDeviceCacheKey), Times.Once);
            mockCacheRepository.Verify(x => x.Set(
                    expectedDeviceCacheKey, 
                    It.IsAny<List<Device>>()
                ), Times.Once);
            
        }

        [TestMethod]
        public void Update_WithExistingDevice_VerifyCahceUpdated2()
        {
            // Arrange         
            var cacheRepository = new CacheRepository<List<Device>>(new MemoryCache(new MemoryCacheOptions()));       
            var sut = new DeviceRepository(cacheRepository);
            var updatedDevice = new Device()
            {
                Id = 0,
                CreatedDate = DateTime.Now,
                Name = "Updated Name",
                IpAddress = "192.168.0.11"
            };
            // Act
            // sut.Create(defaultDevices);
            // sut.Update(updatedDevice);
            // var results = sut.GetAll();
            // //Assert
            // results.Count.Should().Be(1);
            // var firstResult = results.FirstOrDefault();
            // firstResult.Should().NotBeNull();
            // firstResult.Name.Should().Be("Updated Name");
            // firstResult.IpAddress.Should().Be("192.168.0.11");
            
        }

        

        private static List<Device> GenerateMultiple()
        {
            List<Device> devices = new();
            DateTime timestamp = DateTime.Now;
            for(int i=0;i <5; i++)
            {
                devices.Add(new Device()
                {
                    Id = i,
                    IpAddress = "127.0.0.1"
                });
            }
            return devices;
        }
    }
}