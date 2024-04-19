using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talapat.DAL.Entities;
using Talapat.DAL.Entities.Order_Aggregate;

namespace Talapat.DAL.Contexts
{
	public class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext context,ILoggerFactory loggerFactory)
		{
			try
			{
				if (!context.ProductBrands.Any())
				{
					var brandsData = File.ReadAllText("../Talapat.DAL/Entities/SeedData/brands.json");
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
					foreach (var brand in brands)
					{
						context.Set<ProductBrand>().Add(brand);
					}
					await context.SaveChangesAsync();
				}

				if (!context.ProductTypes.Any())
				{
					var TypesData = File.ReadAllText("../Talapat.DAL/Entities/SeedData/types.json");
					var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
					foreach (var Type in Types)
					{
						context.Set<ProductType>().Add(Type);
					}
					await context.SaveChangesAsync();
				}
				if (!context.Products.Any())
				{
					var ProductsData = File.ReadAllText("../Talapat.DAL/Entities/SeedData/products.json");
					var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
					foreach (var Product in Products)
					{
						context.Set<Product>().Add(Product);
					}
					await context.SaveChangesAsync();
				}
                if (!context.DeliveryMethods.Any())
                {
                    var DeliverMethodData = File.ReadAllText("../Talapat.DAL/Entities/SeedData/delivery.json");
                    var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliverMethodData);
                    foreach (var Product in DeliveryMethods)
                    {
                        context.Set<DeliveryMethod>().Add(Product);
                    }
                    await context.SaveChangesAsync();
                }


            }
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<StoreContextSeed>();
				logger.LogError(ex, ex.Message);
			}
			
		}
	}
}
