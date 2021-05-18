using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using JewelryStore.Desktop.Models;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для AddInsWindow.xaml
    /// </summary>
    public partial class AddInsWindow : Window
    {

        private readonly AppDbContext _context = new AppDbContext();

        private IQueryable<Insertion> _insertions;

        public AddInsWindow()
        {
            InitializeComponent();
        }

        private void AddInsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var GemCategory = new List<string> { "", "Напівкоштовний", "Коштовний" };

            foreach (var gemCat in GemCategory)
            {
                CbGemCategory.Items.Add(gemCat);
            }
        }

        private void AddBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Чи впевнені Ви, що бажаєте додати вставку?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var insertion = new Insertion
                    {
                        Id = (byte)(_context.Insertions.OrderBy(x => x.Id).Last().Id + 1),
                        InsertName = TbInsert.Text.Trim(),
                        InsertColor = TbInsertColor.Text.Trim(),
                        GemCategory = CbGemCategory.SelectionBoxItem.ToString()?.Trim()

                    };
                    if ((_context.Insertions.Any(x => x.InsertName == insertion.InsertName)) && (_context.Insertions.Any(x => x.InsertColor == insertion.InsertColor)))
                    {
                        MessageBox.Show("Така вставка вже є в бд", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    _context.Insertions.Add(insertion);
                    _context.SaveChanges();
                    MessageBox.Show("Додано вставку в бд!");
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
