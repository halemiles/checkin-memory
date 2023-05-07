using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkin.Repositories;
using Moq;
using Checkin.Services;
using System.Collections.Generic;
using Checkin.Models;
using System;
using FluentAssertions;
using Serilog;
using Checkin.Services.Interfaces;
using AutoMapper;
using Checkin.Tests.Helpers;

namespace Checkin.Tests
{
    [TestClass]
    public class DeviceServiceCtorTests
    {
        private Mock<IDeviceCacheRepository> mockDeviceRepository;
        private Mock<IMetricsService> mockMetricsService;
        private IMapper mapper;
        private Mock<ILogger> mockLogger;

        [TestInitialize]
        public void SetUp()
        {
            mockDeviceRepository = new Mock<IDeviceCacheRepository>();
            mockMetricsService = new Mock<IMetricsService>();
            mockMetricsService.Setup(x => x.RecordDeviceMetrics(It.IsAny<Device>())).Verifiable();
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DeviceDtoToDeviceProfile>();
                cfg.AddProfile<DeviceToDeviceMergeProfile>();
                cfg.AddProfile<DeviceNetworkToDeviceNetworkDtoProfile>();
                cfg.AddProfile<DeviceNetworkToDeviceNetworkDtoProfile>();
            }));
            mockLogger = new Mock<ILogger>();
        }

        [TestMethod]
        public void Ctor_WhenNoNullParams_ReturnsSuccess()
        {
            // Arrange
            Action sut = () => new DeviceService(
                mockDeviceRepository.Object,
                mockMetricsService.Object,
                mapper,
                mockLogger.Object
            );

            // Act

            //Assert
            sut.Should().NotThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void Ctor_WhenNullFirstParam_Throws()
        {
            // Arrange
            Action sut = () => new DeviceService(
                null,
                mockMetricsService.Object,
                mapper,
                mockLogger.Object
            );

            // Act

            //Assert
            sut.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Ctor_WhenNullSecondParam_Throws()
        {
            // Arrange
            Action sut = () => new DeviceService(
                mockDeviceRepository.Object,
                mockMetricsService.Object,
                null,
                mockLogger.Object
            );

            // Act

            //Assert
            sut.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Ctor_WhenNoNullThirdParam_Throws()
        {
            // Arrange
            Action sut = () => new DeviceService(
                mockDeviceRepository.Object,
                mockMetricsService.Object,
                mapper,
                null
            );

            // Act

            //Assert
            sut.Should().Throw<ArgumentNullException>();
        }
    }
}