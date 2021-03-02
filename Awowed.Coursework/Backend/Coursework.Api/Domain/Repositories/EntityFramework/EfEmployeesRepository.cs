using System.Linq;
using Coursework.Api.Domain.Repositories.Abstract;

namespace Coursework.Api.Domain.Repositories.EntityFramework
{
    public class EfEmployeesRepository : IEmployeesRepository
    {
        private readonly AppDbContext _context;

        public EfEmployeesRepository(AppDbContext context) => _context = context;

        public IQueryable<Employee> GetEmployees() => _context.Employees;
    }
}
