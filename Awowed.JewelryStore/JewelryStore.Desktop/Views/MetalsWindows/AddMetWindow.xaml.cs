using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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

        private void IntPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void AddBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Чи впевнені Ви, що бажаєте додати метал?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var metal = new Metal
                    {
                        Id =  (byte) (_context.Metals.OrderBy(x => x.Id).Last().Id + 1),
                        MetalName = TbMetal.Text.Trim(),
                        Sample = System.Convert.ToInt32(TbSample.Text)
                    };
                    if (_context.Metals.Any(x => x.Sample == metal.Sample))
                    {
                        MessageBox.Show("Такий метал вже є в бд", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    _context.Metals.Add(metal);
                    _context.SaveChanges();
                    MessageBox.Show("Додано метал в бд!");
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
    }
}
