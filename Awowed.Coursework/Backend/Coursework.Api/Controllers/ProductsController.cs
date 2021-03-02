using System.Collections.Generic;
using Coursework.Api.Domain;
using Coursework.Api.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coursework.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        private readonly IProductsRepository _products;

        public ProductsController(ILogger<ProductsController> logger, IProductsRepository products)
        {
            _products = products;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            _logger.LogInformation("Someone get products");
            return _products.GetProducts();
        }
    }
}