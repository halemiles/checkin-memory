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
    public class DeviceServiceGetAllTests
    {
        private Mock<IDeviceCacheRepository> mockDeviceRepository;
        private Mock<IMetricsService> mockMetricsService;
        private Mock<IMapper> mockMapper;
        private Mock<ILogger> mockLogger;
        private DeviceService NewDeviceService() =>
            new(
                    mockDeviceRepository.Object,
                    mockMetricsService.Object,
                    mockMapper.Object,
                    mockLogger.Object
                );

        private Device defaultDevice;

        [TestInitialize]
        public void SetUp()
        {
            mockDeviceRepository = new Mock<IDeviceCacheRepository>();
            mockMetricsService = new Mock<IMetricsService>();
            mockMetricsService.Setup(x => x.RecordDeviceMetrics(It.IsAny<Device>())).Verifiable();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILogger>();

            defaultDevice = new Device
            {
                Id = Guid.Empty,
                CreatedDate = DateTime.Now,
                Name = "Test Device",
                IpAddress = "127.0.0.1"
            };
        }

        [TestMethod]
        public void GetAll_WhenMultipleDevicesReturned_ReturnsMultipleDevices()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(DeviceGenerationHelpers.GenerateMultiple());
            var sut = NewDeviceService();

            // Act
            var results = sut.GetAll();

            //Assert
            results.Payload.Count.Should().Be(5);
            mockDeviceRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetAll_WhenRepositoryReturnsNull_ReturnsEmptyResult()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns((List<Device>)null);
            var sut = NewDeviceService();

            // Act
            var results = sut.GetAll();

            //Assert
            results.Should().NotBeNull();
            results.Payload.Count.Should().Be(0);
        }

        [TestMethod]
        public void GetAll_WhenMultipleDevicesReturned_MatchesSnapshot()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(DeviceGenerationHelpers.GenerateMultiple());
            var sut = NewDeviceService();

            // Act
            var results = sut.GetAll();

            //Assert
            results.Payload.ShouldMatchSnapshot();
        }

        [TestMethod]
        public void GetAll_WhenRepositoryReturnsNull_MatchesSnapshot()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns((List<Device>)null);
            var sut = NewDeviceService();

            // Act
            var results = sut.GetAll();

            //Assert
            results.Payload.ShouldMatchSnapshot();
        }
    }
}