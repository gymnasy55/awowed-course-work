using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Api.Domain.Repositories.Abstract
{
    public interface ISuppliersRepository
    {
        IQueryable<Supplier> GetSuppliers();
    }
}
