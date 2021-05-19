using JewelryStore.Desktop.Models;

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
