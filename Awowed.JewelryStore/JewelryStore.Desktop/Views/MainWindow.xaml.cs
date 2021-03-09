using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using JewelryStore.Desktop.Controls;
using JewelryStore.Desktop.Models;
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
            _context.Database.EnsureCreated();
            _context.Products.Load();

            _products = _context.Products;

            foreach (var product in _products)
            {
                var jewerlyItem = new JewerlyItem { TextBlockContent = product.ProdItem };
                MainStackPanel.Children.Add(jewerlyItem);
            }
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainStackPanel.Children.Clear();
            ShowItems();
        }

        //TODO FIND: https://www.youtube.com/watch?v=lBmMfHqqSXc https://github.com/angelsix/fasetto-word/blob/47cffef752d5608be94154f78e6c5746aa28967b/Source/Fasetto.Word.Core/ViewModel/Chat/ChatMessage/ChatMessageListViewModel.cs


    }
}
