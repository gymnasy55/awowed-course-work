using System;
using System.Collections.Generic;

#nullable disable

namespace Coursework.Api.Domain
{
    public partial class Order
    {
        public Order()
        {
            Productssales = new HashSet<Productssale>();
        }

        public int Id { get; set; }
        public int IdUser { get; set; }
        public byte IdEmp { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? SaleDate { get; set; }

        public virtual Employee IdEmpNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<Productssale> Productssales { get; set; }
    }
}
