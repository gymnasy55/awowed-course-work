using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryStore.Desktop.Models;
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.ViewModels
{
    public class InsertionItemViewModel
    {
        #region Private Fields

        private readonly Insertion _insertion;

        #endregion

        #region Public Properties

        public byte Id => _insertion.Id;

        public string InsertName
        {
            get => _insertion.InsertName;
            set => _insertion.InsertName = value;
        } 

        public string InsertColor
        {
            get => _insertion.InsertColor;
            set => _insertion.InsertColor = value;
        }

        public string GemCategory
        {
            get => _insertion.GemCategory;
            set => _insertion.GemCategory = value;
        }


        #endregion

        #region Constructor

        public InsertionItemViewModel(Insertion insertion)
        {
            _insertion = insertion;
        }

        #endregion
    }
}
