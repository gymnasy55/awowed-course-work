using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Printing;
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
using JewelryStore.Desktop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private readonly AppDbContext _context = new AppDbContext();

        private JewerlyItemViewModel _vm;
        private IQueryable<Metal> _metals;
        private IQueryable<Prodgroup> _prodgroups;
        private IQueryable<Supplier> _suppliers;
        private IQueryable<Insertion> _insertions;
        private List<string> _weaveWays = new List<string> { "", "Машинна", "Ручна" };

        public EditWindow(JewerlyItemViewModel vm)
        {
            _vm = vm;
            InitializeComponent();
        }

        private void EditWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TblPrice.Text = "0 UAH";
            TblWorkPrice.Text = "0 UAH";

            _context.Database.EnsureCreated();
            _context.Products.Load();
            _context.Metals.Load();
            _context.Prodgroups.Load();
            _context.Suppliers.Load();
            _context.Insertions.Load();

            _metals = _context.Metals;
            _prodgroups = _context.Prodgroups;
            _suppliers = _context.Suppliers;
            _insertions = _context.Insertions;

            foreach (var insertion in _insertions)
            {
                CbInsert.Items.Add(insertion.InsertColor == string.Empty ? $"{insertion.InsertName}" : $"{insertion.InsertName} | {insertion.InsertColor}");
            }

            foreach (var metal in _metals)
            {
                CbMetal.Items.Add(metal.MetalName);
            }

            foreach (var prodgroup in _prodgroups)
            {
                CbProdGr.Items.Add(prodgroup.ProdGroupName);
            }

            foreach (var supplier in _suppliers)
            {
                CbSupplier.Items.Add(supplier.Suplname);
            }

            foreach (var weaveWay in _weaveWays)
            {
                CbWeaveWay.Items.Add(weaveWay);
            }

            TbProdItem.Text = _vm.ProdItem;
            DpArrDate.DisplayDate = _vm.ArrivalDate ?? DateTime.Now;
            DpArrDate.Text = DpArrDate.DisplayDate.ToString();
            CbMetal.SelectedItem = _metals.First(x => x.Id == _vm.IdMet).MetalName.ToString();
            CbProdGr.SelectedItem = _prodgroups.First(x => x.Id == _vm.IdProdGr).ProdGroupName.ToString();
            TbProdType.Text = _vm.ProdType;
            CbSupplier.SelectedItem = _suppliers.First(x => x.Id == _vm.IdSupp).Suplname.ToString();
            TbSize.Text = _vm.ProdSize.ToString();
            TbWeight.Text = _vm.Weight.ToString();
            TbClearWeight.Text = _vm.ClearWeight.ToString();
            CbInsert.SelectedItem = _insertions.First(x => x.Id == _vm.IdIns).InsertColor != string.Empty 
                ? $"{_insertions.First(x => x.Id == _vm.IdIns).InsertName} | {_insertions.First(x => x.Id == _vm.IdIns).InsertColor}"
                : $"{_insertions.First(x => x.Id == _vm.IdIns).InsertName}";
            TbFaceting.Text = _vm.Faceting;
            CbWeaveWay.SelectedIndex = _weaveWays.IndexOf(_vm.WeaveWay);
            TbWeaveType.Text = _vm.WeaveType;
            TblWorkPrice.Text = $"{_vm.PriceForTheWork} UAH";
            TblPrice.Text = $"{_vm.Price} UAH";
        }

        private void EditBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Чи впевнені Ви, що бажаєте змінити товар?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var product = _context.Products.FirstOrDefault(x => x.Id == _vm.Id);
                    if (product != null)
                    {
                        product.ProdItem = TbProdItem.Text.Trim();
                        product.BarCode = TbProdItem.Text.Trim();
                        product.ArrivalDate = new DateTime(DpArrDate.DisplayDate.Ticks);
                        product.IdMet = _metals.First(x => x.MetalName == CbMetal.SelectionBoxItem.ToString().Trim()).Id;
                        product.IdProdGr = _prodgroups.First(x => x.ProdGroupName == CbProdGr.SelectionBoxItem.ToString().Trim()).Id;
                        product.ProdType = TbProdType.Text.Trim();
                        product.IdSupp = _suppliers.First(x => x.Suplname == CbSupplier.SelectionBoxItem.ToString().Trim()).Id;
                        product.ProdSize = Convert.ToSingle(TbSize.Text.Trim());
                        product.Weight = Convert.ToSingle(TbWeight.Text.Trim());
                        product.ClearWeight = Convert.ToSingle(TbClearWeight.Text.Trim());
                        product.IdIns = CbInsert.SelectedItem.ToString().Contains('|') 
                            ? _insertions.First(x => x.InsertName == CbInsert.SelectionBoxItem.ToString().Substring(0, CbInsert.SelectionBoxItem.ToString().IndexOf('|') - 1)).Id
                            : _insertions.First(x => x.InsertName == CbInsert.SelectedItem.ToString()).Id;
                        product.Faceting = TbFaceting.Text.Trim();
                        product.WeaveWay = CbWeaveWay.SelectionBoxItem.ToString();
                        product.WeaveType = TbWeaveType.Text.Trim();
                        product.PriceForTheWork = Convert.ToSingle(TblWorkPrice.Text.Substring(0, TblWorkPrice.Text.IndexOf('U') - 1));
                        product.Price = Convert.ToSingle(TblPrice.Text.Substring(0, TblPrice.Text.IndexOf('U') - 1));

                        _context.SaveChanges();
                        MessageBox.Show("Успішно змінено в бд!", "Успіх", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Помилка при зміні в бд!", "Помилка", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
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

        private void TbWeight_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TblPrice.Text = $"{Settings.GramSalePrice * Convert.ToSingle(TbWeight.Text == string.Empty ? "0" : TbWeight.Text)} UAH";
            TblWorkPrice.Text = $"{Settings.GramWorkPrice * Convert.ToSingle(TbWeight.Text == string.Empty ? "0" : TbWeight.Text)} UAH";
        }

        private void IntPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Last() == ',')
                e.Handled = !(Char.IsDigit(e.Text, 0) || e.Text.Last() == ',');
        }
    }
}
