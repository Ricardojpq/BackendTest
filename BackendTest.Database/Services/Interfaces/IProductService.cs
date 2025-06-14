using BackendTest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTest.Database.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id); 
        Task<Product> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product); 
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<Product>> SearchProductsByNameAsync(string name);
    }
}
