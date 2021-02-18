using System;
using System.Collections.Generic;

#nullable disable

namespace TestWebApi.sakila
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public byte Id { get; set; }
        public string Suplname { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
