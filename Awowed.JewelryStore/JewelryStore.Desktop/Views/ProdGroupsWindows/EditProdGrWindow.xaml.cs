using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                        if (_context.Prodgroups.Any(x => x.ProdGroupName == prodgroup.ProdGroupName))
                        {
                            MessageBox.Show("Така група виробу вже є в бд", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (prodgroup.ProdGroupName == "")
                        {
                            MessageBox.Show("Введіть назву групи виробу!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
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

        private void TextBoxes_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text[^1].ToString(), "\"|'"))
                return;

            if (!(sender is TextBox textBox))
                return;

            textBox.Text += '`';
            textBox.CaretIndex = textBox.Text.Length;

            e.Handled = true;
        }
    }
}
