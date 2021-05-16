using System.Linq;
using System.Windows;
using JewelryStore.Desktop.Models;
using JewelryStore.Desktop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для EditSuppWindow.xaml
    /// </summary>
    public partial class EditSuppWindow : Window
    {
        private readonly AppDbContext _context = new AppDbContext();

        private SupplierItemViewModel _vm;
        private IQueryable<Supplier> _suppliers;
        public EditSuppWindow(SupplierItemViewModel vm)
        {
            _vm = vm;
            InitializeComponent();
        }

        private void EditSuppWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            _context.Suppliers.Load();

            _suppliers = _context.Suppliers;

            TbSupp.Text = _vm.Suplname;
        }

        private void EditBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Чи впевнені Ви, що бажаєте змінити постачальника?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == _vm.Id);
                    if(supplier != null)
                    {
                        supplier.Suplname = TbSupp.Text.Trim();
                        _context.SaveChanges();
                        MessageBox.Show("Успішно змінено постачальника в бд!", "Успіх", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    else 
                    {
                        MessageBox.Show("Помилка при зміні в бд!", "Помилка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void ClearBtn_Clicked(object sender, RoutedEventArgs e)
        {
            TbSupp.Text = string.Empty;
        }
    }
}
