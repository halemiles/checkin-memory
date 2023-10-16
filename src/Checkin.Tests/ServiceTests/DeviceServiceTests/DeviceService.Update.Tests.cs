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
using System.Threading.Tasks;

namespace Checkin.Tests
{
    [TestClass]
    public class DeviceServiceUpdateTests
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

            mockLogger.Setup(x => x.Information(It.IsAny<string>())).Verifiable();
            mockLogger.Setup(x => x.ForContext(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>())).Returns(mockLogger.Object);

            defaultDevice = new Device
            {
                Id = new Guid("11e1c0fc-95ac-4cfb-9869-de581d4708af"),
                CreatedDate = new DateTime(2000,1,1),
                Name = "TestDevice",
                IpAddress = "127.0.0.1"
            };
        }

        [TestMethod]
        public async Task Update_WhenNoDevices_CreatesNew()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetByKey(It.IsAny<string>())).ReturnsAsync(new Device());
            var sut = NewDeviceService();

            // Act
            await sut.CreateOrUpdate(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetByKey(It.IsAny<string>()), Times.Once);
            mockDeviceRepository.Verify(x => x.Set(It.IsAny<string>(),It.IsAny<Device>()), Times.Once);
        }

        [TestMethod]
        public async Task Update_WhenDeviceExists_ExistingIsUpdated()
        {
            // Arrange
            var existingDevices = new List<Device>{
                new Device
                {
                    Id =  Guid.Empty,
                    CreatedDate = new DateTime(2000,1,1),
                    Name = "Test Device",
                    IpAddress = "127.0.0.2"
                }
            };

            var expectedDevices = new List<Device>() {defaultDevice};
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(existingDevices);
            var sut = NewDeviceService();

            // Act
            await sut.CreateOrUpdate(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetByKey(It.IsAny<string>()), Times.Once);
            //mockMapper.Verify(x => x.Map(It.IsAny<Device>(), It.IsAny<Device>()), Times.Once);            
        }

        [TestMethod]
        [Ignore("Needs more thought")]
        public async Task Update_WhenDeviceExists_DeviceChangesAreMerged()
        {
            // Arrange
            var existingDevices = new List<Device>{
                new Device
            {
                Id =  Guid.Empty,
                CreatedDate = new DateTime(2000,1,1),
                Name = "Test Device",
                IpAddress = "127.0.0.2"
            }
            };

            var expectedDevices = new List<Device>() {defaultDevice};
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(existingDevices);
            var sut = NewDeviceService();

            // Act
            await sut.CreateOrUpdate(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.Set(expectedDevices), Times.Once);
        }
    }
}