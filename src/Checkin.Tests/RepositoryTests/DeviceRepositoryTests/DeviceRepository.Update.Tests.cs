using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkin.Repositories;
using Moq;
using System.Collections.Generic;
using Checkin.Models;
using System;

namespace Checkin.Tests
{
    public partial class DeviceRepositoryTests
    {

        [TestMethod]
        public void Update_WithExistingDevice_VerifyCacheUpdated()
        {
            // Arrange  
            var sut = NewDeviceRepository();

            var updatedDevice = new Device
            {
                Id = 0,
                CreatedDate = DateTime.Now,
                Name = "Updated Name",
                IpAddress = "Updated IP Address",
            };
            mockCacheRepository.Setup(x => x.GetAll()).Returns(GenerateMultiple());

            // Act
            sut.Update(updatedDevice);
            //Assert
            mockCacheRepository.Verify(x => x.GetAll(), Times.Once);
            mockCacheRepository.Verify(x => x.Set(
                    It.IsAny<List<Device>>()
                ), Times.Once);
        }
    }
}