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
            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();

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
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();
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
