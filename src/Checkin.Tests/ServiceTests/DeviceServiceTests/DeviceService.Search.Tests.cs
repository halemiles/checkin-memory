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
using Snapper;
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
                Id = 0,
                CreatedDate = DateTime.Now,
                Name = "Test Device",
                IpAddress = "127.0.0.1"
            };
        }

        [TestMethod]
        public void GetAll_WhenNoDevicesExist_ReturnsEmptyResults()
        {
            // Arrange
            var sut = NewDeviceService();

            // Act
            var results = sut.Search(null, string.Empty);

            //Assert
            results.Count.Should().Be(0);
        }

        [TestMethod]
        public void GetAll_WhenDevicesExist_WithCorrectSearchParams_ReturnsOneResult()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.Search(It.IsAny<int?>(), It.IsAny<string>())).Returns(new List<Device>{ defaultDevice });
            var sut = NewDeviceService();

            // Act
            var results = sut.Search(0, "192.168.0.0");

            //Assert
            results.Count.Should().Be(1);
        }
    }
}