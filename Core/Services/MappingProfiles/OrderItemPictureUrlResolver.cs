using AutoMapper;
using Domain_Layer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DTO.OrderModule;

namespace Services.MappingProfiles
{
    public class OrderItemPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return string.Empty;
            }
            return $"{configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";

        }
    }
}
