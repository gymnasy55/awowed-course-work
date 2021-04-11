using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryStore.Desktop.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;

namespace JewelryStore.Desktop.ViewModels
{
    public class ProdGroupItemViewModel
    {
        #region Private Fields

        public readonly Prodgroup _prodgroup;

        #endregion

        #region Public Properties

        public byte Id => _prodgroup.Id;

        public string ProdGroupName
        {
            get => _prodgroup.ProdGroupName;
            set => _prodgroup.ProdGroupName = value;
        }

        #endregion

        #region Constructor

        public ProdGroupItemViewModel(Prodgroup prodgroup)
        {
            _prodgroup = prodgroup;
        }

        #endregion
    }
}
