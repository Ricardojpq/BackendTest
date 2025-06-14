using BackendTest.Database.Persistence.Models;
using BackendTest.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace BackendTest.Database.Persistence.SeedData
{
    public class DbInitializer
    {
        private static ILogger<DbInitializer> _logger;

        public static async Task Initialize(ApplicationDbContext context, IConfiguration configuration)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
            _logger = loggerFactory.CreateLogger<DbInitializer>();

            DataModelJson? data = null;

            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var seedFileLocation = currentDirectory + "\\Persistence\\SeedData\\Data\\Data.json";
            data = JsonSerializer.Deserialize<DataModelJson>(await File.ReadAllTextAsync(seedFileLocation));

            if (data != null)
            {
                await SeedProducts(context, data.Products);
            }
        }
        #region SeedData
        public static async Task SeedProducts(ApplicationDbContext context, IEnumerable<Product> data)
        {
            try
            {
                var ProductList = await context.Products.AsNoTracking().ToListAsync();

                var newEntities = data.Where(x => !ProductList.Any(y => x.Name == y.Name)).ToList();

                if (newEntities.Any())
                {
                    await context.Products.AddRangeAsync(newEntities);
                    await context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogInformation("No se encontraron nuevos Products para insertar.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error durante el proceso de seeding de Products.");
            }
        }

        #endregion
    }
}
