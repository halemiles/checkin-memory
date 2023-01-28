using System;
using AutoMapper;

namespace Checkin.Models
{
    public class DockerServiceDtoToDockerServiceProfile : Profile
    {
        public DockerServiceDtoToDockerServiceProfile()
        {
            CreateMap<DockerServiceDto, DockerService>().ReverseMap();
        }
    }
}