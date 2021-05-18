using System;
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
    /// Логика взаимодействия для EditMetWindow.xaml
    /// </summary>
    public partial class EditMetWindow : Window
    {
        private readonly AppDbContext _context = new AppDbContext();

        private MetalItemViewModel _vm;
        private IQueryable<Metal> _metals;
        public EditMetWindow(MetalItemViewModel vm)
        {
            _vm = vm;
            InitializeComponent();
        }

        private void EditMetWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            _context.Metals.Load();

            _metals = _context.Metals;

            TbMetal.Text = _vm.MetalName;
            TbSample.Text = $"{_vm.Sample}";
        }
        private void IntPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void EditBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Чи впевнені Ви, що бажаєте  змінити метал?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var metal = _context.Metals.FirstOrDefault(x => x.Id == _vm.Id);
                    if(metal != null && TbSample.Text != string.Empty && TbSample.Text.Length == 3)
                    {
                        metal.MetalName = TbMetal.Text.Trim();
                        metal.Sample = System.Convert.ToInt32(TbSample.Text);
                        if (_context.Metals.Any(x => x.Sample == metal.Sample) && _context.Metals.Any(x => x.MetalName == metal.MetalName))
                        {
                            MessageBox.Show("Такий метал вже є в бд", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        _context.SaveChanges();
                        MessageBox.Show("Успішно змінено метал в бд!", "Успіх", MessageBoxButton.OK,
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
            TbMetal.Text = string.Empty;
            TbSample.Text = string.Empty;
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
