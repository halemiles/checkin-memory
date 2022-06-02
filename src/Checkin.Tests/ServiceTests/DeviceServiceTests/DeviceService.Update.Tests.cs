using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkin.Repositories;
using Moq;
using Checkin.Services;
using System.Collections.Generic;
using Checkin.Models;
using System;
using FluentAssertions;
using AutoMapper;
using Checkin.Tests.Helpers;
using Serilog;
using Checkin.Services.Interfaces;

namespace Checkin.Tests
{
    [TestClass]
    public class DeviceServiceUpdateTests
    {
        private Mock<IDeviceCacheRepository> mockDeviceRepository;
        private Mock<IMapper> mockMapper;
        private Mock<ILogger> mockLogger;
        private DeviceService NewDnsService() =>
            new(
                    mockDeviceRepository.Object,
                    mockMapper.Object,
                    mockLogger.Object
                );

        private Device defaultDevice;

        [TestInitialize]
        public void SetUp()
        {
            mockDeviceRepository = new Mock<IDeviceCacheRepository>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILogger>();

            defaultDevice = new Device()
            {
                Id = 0,
                CreatedDate = DateTime.Now,
                Name = "Test Device",
                IpAddress = "127.0.0.1"
            };
        }

        [TestMethod]
        public void Add_WhenNoPings_ReturnsFailure()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(new List<Device>());
            var sut = NewDnsService();

            // Act
            sut.Add(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetAll(), Times.Once);
            mockDeviceRepository.Verify(x => x.Set(It.IsAny<List<Device>>()), Times.Once);
        }

        [TestMethod]
        public void GetAll_WithMultiplePings_ReturnsPings()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(DeviceGenerationHelpers.GenerateMultiple());
            var sut = NewDnsService();

            // Act
            var results = sut.GetAll();

            //Assert
            results.Count.Should().Be(5);
            mockDeviceRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetAll_WhenRepositoryReturnsNull_ReturnsEmptyResult()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns((List<Device>)null);
            var sut = NewDnsService();

            // Act
            var results = sut.GetAll();

            //Assert
            results.Should().NotBeNull();
            results.Count.Should().Be(0);
        }
    }
}