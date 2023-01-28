using System;
using System.Collections.Generic;
using Checkin.Models;
using AutoFixture;

namespace Checkin.Tests.Helpers
{
    public static class DeviceGenerationHelpers
    {
        public static List<Device> GenerateMultiple()
        {
            List<Device> devices = new();
            for(int i=0;i <5; i++)
            {
                devices.Add(new Device
                {
                    Id =  Guid.Empty,
                    CreatedDate = new DateTime(2022,1,1),
                    IpAddress = $"192.168.0.{i}",
                    Name=$"Device {i}",
                    ExternalNetwork = new DeviceNetwork
                    {
                        ExternalIpAddress = $"1.2.3.{i}",
                        IspName = "Major ISP",
                        LastModified = new DateTime(2022,1,1),
                        DateCreated = new DateTime(2022,1,1)
                    },
                    Services = new DeviceServices
                    {
                        DockerServices = new List<DockerService>
                        {                        
                            new DockerService() { ContainerName = "Docker", Port=123}
                        }
                    },
                    Power = new DevicePower
                    {
                        Batteries = new List<DeviceBattery>
                        {
                            new DeviceBattery
                            {
                                Name = "Battery 1",
                                BatteryLevel = 100
                            }
                        }
                    }
                });
            }
            return devices;
        }
    }
}