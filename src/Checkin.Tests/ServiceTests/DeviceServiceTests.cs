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
    public class DeviceServiceTests
    {
        private Mock<IDeviceRepository> mockDeviceRepository;
        private Mock<IMapper> mockMapper;
        private DeviceService NewDnsService() =>
            new DeviceService(
                    mockDeviceRepository.Object,
                    mockMapper.Object
                );
           
        private Device defaultDevice;

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
        }

        [TestMethod]
        public void Add_WhenNoPings_ReturnsFailure()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(new List<Device>());
            var sut = NewDnsService();

            // Act
            sut.Add(defaultDevice);

            //Assert
            mockDeviceRepository.Verify(x => x.GetAll(), Times.Once);
            mockDeviceRepository.Verify(x => x.Create(It.IsAny<List<Device>>()), Times.Once);
        }

        [TestMethod]
        public void GetAll_WithMultiplePings_ReturnsPings()
        {
            // Arrange
            mockDeviceRepository.Setup(x => x.GetAll()).Returns(GenerateMultiple());
            var sut = NewDnsService();

            // Act
            var results = sut.GetAll();

            //Assert
            results.Count.Should().Be(5);
            mockDeviceRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetAll_WithNullPings_ReturnsEmptyResult()
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

        private static List<Device> GenerateMultiple()
        {
            List<Device> devices = new List<Device>();
            for(int i=0;i <5; i++)
            {
                devices.Add(new Device()
                {
                    Id = 0,
                    CreatedDate = DateTime.Now,
                    IpAddress = "127.0.0.1"
                });
            }
            return devices;
        }
    }
}