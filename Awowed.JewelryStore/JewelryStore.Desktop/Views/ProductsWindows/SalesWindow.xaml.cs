using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using JewelryStore.Desktop.Models;
using JewelryStore.Desktop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.Views
{
    public class ExtendedProductsSaleViewModel : ProductsSaleItemViewModel
    {
        public int ShowId { get; set; }
        public string ProdItem { get; set; }
        public string BarCode { get; set; }
        public string ProdGroupString { get; set; }
        public float ClearWeight { get; set; }
        public float Weight { get; set; }
        public float PriceForTheWork { get; set; }
        public float Price { get; set; }
        public string SupplierName { get; set; }

        public ExtendedProductsSaleViewModel(ProductsSale productsale, int id) : base(productsale)
        {
            using (var context = new AppDbContext())
            {
                var product = context.Products.First(x => x.Id == productsale.IdProd);
                ProdGroupString = context.Prodgroups.First(x => x.Id == product.IdProdGr).ProdGroupName;
                ProdItem = product.ProdItem;
                BarCode = product.BarCode;
                ClearWeight = product.ClearWeight;
                Weight = product.Weight;
                PriceForTheWork = product.PriceForTheWork;
                Price = product.Price;
                SupplierName = context.Suppliers.First(x => x.Id == product.IdSupp).Suplname;
            }
            
            
            ShowId = id;
        }
    };

    /// <summary>
    /// Interaction logic for SalesWindow.xaml
    /// </summary>
    public partial class SalesWindow : Window
    {
        private readonly AppDbContext _context = new AppDbContext();

        private ProductsSaleItemViewModel _vm;
        private IQueryable<Supplier> _suppliers;
        private IQueryable<Product> _products;

        public SalesWindow()
        {
            InitializeComponent();
        }

        private void CountOverall()
        {
            var temp = DataGrid.Items;
            float overallPrice = 0, overallWorkPrice = 0, overallWeight = 0, overallClearWeight = 0;
            foreach (var item in temp)
            {
                var tempItem = item as ExtendedProductsSaleViewModel;
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

        private void ShowItems(Func<ProductsSale, bool> predicate = null)
        {
            using (var context = new AppDbContext())
            {
                var tempList = predicate == null
                    ? context.Productssales.ToList().Select((x, i) => new ExtendedProductsSaleViewModel(x, i + 1))
                    : context.Productssales.ToList().Where(predicate).Select((x, i) => new ExtendedProductsSaleViewModel(x, i + 1));

                DataGrid.ItemsSource = tempList;
            }
            CountOverall();
        }

        private void SalesWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            _context.Products.Load();
            _context.Metals.Load();
            _context.Prodgroups.Load();
            _context.Suppliers.Load();
            _context.Insertions.Load();
            _context.Productssales.Load();

            _suppliers = _context.Suppliers;
            _products = _context.Products;

            CbSupplier.Items.Add("Усі");
            foreach (var supplier in _suppliers)
            {
                CbSupplier.Items.Add(supplier.Suplname);
            }

            ShowItems();
        }

        private void CbSupplier_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                var supplier = context.Suppliers.FirstOrDefault(x => x.Suplname == CbSupplier.SelectedItem.ToString());
                if (supplier == null)
                {
                    ShowItems();
                }
                else
                {
                    ShowItems(x => context.Suppliers.First(c => c.Id == context.Products.First(y => y.Id == x.IdProd).IdSupp).Id == context.Suppliers.First(c => c.Suplname == supplier.Suplname).Id);
                }
            }
        }

        private void IntPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));
        }

        private void Date_Pickers(DateTime? date1 = null, DateTime? date2 = null)
        {
            if (date1.HasValue && date2.HasValue)
            {
                ShowItems(x => x.SaleDate >= date1.Value && x.SaleDate <= date2.Value);
                return;
            }

            if (date1.HasValue)
            {
                ShowItems(x => x.SaleDate == date1.Value);
                return;
            }

            if (date2.HasValue)
            {
                ShowItems(x => x.SaleDate == date2.Value);
                return;
            }
            ShowItems();
        }

        private void DtFind_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            Date_Pickers(dtFind.SelectedDate, dtFind2.SelectedDate);
        }

        private void DtFind2_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            Date_Pickers(dtFind.SelectedDate, dtFind2.SelectedDate);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(PrintGrid, "Grid");
            }
        }
    }
}
