using System.Linq;
using Coursework.Api.Domain.Repositories.Abstract;

namespace Coursework.Api.Domain.Repositories.EntityFramework
{
    public class EfMetalsRepository : IMetalsRepository
    {
        private readonly AppDbContext _context;

        public EfMetalsRepository(AppDbContext context) => _context = context;

        public IQueryable<Metal> GetMetals() => _context.Metals;
    }
}