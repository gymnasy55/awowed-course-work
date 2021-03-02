using System.Linq;
using Coursework.Api.Domain.Repositories.Abstract;

namespace Coursework.Api.Domain.Repositories.EntityFramework
{
    public class EfOrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext _context;

        public EfOrdersRepository(AppDbContext context) => _context = context;

        public IQueryable<Order> GetOrders() => _context.Orders;
    }
}