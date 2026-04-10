using AutoMapper;
using Domain_Layer.Models.BasketModule;
using Shared.DTO.BasketModule;

namespace Services.MappingProfiles
{
    public class BasketProfile : Profile
    {

        public BasketProfile()
        {
            CreateMap<Basket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
