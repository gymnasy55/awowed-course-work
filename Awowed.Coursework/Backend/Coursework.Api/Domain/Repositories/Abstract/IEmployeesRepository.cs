using System.Linq;

namespace Coursework.Api.Domain.Repositories.Abstract
{
    public interface IEmployeesRepository
    {
        IQueryable<Employee> GetEmployees();
    }
}
