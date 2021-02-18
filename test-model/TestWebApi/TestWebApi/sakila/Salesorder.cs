using System;
using System.Collections.Generic;

#nullable disable

namespace TestWebApi.sakila
{
    public partial class Salesorder
    {
        public int Id { get; set; }
        public int IdProd { get; set; }
        public int IdUser { get; set; }
        public DateTime? SaleDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public float? SalePrice { get; set; }
        public int ProdQuantity { get; set; }

        public virtual Product IdProdNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
