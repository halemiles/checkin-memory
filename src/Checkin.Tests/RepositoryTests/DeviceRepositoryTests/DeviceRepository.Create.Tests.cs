using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Checkin.Models;

namespace Checkin.Tests
{
    public partial class DeviceRepositoryTests
    {

        [TestMethod]
        public void Create_WithSingleDevice_VerifySetIsCalledOnce()
        {
            // Arrange                   
            var sut = NewDeviceRepository();
            // Act
            sut.Create(defaultDevices);

            //Assert
            mockCacheRepository.Verify(x => x.Set(
                    It.IsAny<List<Device>>()
                ), Times.Once);
        }

        [TestMethod]
        public void Create_WithMultipleDevices_VerifySetIsCalledOnce()
        {
            // Arrange                   
            var sut = NewDeviceRepository();
            // Act
            sut.Create(GenerateMultiple());

            //Assert            
            mockCacheRepository.Verify(x => x.Set(
                    It.IsAny<List<Device>>()
                ), Times.Once);
        }

        [TestMethod]
        public void Create_WithEmptyDeviceList_VerifySetIsCalledOnce()
        {
            // Arrange                   
            var sut = NewDeviceRepository();
            // Act
            sut.Create(new List<Device>());

            //Assert            
            mockCacheRepository.Verify(x => x.Set(
                    It.IsAny<List<Device>>()
                ), Times.Once);
        }
    }
}