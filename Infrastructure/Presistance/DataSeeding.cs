using Domain_Layer.Contracts;
using Domain_Layer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Presistance.Data.Contexts;
using System.Text.Json;

namespace Presistance
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var productBrandsData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeed\brands.json");
                    var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);
                    if (productBrands is not null && productBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(productBrands);
                    }
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypesData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeed\types.json");
                    var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypesData);
                    if (productTypes is not null && productTypes.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(productTypes);
                    }
                }

                if (!_dbContext.Products.Any())
                {
                    var productData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeed\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productData);
                    if (products is not null && products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(products);
                    }
                }
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
