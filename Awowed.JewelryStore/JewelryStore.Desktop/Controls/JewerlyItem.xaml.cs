using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using JewelryStore.Desktop.Models;
using JewelryStore.Desktop.ViewModels;
using JewelryStore.Desktop.Views;

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

        private readonly JewerlyItemViewModel _vm;
        private bool _isExpanded;

        #endregion

        #region Constructor

        public JewerlyItem(JewerlyItemViewModel vm)
        {
            InitializeComponent();

            _vm = vm;
            _isExpanded = false;
            using var context = new AppDbContext();
            TextBlockContent = _vm.ProdItem + $" Постачальник: {context.Suppliers.FirstOrDefault(x => x.Id == _vm.IdSupp).Suplname} Продаж: {(_vm.IsSold ? "Продано" : "Не продано")}";
        }

        #endregion

        #region Private Methods

        private void ExpandButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_isExpanded)
            {
                using (var context = new AppDbContext())
                {
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Штрих-код: {_vm.BarCode}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Дата прибуття: {_vm.ArrivalDate?.ToShortDateString()}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Метал: {context.Metals.FirstOrDefault(x => x.Id == _vm.IdMet).MetalName} {context.Metals.FirstOrDefault(x => x.Id == _vm.IdMet).Sample}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Група виробу: {context.Prodgroups.FirstOrDefault(x => x.Id == _vm.IdProdGr).ProdGroupName}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Розмір: {_vm.ProdSize}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Вага: {_vm.Weight}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Чиста Вага: {_vm.ClearWeight}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Вставка: {context.Insertions.FirstOrDefault(x => x.Id == _vm.IdIns).InsertName} | {context.Insertions.FirstOrDefault(x => x.Id == _vm.IdIns).InsertColor}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Огранка: {_vm.Faceting}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Спосіб плетіння: {_vm.WeaveWay}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Тип плетіння: {_vm.WeaveType}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Ціна за роботу: {_vm.PriceForTheWork}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Ціна: {_vm.Price}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                }

                _isExpanded = true;
                ExpandIcon.Data = Geometry.Parse("M7.41,15.41L12,10.83L16.59,15.41L18,14L12,8L6,14L7.41,15.41Z");
                ExpandButton.ToolTip = "Згорнути";
            }
            else
            {
                ItemStackPanel.Children.RemoveRange(1, ItemStackPanel.Children.Count - 1);
                _isExpanded = false;
                ExpandIcon.Data = Geometry.Parse("M7.41,8.58L12,13.17L16.59,8.58L18,10L12,16L6,10L7.41,8.58Z");
                ExpandButton.ToolTip = "Розгорнути";
            }
            
        }

        private void ChangeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditWindow(_vm);
            editWindow.Show();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            using var context = new AppDbContext();
            var product = context.Products.FirstOrDefault(x => x.Id == _vm.Id);
            if (product != null)
            {
                var result = MessageBox.Show("Чи впевнені Ви, що бажаєте видалити товар?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        context.Products.Remove(product);
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

        private void PrintTagButton_OnClick(object sender, RoutedEventArgs e)
        {
            var articleWindow = new ArticleWindow(_vm);
            articleWindow.ShowDialog();
        }

        #endregion
    }
}
