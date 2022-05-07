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

namespace Checkin.Tests
{
    [TestClass]
    public class DeviceServiceGetAllTests
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

        [TestMethod]
        [Ignore("Issue with snapshot updating")]
        public void GetAll_WhenMultipleDevicesReturned_ ()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(DeviceGenerationHelpers.GenerateMultiple());
            var sut = NewDnsService();

            // Act
            var results = sut.GetAll();

            //Assert
            results.ShouldMatchSnapshot();
        }

        [TestMethod]
        [Ignore("Issue with snapshot updating")]
        public void GetAll_WhenRepositoryReturnsNull_MatchesSnapshot()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns((List<Device>)null);
            var sut = NewDnsService();

            // Act
            var results = sut.GetAll();

            //Assert
            results.ShouldMatchSnapshot();
        }
    }
}