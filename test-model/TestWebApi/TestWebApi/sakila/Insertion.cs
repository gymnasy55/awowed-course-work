using System;
using System.Collections.Generic;

#nullable disable

namespace TestWebApi.sakila
{
    public partial class Insertion
    {
        public Insertion()
        {
            Products = new HashSet<Product>();
        }

        public byte Id { get; set; }
        public string InsertName { get; set; }
        public string InsertColor { get; set; }
        public string GemCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
