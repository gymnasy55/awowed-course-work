using System.Collections.Generic;
using Coursework.Api.Domain;
using Coursework.Api.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coursework.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;

        private readonly IEmployeesRepository _employees;

        public EmployeesController(ILogger<EmployeesController> logger, IEmployeesRepository employees)
        {
            _employees = employees;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            _logger.LogInformation("Someone get employees");
            return _employees.GetEmployees();
        }
    }
}