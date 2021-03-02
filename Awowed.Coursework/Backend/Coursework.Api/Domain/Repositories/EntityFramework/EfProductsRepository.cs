using System.Linq;
using Coursework.Api.Domain.Repositories.Abstract;

namespace Coursework.Api.Domain.Repositories.EntityFramework
{
    public class EfProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _context;

        public EfProductsRepository(AppDbContext context) => _context = context;

        public IQueryable<Product> GetProducts() => _context.Products;
    }
}
