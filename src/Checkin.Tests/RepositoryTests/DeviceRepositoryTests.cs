using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkin.Repositories;
using Moq;
using System.Collections.Generic;
using Checkin.Models;
using System;
using FluentAssertions;
using Serilog;
using Checkin.Services.Interfaces;
using AutoFixture;
using System.Linq;

namespace Checkin.Tests
{
    [TestClass]
    public class DeviceRepositoryTests
    {
        private Mock<IDeviceCacheRepository> mockCacheRepository;
        private Mock<ILogger> mockLogger;
        private LocalDeviceRepository NewDeviceRepository() =>
            new(
                    mockCacheRepository.Object,
                    mockLogger.Object
                );

        private List<Device> defaultDevices;

        [TestInitialize]
        public void SetUp()
        {
            mockCacheRepository = new Mock<IDeviceCacheRepository>();
            mockLogger = new Mock<ILogger>();
            Fixture defaultDeviceFixture = new();
            defaultDevices = defaultDeviceFixture.CreateMany<Device>(10).ToList();
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
                    It.IsAny<List<Device>>()
                ), Times.Once);
        }

        [TestMethod]
        public void Create_WithEmptyDeviceList_VerifySetIsCalledOnce()
        {
            // Arrange                   
            var sut = NewDeviceRepository();
            // Act
            sut.Create(new List<Device>());

            //Assert            
            mockCacheRepository.Verify(x => x.Set(
                    It.IsAny<List<Device>>()
                ), Times.Once);
        }

        [TestMethod]
        public void GetAll_WithExistingDevice_VerifyCacheReturnsRecords()
        {
            // Arrange  
            var sut = NewDeviceRepository();
            mockCacheRepository.Setup(x => x.GetAll()).Returns(GenerateMultiple());

            // Act
            var result = sut.GetAll();
            //Assert

            mockCacheRepository.Verify(x => x.GetAll(), Times.Once);
            result.Count.Should().Be(5);
        }

        [TestMethod]
        public void Update_WithExistingDevice_VerifyCacheUpdated()
        {
            // Arrange  
            var sut = NewDeviceRepository();

            var updatedDevice = new Device
            {
                Id = 0,
                CreatedDate = DateTime.Now,
                Name = "Updated Name",
                IpAddress = "Updated IP Address",
            };
            var expectedDevicesToBeWritten = new List<Device>
            {
                updatedDevice,
                new Device{Id = 1,IpAddress = "127.0.0.1"},
                new Device{Id = 2,IpAddress = "127.0.0.1"},
                new Device{Id = 3,IpAddress = "127.0.0.1"},
                new Device{Id = 4,IpAddress = "127.0.0.1"},
            };
            mockCacheRepository.Setup(x => x.GetAll()).Returns(GenerateMultiple());

            // Act
            sut.Update(updatedDevice);
            //Assert
            mockCacheRepository.Verify(x => x.GetAll(), Times.Once);
            mockCacheRepository.Verify(x => x.Set(
                    It.IsAny<List<Device>>()
                ), Times.Once);
        }

        private static List<Device> GenerateMultiple()
        {
            List<Device> devices = new();
            for(int i=0;i <5; i++)
            {
                devices.Add(new Device
                {
                    Id = i,
                    IpAddress = "127.0.0.1"
                });
            }
            return devices;
        }
    }
}