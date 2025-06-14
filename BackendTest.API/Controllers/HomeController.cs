using BackendTest.Database.Services.Interfaces;
using BackendTest.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.API.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData()
        {
            IEnumerable<Product> data = await _productService.GetAllProductsAsync();

            return Ok(data);
        }
    }
}
