using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Castle.Core.Internal;
using JewelryStore.Desktop.Controls;
using JewelryStore.Desktop.Models;
using JewelryStore.Desktop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _context = new AppDbContext();

        private IQueryable<Product> _products;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ShowItems();
        }

        private void AddWindowBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddWindow();
            addWindow.Show();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private void ShowItems()
        {
            MainStackPanel.Children.Clear();

            _context.Database.EnsureCreated();
            _context.Products.Load();

            _products = _context.Products;

            foreach (var product in _products)
            {
                var jewerlyItemViewModel = new JewerlyItemViewModel(product);
                MainStackPanel.Children.Add(new JewerlyItem(jewerlyItemViewModel));
            }
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            ShowItems();
        }

        private void FindTb_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var text = FindTb.Text;
            if (text == string.Empty)
            {
                ShowItems();
                return;
            }

            MainStackPanel.Children.Clear();

            var temp = _context.Products.Where(x => x.ProdItem.Contains(text));

            foreach (var product in temp)
            {
                var jewerlyItemViewModel = new JewerlyItemViewModel(product);
                MainStackPanel.Children.Add(new JewerlyItem(jewerlyItemViewModel));
            }
        }
    }
}
