using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace JewelryStore.Desktop.Views.ProductsWindows
{
    /// <summary>
    /// Interaction logic for CheckWindow.xaml
    /// </summary>
    public class ExtendedViewModel : JewerlyItemViewModel
    {
        public int ShowId { get; set; }

        public string ProdGroupString { get; set; }
        public string Exists{ get; set; }

        public ExtendedViewModel(Product product, int id) : base(product)
        {
            using (var context = new AppDbContext())
            {

                ShowId = id;
                ProdGroupString = context.Prodgroups.First(x => x.Id == product.IdProdGr).ProdGroupName;
                Exists = "-";
            }
        }
    };

    public partial class CheckWindow : Window
    {
        private readonly AppDbContext _context = new AppDbContext();
        private static List<ExtendedViewModel> NewCollection;

        public static readonly DependencyProperty CollectionProperty = DependencyProperty.Register(
            "Collection", typeof(List<ExtendedViewModel>), typeof(CheckWindow), new PropertyMetadata(default(List<ExtendedViewModel>)));

        public List<ExtendedViewModel> Collection
        {
            get => (List<ExtendedViewModel>) GetValue(CollectionProperty);
            set => SetValue(CollectionProperty, value);
        }

        public CheckWindow()
        {
            InitializeComponent();

            Collection = new List<ExtendedViewModel>();
            NewCollection = new List<ExtendedViewModel>();
        }


        private void TextBoxes_OnPreviewSpaceClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        private void IntPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));
        }

        private void CountOverall()
        {
            var temp = DataGrid.Items;
            float overallPrice = 0, overallWorkPrice = 0, overallWeight = 0, overallClearWeight = 0;
            foreach (var item in temp)
            {
                var tempItem = item as JewerlyItemViewModel;
                overallClearWeight += tempItem.ClearWeight;
                overallWeight += tempItem.Weight;
                overallPrice += tempItem.Price;
                overallWorkPrice += tempItem.PriceForTheWork;
            }

            PriceTb.Text = $"{overallPrice} UAH";
            WorkPriceTb.Text = $"{overallWorkPrice} UAH";
            ClearWeightTb.Text = $"{overallClearWeight} г";
            WeightTb.Text = $"{overallWeight} г";
        }

        private void СheckWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            _context.Products.Load();
            _context.Metals.Load();
            _context.Prodgroups.Load();
            _context.Suppliers.Load();
            _context.Insertions.Load();

            ShowItems();
        }

        private void ShowItems(Func<Product, bool> predicate = null)
        {
            using (var context = new AppDbContext())
            {
                Collection = predicate == null
                    ? context.Products.Where(x => !x.IsSold).ToList().Select((x, i) => new ExtendedViewModel(x, i + 1)).ToList()
                    : context.Products.Where(x => !x.IsSold).AsEnumerable().Where(predicate).ToList().Select((x, i) => new ExtendedViewModel(x, i + 1)).ToList();

                DataGrid.ItemsSource = Collection;
            }

            CountOverall();
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox.Text != string.Empty && TextBox.Text.Length == 8 && Collection.Any(x => x.BarCode == TextBox.Text)) //&& NewCollection.Any(x => x.BarCode != TextBox.Text))
            {
                Collection.First(x => x.BarCode == TextBox.Text).Exists = "+";
                if (NewCollection.Count(x => x.BarCode == TextBox.Text) == 0)
                {
                    NewCollection.Add(Collection.First(x => x.BarCode == TextBox.Text));
                }

                TextBox.Text = string.Empty;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(PrintGrid, "Grid");
            };
        }

        private void ShowNewListClicked(object sender, RoutedEventArgs e)
        {
            if (NewCollection.Count == Collection.Count)
            {
                MessageBox.Show("Усі товари присутні!");
                DataGrid.ItemsSource = NewCollection;
                CountOverall();
            }
            else
            {
                MessageBox.Show("Деяких товарів нема!");
                foreach (var item in Collection)
                {
                    if(item.BarCode != NewCollection.First(x => !x.IsSold).BarCode)
                        NewCollection.Add(item);
                }
                DataGrid.ItemsSource = NewCollection.Where(x => x.Exists == "-");
                CountOverall();
            }
        }
    }
}