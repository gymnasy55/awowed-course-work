using System.Linq;
using System.Windows;
using JewelryStore.Desktop.Models;
using JewelryStore.Desktop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для EditProdGrWindow.xaml
    /// </summary>
    public partial class EditProdGrWindow : Window
    {
        private readonly AppDbContext _context = new AppDbContext();

        private ProdGroupItemViewModel _vm;
        private IQueryable<Prodgroup> _prodgroups;

        public EditProdGrWindow(ProdGroupItemViewModel vm)
        {
            _vm = vm;
            InitializeComponent();
        }

        private void EditProdGrWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            _context.Prodgroups.Load();

            _prodgroups = _context.Prodgroups;

            TbProdGroupName.Text = _vm.ProdGroupName;
        }

        private void EditBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Чи впевнені Ви, що бажаєте змінити групу виробу?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var prodgroup = _context.Prodgroups.FirstOrDefault(x => x.Id == _vm.Id);
                    if(prodgroup != null)
                    {
                        prodgroup.ProdGroupName = TbProdGroupName.Text.Trim();
                        _context.SaveChanges();
                        MessageBox.Show("Успішно змінено група виробу в бд!", "Успіх", MessageBoxButton.OK,
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
            TbProdGroupName.Text = string.Empty;
        }
    }
}
