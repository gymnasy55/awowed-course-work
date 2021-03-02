using System.Collections.Generic;
using Coursework.Api.Domain;
using Coursework.Api.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coursework.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetalsController : ControllerBase
    {
        private readonly ILogger<MetalsController> _logger;

        private readonly IMetalsRepository _metals;

        public MetalsController(ILogger<MetalsController> logger, IMetalsRepository metals)
        {
            _metals = metals;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Metal> Get()
        {
            _logger.LogInformation("Someone get metals");
            return _metals.GetMetals();
        }
    }
}