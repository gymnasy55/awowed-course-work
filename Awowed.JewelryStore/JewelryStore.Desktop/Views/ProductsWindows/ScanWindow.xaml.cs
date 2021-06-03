using System;
using System.Collections.Generic;
using System.Windows;
using System.IO.Ports;
using System.Linq;
using System.Windows.Input;
using JewelryStore.Desktop.Models;
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.Views.ProductsWindows
{
    /// <summary>
    /// Логика взаимодействия для ScanWindow.xaml
    /// </summary>
    public partial class ScanWindow : Window
    {
        private List<string> _list;
        
        public ScanWindow()
        {
            InitializeComponent();

            _list = new List<string>();

        }

        private void TextBoxes_OnPreviewSpaceClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        private void IntPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox.Text != string.Empty && TextBox.Text.Length == 8)
            {
                if (!ListBox.Items.Contains(TextBox.Text))
                {
                    _list.Add(TextBox.Text);
                    ListBox.Items.Add(TextBox.Text);
                }
                TextBox.Text = string.Empty;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            using var context = new AppDbContext();
            
            var products = context.Products;
            var productsSales = context.Productssales;
            
            products.Load();
            productsSales.Load();

            var exactProducts = _list
                .Select(barcode => products.FirstOrDefault(product => product.BarCode == barcode))
                .SkipWhile(x => x == null)
                .ToList();
            
            exactProducts.ForEach(product => product.IsSold = true);

            productsSales.AddRange(exactProducts.Select(product => new ProductsSale
            {
                Id = productsSales.Max(productsSale => productsSale.Id) + 1,
                IdProd = product.Id,
                SaleDate = DateTime.Now
            }));

            context.SaveChanges();
            
            _list.Clear();
            ListBox.Items.Clear();
        }

        private void ListBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                _list.Remove(ListBox.SelectedItem.ToString());
                ListBox.Items.Remove(ListBox.SelectedItem);
            }
        }
    }
}
