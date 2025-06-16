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
            //data = JsonSerializer.Deserialize<DataModelJson>(await File.ReadAllTextAsync(seedFileLocation));
            List<Product> dataProducts = new List<Product>
            {
                new Product
                {
                    Name = "PC",
                    Price = 14.1m,
                    Stock = 1,
                    Description = "PC GAMER"
                },
                new Product
                {
                    Name = "Monitor",
                    Price = 250.00m,
                    Stock = 5,
                    Description = "Monitor de 27 pulgadas, 144Hz"
                },
                new Product
                {
                    Name = "Teclado Mecánico",
                    Price = 85.75m,
                    Stock = 12,
                    Description = "Teclado RGB con switches azules"
                },
                new Product
                {
                    Name = "Mouse Gaming",
                    Price = 55.00m,
                    Stock = 8,
                    Description = "Ratón ergonómico con sensor de alta precisión"
                },
                new Product
                {
                    Name = "Auriculares Inalámbricos",
                    Price = 120.00m,
                    Stock = 7,
                    Description = "Cascos con cancelación de ruido y sonido envolvente"
                },
                new Product
                {
                    Name = "Webcam HD",
                    Price = 45.99m,
                    Stock = 15,
                    Description = "Cámara web para streaming y videollamadas"
                },
                new Product
                {
                    Name = "Disco Duro SSD 1TB",
                    Price = 99.50m,
                    Stock = 10,
                    Description = "Almacenamiento de estado sólido de alta velocidad"
                },
                new Product
                {
                    Name = "Tarjeta Gráfica RTX 4060",
                    Price = 499.99m,
                    Stock = 3,
                    Description = "Potente tarjeta gráfica para juegos de última generación"
                },
                new Product
                {
                    Name = "Silla Gamer",
                    Price = 180.00m,
                    Stock = 4,
                    Description = "Silla ergonómica con soporte lumbar"
                },
                new Product
                {
                    Name = "Impresora Multifuncional",
                    Price = 175.20m,
                    Stock = 6,
                    Description = "Impresora, escáner y copiadora con Wi-Fi"
                },
                new Product
                {
                    Name = "Case Gamer",
                    Price = 200m,
                    Stock = 0,
                    Description = "Case Gamer con lucer RGB"
                }
            };

            //if (data != null)
            //{
            await SeedProducts(context, dataProducts);
            //}
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
