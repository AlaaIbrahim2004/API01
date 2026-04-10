using AutoMapper;
using Domain_Layer.Models.OrderModule;
using Shared.DTO.IdentityModule;
using Shared.DTO.OrderModule;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dist => dist.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dist => dist.ProductName, options => options.MapFrom(src => src.Product.ProductName))
                .ForMember(dist => dist.PictureUrl, options => options.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDto>();


        }
    }
}
