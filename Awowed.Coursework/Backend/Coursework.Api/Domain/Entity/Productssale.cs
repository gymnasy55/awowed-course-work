using System;
using System.Collections.Generic;

#nullable disable

namespace Coursework.Api.Domain
{
    public partial class Productssale
    {
        public int Id { get; set; }
        public int IdOrd { get; set; }
        public int IdProd { get; set; }
        public int ProdQuantity { get; set; }
        public float? SalePrice { get; set; }

        public virtual Order IdOrdNavigation { get; set; }
        public virtual Product IdProdNavigation { get; set; }
    }
}
