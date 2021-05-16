using System;

#nullable disable

namespace JewelryStore.Desktop.Models
{
    public partial class ProductsSale
    {
        public int Id { get; set; }
        public int IdProd { get; set; }
        public DateTime? SaleDate { get; set; }

        public virtual Product IdProdNavigation { get; set; }
    }
}
