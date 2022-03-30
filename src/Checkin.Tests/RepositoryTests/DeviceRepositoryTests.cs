using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkin.Repositories;
using Moq;
using Checkin.Services;
using System.Collections.Generic;
using Checkin.Models;
using System;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;

namespace Checkin.Tests
{
    [TestClass]
    public class DeviceRepositoryTests
    {
        private Mock<ICacheRepository<List<Device>>> mockCacheRepository;
        private DeviceRepository NewDeviceRepository() => 
            new DeviceRepository(mockCacheRepository.Object);

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
                    It.IsAny<string>(), 
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
                    It.IsAny<string>(), 
                    It.IsAny<List<Device>>()
                ), Times.Once);
        }

        private static List<Device> GenerateMultiple()
        {
            List<Device> devices = new();
            for(int i=0;i <5; i++)
            {
                devices.Add(new Device()
                {
                    Id = 0,
                    CreatedDate = DateTime.Now,
                    IpAddress = "127.0.0.1"
                });
            }
            return devices;
        }
    }
}