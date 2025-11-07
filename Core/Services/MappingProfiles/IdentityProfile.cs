using AutoMapper;
using Domain_Layer.Models.IdentityModule;
using Shared.DTO.IdentityModule;

namespace Services.MappingProfiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
