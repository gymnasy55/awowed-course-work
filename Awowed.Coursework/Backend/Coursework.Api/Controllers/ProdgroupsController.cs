using System.Collections.Generic;
using Coursework.Api.Domain;
using Coursework.Api.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coursework.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdgroupsController : ControllerBase
    {
        private readonly ILogger<ProdgroupsController> _logger;

        private readonly IProdgroupsRepository _prodgroups;

        public ProdgroupsController(ILogger<ProdgroupsController> logger, IProdgroupsRepository prodgroups)
        {
            _prodgroups = prodgroups;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Prodgroup> Get()
        {
            _logger.LogInformation("Someone get product groups");
            return _prodgroups.GetProdgroups();
        }
    }
}