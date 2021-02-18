using System;
using System.Collections.Generic;
using System.Linq;
using TestWebApi.Domain.Repositories.Abstract;

namespace TestWebApi.Domain.Repositories.EntityFramework
{
    public class EFUsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public EFUsersRepository(AppDbContext context) => _context = context;

        public IQueryable<User> GetUsers() => _context.Users;
    }
}
