using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using JewelryStore.Desktop.Models;
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private readonly AppDbContext _context = new AppDbContext();

        private IQueryable<Metal> _metals;
        private IQueryable<Prodgroup> _prodgroups;
        private IQueryable<Supplier> _suppliers;
        private IQueryable<Insertion> _insertions;

        public AddWindow()
        {
            InitializeComponent();
        }

        private string BarCodeCreation()
        {
            const string chars = "0123456789";
            string barcode = "";
            var rnd = new Random();

            do
            {
                barcode = new string(Enumerable.Repeat(chars, 8).Select(s => s[rnd.Next(s.Length)]).ToArray());
            } while (_context.Products.Any(x => x.BarCode == barcode));

            return barcode;
        }

        private void AddWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DpArrDate.Text = DateTime.Now.ToString();
            TblPrice.Text = "0 UAH";
            TblWorkPrice.Text = "0 UAH";
            TblBarCode.Text = BarCodeCreation();

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

        private void AddBtn_Clicked(object sender, RoutedEventArgs e)
        {
            DpArrDate.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            var result = MessageBox.Show("Чи впевнені Ви, що бажаєте додати товар?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var product = new Product
                    {
                        Id = _context.Products.OrderBy(x => x.Id).Last().Id + 1,
                        ProdItem = TbProdItem.Text.Trim(),
                        BarCode = TbProdItem.Text.Trim(),
                        ArrivalDate = new DateTime(DpArrDate.DisplayDate.Ticks),
                        IdMet = _metals.First(x => x.MetalName == CbMetal.SelectionBoxItem.ToString().Trim()).Id,
                        IdProdGr = _prodgroups.First(x => x.ProdGroupName == CbProdGr.SelectionBoxItem.ToString().Trim()).Id,
                        ProdType = TbProdType.Text.Trim(),
                        IdSupp = _suppliers.First(x => x.Suplname == CbSupplier.SelectionBoxItem.ToString().Trim()).Id,
                        ProdSize = Convert.ToSingle(TbSize.Text.Trim()),
                        Weight = Convert.ToSingle(TbWeight.Text.Trim()),
                        ClearWeight = Convert.ToSingle(TbClearWeight.Text.Trim()),
                        IdIns = CbInsert.SelectedItem.ToString().Contains('|')
                            ? _insertions.First(x => x.InsertName == CbInsert.SelectionBoxItem.ToString().Substring(0, CbInsert.SelectionBoxItem.ToString().IndexOf('|') - 1)).Id
                            : _insertions.First(x => x.InsertName == CbInsert.SelectedItem.ToString()).Id,
                        Faceting = TbFaceting.Text.Trim(),
                        WeaveWay = CbWeaveWay.SelectionBoxItem.ToString().Trim(),
                        WeaveType = TbWeaveType.Text.Trim(),
                        IsSold = false,
                        PriceForTheWork = Convert.ToSingle(TblWorkPrice.Text.Substring(0, TblWorkPrice.Text.IndexOf('U') - 1)),
                        Price = Convert.ToSingle(TblPrice.Text.Substring(0, TblPrice.Text.IndexOf('U') - 1))
                    };
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    MessageBox.Show("Додано в бд!");
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

        // private void TbWeight_OnTextChanged(object sender, TextChangedEventArgs e)
        // {
        //     TblPrice.Text = $"{Settings.GramSalePrice * Convert.ToSingle(TbWeight.Text == string.Empty ? "0" : TbWeight.Text)} UAH";
        //     TblWorkPrice.Text = $"{Settings.GramWorkPrice * Convert.ToSingle(TbWeight.Text == string.Empty ? "0" : TbWeight.Text)} UAH";
        // }

        // TODO: Сделать ввод только цифр с одной точкой - и что бы это работало в TBWeight_OnTextChanged
        private void IntPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // if (e.Text.Last() == ',') 
            //     e.Handled = !(Char.IsDigit(e.Text, 0) || e.Text.Last() == ',');

            if(!(sender is TextBox textBox))
                return;

            if ((textBox.Text.Contains(',') && e.Text[^1] == ',')
                || (!Regex.IsMatch(e.Text[^1].ToString(), @"\d|,"))
                || (textBox.Text.Length == 0 && e.Text[^1] == ','))
            {
                e.Handled = true;
                return;
            }

            if (Regex.IsMatch(textBox.Text, @"\d+") && e.Text[^1] == ',')
            {
                textBox.Text += ",0";
                textBox.CaretIndex = textBox.Text.Length;
                e.Handled = true;
            }

            if (textBox.Name == "TbWeight")
            {
                TblPrice.Text = $"{Settings.GramSalePrice * Convert.ToSingle(TbWeight.Text == string.Empty ? "0" : TbWeight.Text)} UAH";
                TblWorkPrice.Text = $"{Settings.GramWorkPrice * Convert.ToSingle(TbWeight.Text == string.Empty ? "0" : TbWeight.Text)} UAH";
            }
        }

        private void TextBoxes_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text[^1].ToString(), "\"|'")) 
                return;
            
            if (!(sender is TextBox textBox))
                return;
            
            textBox.Text += '`';
            textBox.CaretIndex = textBox.Text.Length;
            
            e.Handled = true;
        }
    }
}