using System.Collections.Generic;
using System.Linq;
using Coursework.Api.Domain;
using Coursework.Api.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coursework.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        private readonly IUsersRepository _users;

        public UsersController(ILogger<UsersController> logger, IUsersRepository users)
        {
            _users = users;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            _logger.LogInformation("Someone get users");
            return _users.GetUsers();
        }

        [HttpGet("{userid}", Name = "GetN")]
        public IEnumerable<Order> GetOrder(int id)
        {
            return _users.GetUsers().First(x => x.Id == id).Orders;
        }
    }
}
