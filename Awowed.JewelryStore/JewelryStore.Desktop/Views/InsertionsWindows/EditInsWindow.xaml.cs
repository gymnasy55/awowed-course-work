using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для EditInsWindow.xaml
    /// </summary>
    public partial class EditInsWindow : Window
    {

        private readonly AppDbContext _context = new AppDbContext();

        private InsertionItemViewModel _vm;
        private IQueryable<Insertion> _insertions;
        private List<string> _gemCategory = new List<string> { "", "Напівкоштовний", "Коштовний" };
        public EditInsWindow(InsertionItemViewModel vm)
        {
            _vm = vm;
            InitializeComponent();
        }

        private void TextBoxes_OnPreviewSpaceClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }
        private void IntPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void EditInsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            _context.Insertions.Load();

            _insertions = _context.Insertions;
            var GemCategory = new List<string> { "", "Напівкоштовний", "Коштовний" };

            TbInsert.Text = _vm.InsertName;
            TbInsertColor.Text = _vm.InsertColor;

            foreach (var gemCat in GemCategory)
            {
                CbGemCategory.Items.Add(gemCat);
            }

            CbGemCategory.SelectedIndex = _gemCategory.IndexOf(_vm.GemCategory);
            TbPrice.Text = $"{_vm.Price}";
            TbWorkPrice.Text =$"{_vm.WorkPrice}";
        }
        private void EditBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (TbPrice.Text == string.Empty || TbWorkPrice.Text == string.Empty)
            {
                {
                    MessageBox.Show("Ви не заповнили одне з полів: Ціна за карат, Ціна за роботу!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            var result = MessageBox.Show("Чи впевнені Ви, що бажаєте змінити вставку?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var insertion = _context.Insertions.FirstOrDefault(x => x.Id == _vm.Id);
                    if (insertion != null)
                    { 
                        insertion.InsertName = TbInsert.Text.Trim();
                        insertion.InsertColor = TbInsertColor.Text.Trim();
                        insertion.GemCategory = CbGemCategory.SelectionBoxItem.ToString()?.Trim();
                        insertion.Price = float.Parse(TbPrice.Text);
                        insertion.WorkPrice = float.Parse(TbWorkPrice.Text);
                        if (TbWorkPrice.Text == string.Empty || TbPrice.Text == string.Empty)
                        {
                            MessageBox.Show("Введіть ціни вставки!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (insertion.InsertName == string.Empty)
                        {
                            MessageBox.Show("Введіть назву вставки!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (insertion.InsertColor == string.Empty)
                        {
                            MessageBox.Show("Введіть колір вставки!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        var metals = _context.Metals.ToList();
                        foreach (var product in _context.Products.Where(prod => prod.IdIns == insertion.Id).Where(prod => !prod.IsSold))
                        {
                            var metal = metals.FirstOrDefault(met => met.Id == product.IdMet);
                            if (metal != null)
                            {
                                product.Price =
                                    (float)Math.Round(metal.Price * product.Weight + insertion.Price * product.Carat, 1);
                                product.PriceForTheWork =
                                    (float)Math.Round(metal.WorkPrice * product.Weight + insertion.WorkPrice * product.Carat, 1);

                            }
                        }
                        _context.SaveChanges();
                        MessageBox.Show("Успішно змінена вставка в бд!", "Успіх", MessageBoxButton.OK,
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
            TbInsert.Text = string.Empty;
            TbInsertColor.Text = string.Empty;
            CbGemCategory.Text = string.Empty;
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
