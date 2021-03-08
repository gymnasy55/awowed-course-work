using System;
using System.Collections.Generic;

#nullable disable

namespace JewelryStore.Desktop.Models
{
    public partial class Productssale
    {
        public int Id { get; set; }
        public int IdProd { get; set; }
        public DateTime? SaleDate { get; set; }

        public virtual Product IdProdNavigation { get; set; }
    }
}
