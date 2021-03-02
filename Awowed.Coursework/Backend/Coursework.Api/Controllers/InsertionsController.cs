using System.Collections.Generic;
using Coursework.Api.Domain;
using Coursework.Api.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coursework.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsertionsController : ControllerBase
    {
        private readonly ILogger<InsertionsController> _logger;

        private readonly IInsertionsRepository _insertions;

        public InsertionsController(ILogger<InsertionsController> logger, IInsertionsRepository insertions)
        {
            _insertions = insertions;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Insertion> Get()
        {
            _logger.LogInformation("Someone get insertions");
            return _insertions.GetInsertions();
        }
    }
}