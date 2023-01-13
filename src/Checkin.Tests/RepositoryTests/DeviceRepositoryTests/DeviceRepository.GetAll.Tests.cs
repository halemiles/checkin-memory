using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;

namespace Checkin.Tests
{
    [TestClass]
    public class DeviceRepositoryGetAllTests : DeviceServiceTestSetup
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