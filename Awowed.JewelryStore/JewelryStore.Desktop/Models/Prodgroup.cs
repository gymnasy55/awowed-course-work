using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable

namespace JewelryStore.Desktop.Models
{
    public partial class Prodgroup
    {
        public Prodgroup()
        {
            Products = new HashSet<Product>();
        }

        public byte Id { get; set; }
        public string ProdGroupName { get; set; }

        public virtual ICollection<Product> Products { get; private set; }
            = new ObservableCollection<Product>();
    }
}
