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
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Checkin.Tests
{
    [TestClass]
    public class DeviceServiceUpdateTests
    {
        private Mock<IDeviceRepository> mockDeviceRepository;
        private Mock<IMapper> mockMapper;
        private DeviceService NewDeviceService() =>
            new DeviceService(
                    mockDeviceRepository.Object,
                    mockMapper.Object
                );
           
        private Device defaultDevice;
        private Device newDeviceDetails;

        [TestInitialize]
        public void SetUp()
        {
            mockDeviceRepository = new Mock<IDeviceRepository>();
            mockMapper = new Mock<IMapper>();

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
        public void Update_WithUpdatedProperties_VerifyCalledMethods()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(new List<Device>(){defaultDevice});
            var sut = NewDeviceService();

            // Act
            sut.Update(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetAll(), Times.Once);
            mockDeviceRepository.Verify(x => x.Update(It.IsAny<Device>()), Times.Once);
        }

        [TestMethod]
        public void Update_WithUpdatedIpAddress_IpAddressIsUpdated()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(new List<Device>(){defaultDevice});
            var sut = NewDeviceService();

            // Act
            sut.Update(defaultDevice);

            //Assert
            mockMapper.Verify(x => x.Map(It.IsAny<Device>(), It.IsAny<Device>()), Times.Once);
            mockDeviceRepository.Verify(x => x.Update(It.IsAny<Device>()), Times.Once);
        }

        private static List<Device> GenerateMultiple()
        {
            List<Device> devices = new List<Device>();
            for(int i=0;i <5; i++)
            {
                devices.Add(new Device()
                {
                    Id = 0,
                    CreatedDate = DateTime.Now,
                    IpAddress = $"192.168.0.{i}"
                });
            }
            return devices;
        }
    }
}