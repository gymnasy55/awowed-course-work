using System.Collections.Generic;
using Coursework.Api.Domain;
using Coursework.Api.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coursework.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        private readonly IOrdersRepository _orders;

        public OrdersController(ILogger<OrdersController> logger, IOrdersRepository orders)
        {
            _orders = orders;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            _logger.LogInformation("Someone get product orders");
            return _orders.GetOrders();
        }
    }
}