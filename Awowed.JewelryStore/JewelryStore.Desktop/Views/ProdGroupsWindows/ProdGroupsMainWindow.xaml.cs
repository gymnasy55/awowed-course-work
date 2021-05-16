using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using JewelryStore.Desktop.Controls;
using JewelryStore.Desktop.Models;
using JewelryStore.Desktop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для ProdGroupsMainWindow.xaml
    /// </summary>
    public partial class ProdGroupsMainWindow : Window
    {

        private IQueryable<Prodgroup> _prodgroups;
        public ProdGroupsMainWindow()
        {
            InitializeComponent();
        }

        private void ShowItems(Func<Prodgroup, bool> predicate = null)
        {
            using var context = new AppDbContext();
            MainStackPanel.Children.Clear();

            context.Database.EnsureCreated();
            context.Prodgroups.Load();

            _prodgroups = predicate == null
                ? context.Prodgroups
                : context.Prodgroups.Where(predicate).AsQueryable();

            foreach (var prodgroup in _prodgroups)
            {
                var prodGrItemViewModel = new ProdGroupItemViewModel(prodgroup);
                MainStackPanel.Children.Add(new ProdGroupItem(prodGrItemViewModel));
            }
        }

        private void ProdGroupsMainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ShowItems();
        }

        private void FindTb_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var text = FindTb.Text.ToLower();
            if (text == string.Empty)
            {
                ShowItems();
                return;
            }

            using (var context = new AppDbContext())
            {
                ShowItems(x => x.ProdGroupName.ToLower().Contains(text));
            }
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            FindTb.Text = string.Empty;
            ShowItems();
        }

        private void AddProdGrWindowBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var addProdGrWindow = new AddProdGrWindow();
            addProdGrWindow.ShowDialog();
        }
    }
}
