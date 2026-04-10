using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using ServicesAbstraction;

namespace Services
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(conf => conf.AddProfile(new ProductProfile()), typeof(Services.AssembleyRefrence).Assembly);
            Services.AddScoped<IServiceManager, ServiceManager>();
            return Services;
        }
    }
}
