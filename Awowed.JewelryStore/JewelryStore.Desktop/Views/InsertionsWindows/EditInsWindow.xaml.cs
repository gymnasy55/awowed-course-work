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
using JewelryStore.Desktop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.Views.InsertionsWindows
{
    /// <summary>
    /// Логика взаимодействия для EditInsWindow.xaml
    /// </summary>
    public partial class EditInsWindow : Window
    {

        private readonly AppDbContext _context = new AppDbContext();

        private InsertionItemViewModel _vm;
        private IQueryable<Insertion> _insertions;
        public EditInsWindow(InsertionItemViewModel vm)
        {
            _vm = vm;
            InitializeComponent();
        }

        private void EditInsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            _context.Insertions.Load();

            _insertions = _context.Insertions;

            TbInsert.Text = _vm.InsertName;
            TbInsertColor.Text = _vm.InsertColor;
            TbGemCategory.Text = _vm.GemCategory;
        }
        private void EditBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Чи впевнені Ви, що бажаєте змінити вставку?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var insertion = _context.Insertions.FirstOrDefault(x => x.Id == _vm.Id);
                    if(insertion != null)
                    { 
                        insertion.InsertName = TbInsert.Text.Trim();
                        insertion.InsertColor = TbInsertColor.Text.Trim();
                        insertion.GemCategory = TbGemCategory.Text.Trim();
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
            TbGemCategory.Text = string.Empty;
        }
    }
}
