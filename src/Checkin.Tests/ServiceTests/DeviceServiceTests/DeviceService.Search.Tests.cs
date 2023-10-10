using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Checkin.Services;
using System.Collections.Generic;
using Checkin.Models;
using System;
using FluentAssertions;
using AutoMapper;
using Serilog;
using Checkin.Services.Interfaces;

namespace Checkin.Tests
{
    [TestClass]
    public class DeviceServiceSearchTests
    {
        private Mock<IDeviceCacheRepository> mockDeviceRepository;
        private Mock<IMapper> mockMapper;
        private Mock<ILogger> mockLogger;
        private DeviceService NewDeviceService() =>
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

            defaultDevice = new Device
            {
                Id =  Guid.Empty,
                CreatedDate = DateTime.Now,
                Name = "Test Device",
                IpAddress = "127.0.0.1"
            };
        }

        [TestMethod]
        [Ignore("Mock logger is broken")]
        public void GetAll_WhenNoDevicesExist_ReturnsEmptyResults()
        {
            // Arrange
            var sut = NewDeviceService();

            // Act
            var results = sut.Search(new SearchDto());

            //Assert
            results.Payload.Count.Should().Be(0);
        }

        [TestMethod]
        [Ignore("Mock logger is broken")]
        public void GetAll_WhenDevicesExist_WithCorrectSearchParams_ReturnsOneResult()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.Search(It.IsAny<SearchDto>()))
                .Returns(new List<Device>{ defaultDevice });
            var sut = NewDeviceService();

            // Act
            var results = sut.Search(new SearchDto());
            
            //Assert
            results.Payload.Count.Should().Be(1);
        }
    }
}