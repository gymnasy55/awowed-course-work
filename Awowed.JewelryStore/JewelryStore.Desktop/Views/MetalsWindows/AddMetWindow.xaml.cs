using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using JewelryStore.Desktop.Models;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для AddMetWindow.xaml
    /// </summary>
    public partial class AddMetWindow : Window
    {

        private readonly AppDbContext _context = new AppDbContext();

        private IQueryable<Metal> _metals;
        public AddMetWindow()
        {
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

        private void AddBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (TbPrice.Text == string.Empty || TbWorkPrice.Text == string.Empty)
            {
                {
                    MessageBox.Show("Ви не заповнили одне з полів: Ціна за грам, Ціна за роботу!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            var result = MessageBox.Show("Чи впевнені Ви, що бажаєте додати метал?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (TbSample.Text != string.Empty && TbSample.Text.Length == 3 && TbWorkPrice.Text != String.Empty && TbPrice.Text != String.Empty)
                    {
                        var metal = new Metal
                        {
                            Id = (byte)(_context.Metals.OrderBy(x => x.Id).Last().Id + 1),
                            MetalName = TbMetal.Text.Trim(),
                            Sample = System.Convert.ToInt32(TbSample.Text),
                            Price = float.Parse(TbPrice.Text),
                            WorkPrice = float.Parse(TbWorkPrice.Text)

                        };
                        if (_context.Metals.Any(x => x.Sample == metal.Sample) && _context.Metals.Any(x => x.MetalName == metal.MetalName))
                        {
                            MessageBox.Show("Такий метал вже є в бд", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (metal.MetalName == String.Empty)
                        {
                            MessageBox.Show("Введіть назву металу!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        _context.Metals.Add(metal);
                        _context.SaveChanges();
                        MessageBox.Show("Додано метал в бд!");
                    }
                    else
                    {
                        MessageBox.Show("Помилка при додаванні в бд!", "Помилка", MessageBoxButton.OK,
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
