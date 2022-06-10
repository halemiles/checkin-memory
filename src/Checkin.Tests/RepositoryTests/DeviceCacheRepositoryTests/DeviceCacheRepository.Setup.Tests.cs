using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkin.Repositories;
using Moq;
using System.Collections.Generic;
using Checkin.Models;
using Serilog;
using Checkin.Services.Interfaces;
using AutoFixture;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using FluentAssertions;
using System.Collections.Specialized;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Checkin.Tests
{
    [TestClass]
    public partial class DeviceCacheRepositoryTests
    {
        private IMemoryCache cache;
        private string deviceCacheKey = "Devices";
        public DeviceCacheRepository NewDeviceCacheRepository() =>
            new(
                    cache
                );

        [TestInitialize]
        public void SetUp()
        {
            IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices(services => services.AddMemoryCache())
            .Build();

            cache = host.Services.GetRequiredService<IMemoryCache>();
        }

        

        public static List<Device> GenerateMultiple()
        {
            List<Device> devices = new();
            for(int i=0;i <5; i++)
            {
                devices.Add(new Device
                {
                    Id = i,
                    IpAddress = "127.0.0.1"
                });
            }
            return devices;
        }
    }
}