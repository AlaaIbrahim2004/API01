using E_Commerce.Web.Exctentions;
using Presistance;
using Services;
namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services to the container
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            #region Add Authorization to Swagger

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "E-Commerce API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter JWT token like: Bearer {your token here}"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            #endregion

            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();
            builder.Services.AddJWTServices(builder.Configuration);
            #endregion

            #region Data Seeding

            var app = builder.Build();
            await app.SeedDatAsynca();
            #endregion

            #region Configer http request pipeline

            // Configure the HTTP request pipeline.
            //////app.Use(async (RequestContext, NextMiddleWare) =>
            //////{
            //////    Console.WriteLine("Request Under Processing");
            //////    await NextMiddleWare.Invoke();
            //////    Console.WriteLine("Waiting Response");
            //////    Console.WriteLine(RequestContext.Response.Body);
            //////});

            app.UseCustomExceptionMiddleWare();
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwaggerMiddleWare();
            //}
            app.UseSwaggerMiddleWare();
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            #endregion

            app.Run();

        }
    }
}
