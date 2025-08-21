using AutoMapper;
using BrandIT.Application.DTOs.Auth;
using BrandIT.Application.DTOs.Contact;
using BrandIT.Application.DTOs.Services;
using BrandIT.Domain.Entities;

namespace BrandIT.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserDto>();

        // Service mappings
        CreateMap<Service, ServiceDto>();
        CreateMap<CreateServiceRequest, Service>();
        CreateMap<UpdateServiceRequest, Service>();

        // Contact Message mappings
        CreateMap<ContactMessage, ContactMessageDto>()
            .ForMember(dest => dest.AssignedToUser, opt => opt.MapFrom(src => src.AssignedToUser));
        CreateMap<CreateContactMessageRequest, ContactMessage>();
        CreateMap<UpdateContactMessageRequest, ContactMessage>();
    }
}
