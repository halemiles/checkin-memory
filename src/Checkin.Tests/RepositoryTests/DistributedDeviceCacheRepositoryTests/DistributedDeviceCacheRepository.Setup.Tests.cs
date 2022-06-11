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
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Checkin.Tests
{
    [TestClass]
    public class DistributedDeviceCacheRepositorySetup
    {
        public IDistributedCache distributedCache;
        public Mock<ILogger> mockLogger;
        public string deviceCacheKey = "Devices";
        public DistributedDeviceCacheRepository NewDistributedDeviceCacheRepository() =>
            new(
                    distributedCache,
                    mockLogger.Object
                );

        [TestInitialize]
        public void SetUp()
        {
            var opts = Options.Create<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions());
            
            distributedCache = new MemoryDistributedCache(opts);
            mockLogger = new Mock<ILogger>();
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