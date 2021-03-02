using System.Linq;
using Coursework.Api.Domain.Repositories.Abstract;

namespace Coursework.Api.Domain.Repositories.EntityFramework
{
    public class EfInsertionsRepository : IInsertionsRepository
    {
        private readonly AppDbContext _context;

        public EfInsertionsRepository(AppDbContext context) => _context = context;

        public IQueryable<Insertion> GetInsertions() => _context.Insertions;
    }
}