using System.Linq;
using Coursework.Api.Domain.Repositories.Abstract;

namespace Coursework.Api.Domain.Repositories.EntityFramework
{
    public class EfProductssalesRepository : IProductssalesRepository
    {
        private readonly AppDbContext _context;

        public EfProductssalesRepository(AppDbContext context) => _context = context;

        public IQueryable<Productssale> GetProductssales() => _context.Productssales;
    }
}