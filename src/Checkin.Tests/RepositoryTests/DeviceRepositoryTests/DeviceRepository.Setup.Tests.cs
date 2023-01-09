using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkin.Repositories;
using Moq;
using System.Collections.Generic;
using Checkin.Models;
using Serilog;
using Checkin.Services.Interfaces;
using AutoFixture;
using System.Linq;
using System;

namespace Checkin.Tests
{
    [TestClass]
    public class DeviceServiceTestSetup
    {
        public Mock<IDeviceCacheRepository> mockCacheRepository;
        public Mock<ILogger> mockLogger;
        public LocalDeviceRepository NewDeviceRepository() =>
            new(
                    mockCacheRepository.Object,
                    mockLogger.Object
                );

        public List<Device> defaultDevices;

        [TestInitialize]
        public void SetUp()
        {
            mockCacheRepository = new Mock<IDeviceCacheRepository>();
            mockLogger = new Mock<ILogger>();
            Fixture defaultDeviceFixture = new();
            defaultDevices = defaultDeviceFixture.CreateMany<Device>(10).ToList();
        }
        
        public static List<Device> GenerateMultiple()
        {
            List<Device> devices = new();
            for(int i=0;i <5; i++)
            {
                devices.Add(new Device
                {
                    Id = Guid.Empty,
                    IpAddress = "127.0.0.1"
                });
            }
            return devices;
        }
    }
}