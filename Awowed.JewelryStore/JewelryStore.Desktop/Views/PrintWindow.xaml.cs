﻿using System;
using System.Collections.Generic;
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
using JewelryStore.Desktop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : Window
    {
        private readonly AppDbContext _context = new AppDbContext();

        private JewerlyItemViewModel _vm;
        private IQueryable<Prodgroup> _prodgroups;
        private IQueryable<Supplier> _suppliers;

        public PrintWindow()
        {
            InitializeComponent();
        }

        private void PrintWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            _context.Products.Load();
            _context.Metals.Load();
            _context.Prodgroups.Load();
            _context.Suppliers.Load();
            _context.Insertions.Load();

            _prodgroups = _context.Prodgroups;
            _suppliers = _context.Suppliers;

            //foreach (var prodgroup in _prodgroups)
            //{
            //    CbProdGr.Items.Add(prodgroup.ProdGroupName);
            //}

            foreach (var supplier in _suppliers)
            {
                CbSupplier.Items.Add(supplier.Suplname);
            }

            using (var context = new AppDbContext())
            {
                var tempList = context.Products.ToList().Select(x => new JewerlyItemViewModel(x));
                DataGrid.ItemsSource = tempList;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(PrintGrid, "Grid");
            }
        }

        private void IntPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void CbSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
