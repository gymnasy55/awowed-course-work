using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using JewelryStore.Desktop.Models;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для AddSuppWindow.xaml
    /// </summary>
    public partial class AddSuppWindow : Window
    {

        private readonly AppDbContext _context = new AppDbContext();

        private IQueryable<Supplier> _suppliers;
        public AddSuppWindow()
        {
            InitializeComponent();
        }

        private void ClearBtn_Clicked(object sender, RoutedEventArgs e)
        {
            TbSupp.Text = string.Empty;
        }

        private void AddBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Чи впевнені Ви, що бажаєте додати постачальника?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var supplier = new Supplier
                    {
                        Id = (byte)(_context.Suppliers.OrderBy(x => x.Id).Last().Id + 1),
                        Suplname = TbSupp.Text.Trim(),
                    };
                    if (_context.Suppliers.Any(x => x.Suplname == supplier.Suplname))
                    {
                        MessageBox.Show("Такий постачальник вже є в бд!","Помилка",MessageBoxButton.OK, MessageBoxImage.Error  );
                        return;
                    }
                    if (supplier.Suplname == "")
                    {
                        MessageBox.Show("Введіть назву постачальника!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    _context.Suppliers.Add(supplier);
                    _context.SaveChanges();
                    MessageBox.Show("Додано постачальника в бд!");
                    break;
                case MessageBoxResult.No:
                    break;
            }
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
