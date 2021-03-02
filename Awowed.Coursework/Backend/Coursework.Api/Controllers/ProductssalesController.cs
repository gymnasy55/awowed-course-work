using System.Collections.Generic;
using Coursework.Api.Domain;
using Coursework.Api.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coursework.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductssalesController : ControllerBase
    {
        private readonly ILogger<ProductssalesController> _logger;

        private readonly IProductssalesRepository _productssales;

        public ProductssalesController(ILogger<ProductssalesController> logger, IProductssalesRepository productssales)
        {
            _productssales = productssales;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Productssale> Get()
        {
            _logger.LogInformation("Someone get products sales");
            return _productssales.GetProductssales();
        }
    }
}