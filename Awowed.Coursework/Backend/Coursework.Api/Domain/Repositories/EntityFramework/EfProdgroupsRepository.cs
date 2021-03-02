using System.Linq;
using Coursework.Api.Domain.Repositories.Abstract;

namespace Coursework.Api.Domain.Repositories.EntityFramework
{
    public class EfProdgroupsRepository : IProdgroupsRepository
    {
        private readonly AppDbContext _context;

        public EfProdgroupsRepository(AppDbContext context) => _context = context;

        public IQueryable<Prodgroup> GetProdgroups() => _context.Prodgroups;
    }
}