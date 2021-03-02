using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Api.Domain.Repositories.Abstract;

namespace Coursework.Api.Domain.Repositories.EntityFramework
{
    public class EfSuppliersRepository : ISuppliersRepository
    {
        private readonly AppDbContext _context;

        public EfSuppliersRepository(AppDbContext context) => _context = context;

        public IQueryable<Supplier> GetSuppliers() => _context.Suppliers;
    }
}
