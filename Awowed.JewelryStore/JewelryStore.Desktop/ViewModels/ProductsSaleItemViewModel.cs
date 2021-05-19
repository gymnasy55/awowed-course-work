using System;
using JewelryStore.Desktop.Models;

namespace JewelryStore.Desktop.ViewModels
{
    public class ProductsSaleItemViewModel
    {
        #region Private Fields

        public readonly ProductsSale ProductsSale;

        #endregion

        #region Public Properties

        public int Id => ProductsSale.Id;

        public int ProductId
        {
            get => ProductsSale.IdProd;
            set => ProductsSale.IdProd = value;
        }

        public DateTime? SaleDate
        {
            get => ProductsSale.SaleDate;
            set => ProductsSale.SaleDate = value;
        }

        public string SaleDateString => SaleDate.Value.ToShortDateString();

        #endregion

        #region Constructor

        public ProductsSaleItemViewModel(ProductsSale productsSale)
        {
            ProductsSale = productsSale;
        }

        #endregion
    }
}
