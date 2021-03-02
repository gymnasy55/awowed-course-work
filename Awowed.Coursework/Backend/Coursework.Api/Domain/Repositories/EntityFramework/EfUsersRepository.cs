using System.Linq;
using Coursework.Api.Domain.Repositories.Abstract;

namespace Coursework.Api.Domain.Repositories.EntityFramework
{
    public class EfUsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public EfUsersRepository(AppDbContext context) => _context = context;

        public IQueryable<User> GetUsers() => _context.Users;
    }
}
