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
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для ChangeWindow.xaml
    /// </summary>
    public partial class ChangeWindow : Window
    {
        public ChangeWindow()
        {
            InitializeComponent();
        }

        private readonly AppDbContext _context = new AppDbContext();

        private IQueryable<Metal> _metals;
        private IQueryable<Prodgroup> _prodgroups;
        private IQueryable<Supplier> _suppliers;
        private IQueryable<Insertion> _insertions;
       
        private void ChangeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DpArrDate.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
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

            var weaveWays = new List<string> { "", "Машинна", "Ручна" };

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

            foreach (var weaveWay in weaveWays)
            {
                CbWeaveWay.Items.Add(weaveWay);
            }
        }



        //private void ChangeBtn_Clicked(object sender, RoutedEventArgs e)
        //{
        //    DpArrDate.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        //    var result = MessageBox.Show("Уверены ли Вы, что хотите изменить товар?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
        //    switch (result)
        //    {
        //        case MessageBoxResult.Yes:
        //            var product = new Product
        //            {
        //                Id = _context.Products.OrderBy(x => x.Id).Last().Id + 1,
        //                ProdItem = TbProdItem.Text,
        //                BarCode = TbProdItem.Text, //todo: MAKE BARCODE
        //                ArrivalDate = new DateTime(DpArrDate.DisplayDate.Ticks),
        //                IdMet = _metals.First(x => x.MetalName == CbMetal.SelectionBoxItem.ToString()).Id,
        //                IdProdGr = _prodgroups.First(x => x.ProdGroupName == CbProdGr.SelectionBoxItem.ToString()).Id,
        //                ProdType = TbProdType.Text,
        //                IdSupp = _suppliers.First(x => x.Suplname == CbSupplier.SelectionBoxItem.ToString()).Id,
        //                ProdSize = Convert.ToSingle(TbSize.Text),
        //                Weight = Convert.ToSingle(TbWeight.Text),
        //                ClearWeight = Convert.ToSingle(TbClearWeight.Text),
        //                IdIns = _insertions.First(x => x.InsertName == CbInsert.SelectionBoxItem.ToString().Substring(0, CbInsert.SelectionBoxItem.ToString().IndexOf('|') - 1)).Id,
        //                Faceting = TbFaceting.Text,
        //                WeaveWay = CbWeaveWay.SelectionBoxItem.ToString(),
        //                WeaveType = TbWeaveType.Text,
        //                PriceForTheWork = Convert.ToSingle(TblWorkPrice.Text.Substring(0, TblWorkPrice.Text.IndexOf('U') - 1)),
        //                Price = Convert.ToSingle(TblPrice.Text.Substring(0, TblPrice.Text.IndexOf('U') - 1))
        //            };
        //            _context.Products.Add(product);
        //            _context.SaveChanges();
        //            MessageBox.Show("Изменено!");
        //            break;
        //        case MessageBoxResult.No:
        //            break;
        //    }
        //}

        private void TbWeight_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TblPrice.Text = $"{Settings.GramSalePrice * Convert.ToSingle(TbWeight.Text == string.Empty ? "0" : TbWeight.Text)} UAH";
            TblWorkPrice.Text = $"{Settings.GramWorkPrice * Convert.ToSingle(TbWeight.Text == string.Empty ? "0" : TbWeight.Text)} UAH";
        }
    }
}
