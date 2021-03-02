using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Api.Domain.Repositories.Abstract;

namespace Coursework.Api.Domain.Repositories
{
    public class DataManager
    {
        public IEmployeesRepository Employees { get; set; }
        public IInsertionsRepository Insertions { get; set; }
        public IMetalsRepository Metals { get; set; }
        public IOrdersRepository Orders { get; set; }
        public IProdgroupsRepository ProdGroups { get; set; }
        public IProductsRepository Products { get; set; }
        public IProductssalesRepository ProductsSales { get; set; }
        public ISuppliersRepository Suppliers { get; set; }
        public IUsersRepository Users { get; set; }

        public DataManager(
            IEmployeesRepository employeesRepository,
            IInsertionsRepository insertionsRepository,
            IMetalsRepository metalsRepository,
            IOrdersRepository ordersRepository,
            IProdgroupsRepository prodgroupsRepository,
            IProductsRepository productsRepository,
            IProductssalesRepository productssalesRepository,
            ISuppliersRepository suppliersRepository,
            IUsersRepository usersRepository
        )
        {
            Employees = employeesRepository;
            Insertions = insertionsRepository;
            Metals = metalsRepository;
            Orders = ordersRepository;
            ProdGroups = prodgroupsRepository;
            Products = productsRepository;
            ProductsSales = productssalesRepository;
            Suppliers = suppliersRepository;
            Users = usersRepository;
        }
    }
}
