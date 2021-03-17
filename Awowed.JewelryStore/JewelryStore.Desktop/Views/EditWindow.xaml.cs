using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using JewelryStore.Desktop.Models;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
        }

        private void EditWindow_Loaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TbWeight_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TblPrice.Text = $"{Settings.GramSalePrice * Convert.ToSingle(TbWeight.Text == string.Empty ? "0" : TbWeight.Text)} UAH";
            TblWorkPrice.Text = $"{Settings.GramWorkPrice * Convert.ToSingle(TbWeight.Text == string.Empty ? "0" : TbWeight.Text)} UAH";
        }

        private void EditBtn_Clicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ClearBtn_Clicked(object sender, RoutedEventArgs e)
        {
            DpArrDate.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            TbProdItem.Text = string.Empty;
            CbMetal.Text = string.Empty;
            CbProdGr.Text = string.Empty;
            CbSupplier.Text = string.Empty;
            TbProdType.Text = string.Empty;
            CbInsert.Text = string.Empty;
            TbSize.Text = string.Empty;
            TbWeight.Text = string.Empty;
            TbClearWeight.Text = string.Empty;
            TbFaceting.Text = string.Empty;
            CbWeaveWay.Text = string.Empty;
            TbWeaveType.Text = string.Empty;
        }
    }
}
