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
using System.Windows.Navigation;
using System.Windows.Shapes;
using JewelryStore.Desktop.Models;
using JewelryStore.Desktop.ViewModels;
using JewelryStore.Desktop.Views;
using Microsoft.EntityFrameworkCore;

namespace JewelryStore.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for JewerlyItem.xaml
    /// </summary>
    public partial class JewerlyItem : UserControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty TextBlockContentProperty = DependencyProperty.Register(
            "TextBlockContent", typeof(string), typeof(JewerlyItem), new PropertyMetadata(default(string)));

        public string TextBlockContent
        {
            get => (string)GetValue(TextBlockContentProperty);
            set => SetValue(TextBlockContentProperty, value);
        }

        #endregion

        #region Private Fields

        private readonly JewerlyItemViewModel _jewerlyItemViewModel;

        #endregion

        #region Constructor

        public JewerlyItem(JewerlyItemViewModel jewerlyItemViewModel)
        {
            InitializeComponent();

            _jewerlyItemViewModel = jewerlyItemViewModel;

            TextBlockContent = _jewerlyItemViewModel.ProdItem;
        }

        #endregion

        //TODO: ACTIONS ON THESE BUTTS
        private void ExpandButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChangeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditWindow(_jewerlyItemViewModel);
            editWindow.Show();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            using var context = new AppDbContext();
            var product = context.Products.FirstOrDefault(x => x.Id == _jewerlyItemViewModel.Id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                MessageBox.Show("Успішно видалено з бд!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Помилка видалення з бд!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrintTagButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
