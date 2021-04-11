using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryStore.Desktop.Models;

namespace JewelryStore.Desktop.ViewModels
{
    public class SupplierItemViewModel
    {
        #region Private Fields

        private readonly Supplier _supplier;

        #endregion

        #region Public Properties

        public byte Id => _supplier.Id;

        public string Suplname
        {
            get => _supplier.Suplname;
            set => _supplier.Suplname = value;
        }

        #endregion

        #region Constructor

        public SupplierItemViewModel(Supplier supplier)
        {
            _supplier = supplier;
        }

        #endregion
    }
}
