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
        public EditMetWindow(MetalItemViewModel vm)
        {
            _vm = vm;
            InitializeComponent();
        }

        private void EditMetWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            _context.Metals.Load();
            _context.Insertions.Load();
            _context.Products.Load();


            TbMetal.Text = _vm.MetalName;
            TbSample.Text = $"{_vm.Sample}";
            TbPrice.Text = $"{_vm.Price}";
            TbWorkPrice.Text = $"{_vm.WorkPrice}";
        }
        private void TextBoxes_OnPreviewSpaceClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        private void IntPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void EditBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (TbPrice.Text == string.Empty || TbWorkPrice.Text == string.Empty)
            {
                {
                    MessageBox.Show("Ви не заповнили одне з полів: Ціна за грам, Ціна за роботу!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            var result = MessageBox.Show("Чи впевнені Ви, що бажаєте  змінити метал?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var metal = _context.Metals.FirstOrDefault(x => x.Id == _vm.Id);
                    if (metal != null && TbSample.Text != string.Empty && TbSample.Text.Length == 3)
                    {
                        metal.MetalName = TbMetal.Text.Trim();
                        metal.Sample = System.Convert.ToInt32(TbSample.Text);
                        metal.Price = float.Parse(TbPrice.Text);
                        metal.WorkPrice = float.Parse(TbWorkPrice.Text);
                        if (metal.MetalName == string.Empty)
                        {
                            MessageBox.Show("Введіть назву металу!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (TbWorkPrice.Text == string.Empty || TbPrice.Text == string.Empty)
                        {
                            MessageBox.Show("Введіть ціни металу!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        var insertions = _context.Insertions.ToList();
                        foreach (var product in _context.Products.Where(product => product.IdMet == metal.Id && !product.IsSold))
                        {
                            var insertion = insertions.FirstOrDefault(insert => insert.Id == product.IdIns);
                            if (insertion != null)
                            {
                                product.Price =
                                    (float)Math.Round(metal.Price * product.Weight + insertion.Price * product.Carat, 1);
                                product.PriceForTheWork =
                                    (float)Math.Round(metal.WorkPrice * product.Weight + insertion.WorkPrice * product.Carat, 1);
                            }
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
            TbPrice.Text = string.Empty;
            TbWorkPrice.Text = string.Empty;
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
