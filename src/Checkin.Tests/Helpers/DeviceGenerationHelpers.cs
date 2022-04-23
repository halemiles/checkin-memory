using System;
using System.Collections.Generic;
using Checkin.Models;

namespace Checkin.Tests.Helpers
{
    public static class DeviceGenerationHelpers
    {
        public static List<Device> GenerateMultiple()
        {
            List<Device> devices = new List<Device>();
            for(int i=0;i <5; i++)
            {
                devices.Add(new Device()
                {
                    Id = 0,
                    CreatedDate = DateTime.Now,
                    IpAddress = $"192.168.0.{i}",
                    ExternalNetwork = new DeviceNetwork()
                    {
                        ExternalIpAddress = "1.2.3.4",
                        DomainName = "test.domain",
                        IspName = "Major ISP",
                        LastModified = new DateTime(2022,1,1),
                        DateCreated = new DateTime(2022,1,1)
                    },
                    Services = new List<ServiceStatus>()
                    {
                        new ServiceStatus()
                        {
                            Name = "Docker",
                            Status = DeviceServiceStatus.UP
                        }
                    }
                });
            }
            return devices;
        }
    }
}