using JewelryStore.Desktop.Models;

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

        public float Price
        {
            get => _insertion.Price;
            set => _insertion.Price = value;
        }

        public float WorkPrice
        {
            get => _insertion.WorkPrice;
            set => _insertion.WorkPrice = value;
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
