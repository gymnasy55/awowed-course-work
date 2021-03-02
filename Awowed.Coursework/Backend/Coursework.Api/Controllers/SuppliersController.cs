using Coursework.Api.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Coursework.Api.Domain;

namespace Coursework.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ILogger<SuppliersController> _logger;

        private readonly ISuppliersRepository _suppliers;

        public SuppliersController(ILogger<SuppliersController> logger, ISuppliersRepository suppliers)
        {
            _logger = logger;
            _suppliers = suppliers;
        }

        [HttpGet]
        public IEnumerable<Supplier> Get()
        {
            _logger.LogInformation("Someone get suppliers info");
            return _suppliers.GetSuppliers();
        }
    }
}