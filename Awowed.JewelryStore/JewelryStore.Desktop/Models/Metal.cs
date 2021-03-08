using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JewelryStore.Desktop.Models
{
    public partial class Metal
    {
        public Metal()
        {
            Products = new HashSet<Product>();
        }

        public byte Id { get; set; }
        public string MetalName { get; set; }
        public int? Sample { get; set; }

        public virtual ICollection<Product> Products { get; private set; } 
            = new ObservableCollection<Product>();
    }
}
