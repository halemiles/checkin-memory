using System;
using AutoMapper;

namespace Checkin.Models
{
    public class DockerServiceDtoToDockerServiceProfile : Profile
    {
        public DockerServiceDtoToDockerServiceProfile()
        {
            CreateMap<DockerServiceDto, DockerService>()
                .ForMember(dest => dest.ContainerName, opt => opt.MapFrom(src => src.ContainerName))
                .ForMember(dest => dest.Port, opt => opt.MapFrom(src => src.Port));
        }
    }
}