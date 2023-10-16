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
using System.Threading.Tasks;

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

            mockLogger.Setup(x => x.Information(It.IsAny<string>())).Verifiable();
            mockLogger.Setup(x => x.ForContext(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>())).Returns(mockLogger.Object);
        }

        [TestMethod]
        public async Task Add_WhenRepositoryReturnsNull_DeviceCreated_ReturnsSuccess()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetByKey(It.IsAny<string>())).ReturnsAsync(new Device());
            var sut = NewDeviceService();

            // Act
            var result = await sut.CreateOrUpdate(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetByKey(It.IsAny<string>()), Times.Once);
            mockDeviceRepository.Verify(x => x.Set(It.IsAny<string>(),It.IsAny<Device>()), Times.Once);
            result.Payload.Should().BeTrue();
        }

        [TestMethod]
        public async Task Add_WhenNoDevices_DeviceCreated_ReturnsSuccess()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetByKey(It.IsAny<string>())).ReturnsAsync(new Device());
            var sut = NewDeviceService();

            // Act
            var result = await sut.CreateOrUpdate(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetByKey(It.IsAny<string>()), Times.Once);
            mockDeviceRepository.Verify(x => x.Set(It.IsAny<string>(),It.IsAny<Device>()), Times.Once);
            result.Payload.Should().BeTrue();
        }

        [TestMethod]
        public async Task Add_WhenExistingDevices_DeviceUpdated_ReturnsSuccess()
        {
            // Arrange
            //List<Device> devices = DeviceGenerationHelpers();

            mockDeviceRepository.Setup(x => x.GetByKey(It.IsAny<string>())).ReturnsAsync(new Device());
            mockDeviceRepository.Setup(x => x.Set(It.IsAny<List<Device>>())).Returns(true);
            var sut = NewDeviceService();

            // Act
            var result = await sut.CreateOrUpdate(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetByKey(It.IsAny<string>()), Times.Once);
            mockDeviceRepository.Verify(x => x.Set(It.IsAny<string>(),It.IsAny<Device>()), Times.Once);
            result.Payload.Should().BeTrue();
        }

        [TestMethod]
        public async Task Add_WhenExistingDevices_ReturnsFailure()
        {
            // Arrange
            Device devices = new();
            mockDeviceRepository.Setup(x => x.GetByKey(It.IsAny<string>())).ReturnsAsync(devices);
            mockDeviceRepository.Setup(x => x.Set(It.IsAny<string>(),It.IsAny<Device>())).Throws(new Exception());
            var sut = NewDeviceService();

            // Act
            var result = await sut.CreateOrUpdate(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetByKey(It.IsAny<string>()), Times.Once);
            mockDeviceRepository.Verify(x => x.Set(It.IsAny<string>(),It.IsAny<Device>()), Times.Once);
            mockLogger.Verify(x => x.Fatal(It.IsAny<string>()), Times.Once);
            result.Payload.Should().BeFalse();
        }
    }
}