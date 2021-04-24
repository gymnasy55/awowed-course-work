using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryStore.Desktop.Models;
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.ViewModels
{
    public class JewerlyItemViewModel
    {
        #region Private Fields

        private readonly Product _product;

        #endregion

        #region Public Properties

        public int Id => _product.Id;

        public byte IdMet
        {
            get => _product.IdMet;
            set => _product.IdMet = value;
        }

        public byte IdIns
        {
            get => _product.IdIns;
            set => _product.IdIns = value;
        }

        public byte IdProdGr
        {
            get => _product.IdProdGr;
            set => _product.IdProdGr = value;
        }

        public byte IdSupp
        {
            get => _product.IdSupp;
            set => _product.IdSupp = value;
        }

        public DateTime? ArrivalDate
        {
            get => _product.ArrivalDate;
            set => _product.ArrivalDate = value;
        }

        public string ArrivalDateString => ArrivalDate.Value.ToShortDateString();

        public string BarCode
        {
            get => _product.BarCode;
            set => _product.BarCode = value;
        }

        public string ProdItem
        {
            get => _product.ProdItem;
            set => _product.ProdItem = value;
        }

        public float Weight
        {
            get => _product.Weight;
            set => _product.Weight = value;
        }

        public float ClearWeight
        {
            get => _product.ClearWeight;
            set => _product.ClearWeight = value;
        }

        public string ProdType
        {
            get => _product.ProdType;
            set => _product.ProdType = value;
        }

        public float? ProdSize
        {
            get => _product.ProdSize;
            set => _product.ProdSize = value;
        }

        public string WeaveType
        {
            get => _product.WeaveType;
            set => _product.WeaveType = value;
        }

        public string WeaveWay
        {
            get => _product.WeaveWay;
            set => _product.WeaveWay = value;
        }

        public string Faceting
        {
            get => _product.Faceting;
            set => _product.Faceting = value;
        }

        public float PriceForTheWork
        {
            get => _product.PriceForTheWork;
            set => _product.PriceForTheWork = value;
        }

        public float Price
        {
            get => _product.Price;
            set => _product.Price = value;
        }

        #endregion

        #region Constructor

        public JewerlyItemViewModel(Product product)
        {
            _product = product;
        }

        #endregion
    }
}
