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

            defaultDevice = new Device()
            {
                Id = 0,
                CreatedDate = DateTime.Now,
                Name = "Test Device",
                IpAddress = "127.0.0.1"
            };

            newDeviceDetails = new Device()
            {
                Id = 0,
                CreatedDate = DateTime.Now,
                Name = "Updated Name",
                IpAddress = "192.168.0.195"
            };
        }

        [TestMethod]
        public void Add_WhenNoDevices_ReturnsFailure()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(new List<Device>());
            var sut = NewDeviceService();

            // Act
            sut.Add(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetAll(), Times.Once);
            mockDeviceRepository.Verify(x => x.Set(It.IsAny<List<Device>>()), Times.Once);
        }

        [TestMethod]
        public void Add_WhenExistingDevices_ReturnsFailure()
        {
            // Arrange
            List<Device> devices = DeviceGenerationHelpers.GenerateMultiple();
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(devices);
            var sut = NewDeviceService();

            // Act
            sut.Add(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetAll(), Times.Exactly(2));
            mockDeviceRepository.Verify(x => x.Set(It.IsAny<List<Device>>()), Times.Once);
        }
    }
}