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
        public void GetAll_WithExistingDevice_VerifyCacheReturnsRecords()
        {
            // Arrange  
            var sut = NewDeviceRepository();
            mockCacheRepository.Setup(x => x.GetAll()).Returns(GenerateMultiple());

            // Act
            var result = sut.GetAll();
            //Assert

            mockCacheRepository.Verify(x => x.GetAll(), Times.Once);
            result.Count.Should().Be(5);
        }
    }
}