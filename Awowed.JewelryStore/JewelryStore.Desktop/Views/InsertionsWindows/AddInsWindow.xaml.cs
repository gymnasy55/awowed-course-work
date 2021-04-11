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

namespace JewelryStore.Desktop.Views.InsertionsWindows
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
                        GemCategory = TbGemCategory.Text.Trim()
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
            TbGemCategory.Text = string.Empty;
        }
    }
}
