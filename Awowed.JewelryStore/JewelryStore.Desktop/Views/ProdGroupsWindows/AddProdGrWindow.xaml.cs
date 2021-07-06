using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using JewelryStore.Desktop.Models;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для AddProdGrWindow.xaml
    /// </summary>
    public partial class AddProdGrWindow : Window
    {

        private readonly AppDbContext _context = new AppDbContext();

        private IQueryable<Prodgroup> _prodgroups;
        public AddProdGrWindow()
        {
            InitializeComponent();
        }

        private void AddBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Чи впевнені Ви, що бажаєте додати групу виробу?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var prodgroup = new Prodgroup
                    {
                        Id = _context.Prodgroups.Count() > 0 ? (byte)(_context.Prodgroups.OrderBy(x => x.Id).Last().Id + 1) : (byte) 1,
                        ProdGroupName = TbProdGroupName.Text.Trim()
                    };
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
                    _context.Prodgroups.Add(prodgroup);
                    _context.SaveChanges();
                    MessageBox.Show("Додано групу виробу в бд!");
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
