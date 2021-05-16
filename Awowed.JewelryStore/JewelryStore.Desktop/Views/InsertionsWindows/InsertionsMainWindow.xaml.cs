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
    /// Логика взаимодействия для InsertionsMainWindow.xaml
    /// </summary>
    public partial class InsertionsMainWindow : Window
    {

        private IQueryable<Insertion> _insertions;
        public InsertionsMainWindow()
        {
            InitializeComponent();
        }

        private void ShowItems(Func<Insertion, bool> predicate = null)
        {
            using var context = new AppDbContext();
            MainStackPanel.Children.Clear();

            context.Database.EnsureCreated();
            context.Insertions.Load();

            _insertions = predicate == null
                ? context.Insertions
                : context.Insertions.Where(predicate).AsQueryable();

            foreach (var insertion in _insertions)
            {
                var insertionItemViewModel = new InsertionItemViewModel(insertion);
                MainStackPanel.Children.Add(new InsertionItem(insertionItemViewModel));
            }
        }

        private void InsertionsMainWindow_OnLoaded(object sender, RoutedEventArgs e)
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
                ShowItems(x => x.InsertName.ToLower().Contains(text) || context.Insertions.First(c => c.Id == x.Id).InsertColor.ToLower().Contains(text) 
                                                                     || context.Insertions.First(d=>d.Id == x.Id).GemCategory.ToLower().Contains(text));
            }
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            FindTb.Text = string.Empty;
            ShowItems();
        }

        private void AddInsWindowBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var addInsWindow = new AddInsWindow();
            addInsWindow.ShowDialog();
        }
    }
}
