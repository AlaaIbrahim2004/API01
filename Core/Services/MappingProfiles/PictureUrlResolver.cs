using AutoMapper;
using Domain_Layer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DTO.ProductModule;

namespace Services.MappingProfiles
{
    public class PictureUrlResolver(IConfiguration _configration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            return $"{_configration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
