using System;
using System.Collections.Generic;

#nullable disable

namespace TestWebApi.sakila
{
    public partial class Product
    {
        public Product()
        {
            Salesorders = new HashSet<Salesorder>();
        }

        public int Id { get; set; }
        public byte IdMet { get; set; }
        public byte IdIns { get; set; }
        public byte IdProdGr { get; set; }
        public byte IdSupp { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string ProdItem { get; set; }
        public float Weight { get; set; }
        public float ClearWeight { get; set; }
        public string ProdType { get; set; }
        public float? ProdSize { get; set; }
        public float? ProdLength { get; set; }
        public string WeaveType { get; set; }
        public string WeaveWay { get; set; }
        public string Faceting { get; set; }
        public float PriceForTheWork { get; set; }

        public virtual Insertion IdInsNavigation { get; set; }
        public virtual Metal IdMetNavigation { get; set; }
        public virtual Prodgroup IdProdGrNavigation { get; set; }
        public virtual Supplier IdSuppNavigation { get; set; }
        public virtual ICollection<Salesorder> Salesorders { get; set; }
    }
}
