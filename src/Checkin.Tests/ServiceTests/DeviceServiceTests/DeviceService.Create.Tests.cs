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
using AutoFixture;

namespace Checkin.Tests
{
    [TestClass]
    public class DeviceServiceAddTests
    {
        private Mock<IDeviceCacheRepository> mockDeviceRepository;

        private IMapper mapper;
        private Mock<ILogger> mockLogger;
        private DeviceService NewDeviceService() =>
            new(
                    mockDeviceRepository.Object,
                    mapper,
                    mockLogger.Object
                );

        private Device defaultDevice;
        private Device newDeviceDetails;

        [TestInitialize]
        public void SetUp()
        {
            mockDeviceRepository = new Mock<IDeviceCacheRepository>();
            mockLogger = new Mock<ILogger>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DeviceDtoToDeviceProfile>();
                cfg.AddProfile<DeviceToDeviceMergeProfile>();
                cfg.AddProfile<DeviceNetworkToDeviceNetworkDtoProfile>();
                cfg.AddProfile<DeviceNetworkToDeviceNetworkDtoProfile>();
            });
            mapper = mapperConfig.CreateMapper();
            Fixture deviceFixture = new Fixture();
            defaultDevice = deviceFixture.Create<Device>();

            newDeviceDetails = deviceFixture.Create<Device>();
        }

        [TestMethod]
        public void Add_WhenRepositoryReturnsNull_DeviceCreated_ReturnsSuccess()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns<List<Device>>(null);
            var sut = NewDeviceService();

            // Act
            var result = sut.CreateOrUpdate(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetByKey(It.IsAny<string>()), Times.Once);
            mockDeviceRepository.Verify(x => x.Set(It.IsAny<string>(),It.IsAny<Device>()), Times.Once);
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Add_WhenNoDevices_DeviceCreated_ReturnsSuccess()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(new List<Device>());
            var sut = NewDeviceService();

            // Act
            var result = sut.CreateOrUpdate(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetByKey(It.IsAny<string>()), Times.Once);
            mockDeviceRepository.Verify(x => x.Set(It.IsAny<string>(),It.IsAny<Device>()), Times.Once);
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Add_WhenExistingDevices_DeviceUpdated_ReturnsSuccess()
        {
            // Arrange
            //List<Device> devices = DeviceGenerationHelpers();

            mockDeviceRepository.Setup(x => x.GetByKey(It.IsAny<string>())).Returns(new Device());
            mockDeviceRepository.Setup(x => x.Set(It.IsAny<List<Device>>())).Returns(true);
            var sut = NewDeviceService();

            // Act
            var result = sut.CreateOrUpdate(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetByKey(It.IsAny<string>()), Times.Once);
            mockDeviceRepository.Verify(x => x.Set(It.IsAny<string>(),It.IsAny<Device>()), Times.Once);
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Add_WhenExistingDevices_ReturnsFailure()
        {
            // Arrange
            Device devices = new();
            mockDeviceRepository.Setup(x => x.GetByKey(It.IsAny<string>())).Returns(devices);
            mockDeviceRepository.Setup(x => x.Set(It.IsAny<string>(),It.IsAny<Device>())).Throws(new Exception());
            var sut = NewDeviceService();

            // Act
            var result = sut.CreateOrUpdate(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetByKey(It.IsAny<string>()), Times.Once);
            mockDeviceRepository.Verify(x => x.Set(It.IsAny<string>(),It.IsAny<Device>()), Times.Once);
            mockLogger.Verify(x => x.Fatal(It.IsAny<string>()), Times.Once);
            result.Should().BeFalse();
        }
    }
}