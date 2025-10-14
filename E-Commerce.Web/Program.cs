using Domain_Layer.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Data.Contexts;
using Presistance.Repositories;
using Services;
using Services.MappingProfiles;
using ServicesAbstraction;
namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Services to the container
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(conf => conf.AddProfile(new ProductProfile()), typeof(Services.AssembleyRefrence).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();


            #endregion

            #region Data Seeding

            var app = builder.Build();

            var Scope = app.Services.CreateScope();

            var seed = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            seed.DataSeedAsync();
            #endregion

            #region Configer http request pipeline

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseAuthorization();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
