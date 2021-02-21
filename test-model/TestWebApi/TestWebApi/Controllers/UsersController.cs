using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestWebApi.Domain;
using TestWebApi.Domain.Repositories.Abstract;

namespace TestWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        private readonly IUsersRepository _users;

        public UsersController(ILogger<UsersController> logger, IUsersRepository users)
        {
            _logger = logger;
            _users = users;
        }
        
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _users.GetUsers();
        }
        
        [HttpGet("{name}", Name = "GetN")]
        public IEnumerable<User> Get(string name)
        {
            return _users.GetUsers().Where(x => x.Name == name);
        }
    }
}