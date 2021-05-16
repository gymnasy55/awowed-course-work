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

namespace JewelryStore.Desktop.Controls
{
    /// <summary>
    /// Логика взаимодействия для ProdGroupItem.xaml
    /// </summary>
    public partial class ProdGroupItem : UserControl
    {

        #region Dependency Properties

        public static readonly DependencyProperty TextBlockContentProperty = DependencyProperty.Register(
            "TextBlockContent", typeof(string), typeof(ProdGroupItem), new PropertyMetadata(default(string)));

        public string TextBlockContent
        {
            get => (string)GetValue(TextBlockContentProperty);
            set => SetValue(TextBlockContentProperty, value);
        }

        #endregion

        #region Private Fields

        private readonly ProdGroupItemViewModel _vm;

        #endregion

        #region Constructor

        public ProdGroupItem(ProdGroupItemViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            using var context = new AppDbContext();
            TextBlockContent = _vm.ProdGroupName;
        }

        #endregion

        #region Private Methods

        private void ChangeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var editProdGrWindow = new EditProdGrWindow(_vm);
            editProdGrWindow.ShowDialog();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            using var context = new AppDbContext();
            var prodgroup = context.Prodgroups.FirstOrDefault(x => x.Id == _vm.Id);
            if (prodgroup != null)
            {
                var result = MessageBox.Show("Чи впевнені Ви, що бажаєте видалити групу виробів?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        context.Prodgroups.Remove(prodgroup);
                        context.SaveChanges();
                        MessageBox.Show("Успішно видалено з бд!", "Успіх", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Помилка видалення з бд!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

    }
}
