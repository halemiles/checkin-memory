using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkin.Repositories;
using Moq;
using System.Collections.Generic;
using Checkin.Models;
using System;
using FluentAssertions;
using Serilog;
using Checkin.Services.Interfaces;
using AutoFixture;
using System.Linq;

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