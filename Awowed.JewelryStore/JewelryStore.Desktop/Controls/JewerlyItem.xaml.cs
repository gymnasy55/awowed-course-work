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
        private bool _isExpanded;

        #endregion

        #region Constructor

        public JewerlyItem(JewerlyItemViewModel jewerlyItemViewModel)
        {
            InitializeComponent();

            _jewerlyItemViewModel = jewerlyItemViewModel;
            _isExpanded = false;
            using var context = new AppDbContext();
            TextBlockContent = _jewerlyItemViewModel.ProdItem + $" Постачальник: {context.Suppliers.FirstOrDefault(x => x.Id == _jewerlyItemViewModel.IdSupp).Suplname}";
        }

        #endregion

        #region Private Methods

        //TODO: ACTIONS ON THESE BUTTS
        private void ExpandButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_isExpanded)
            {
                using (var context = new AppDbContext())
                {
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Штрих-код: {_jewerlyItemViewModel.BarCode}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Дата прибуття: {_jewerlyItemViewModel.ArrivalDate?.ToString()}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Метал: {context.Metals.FirstOrDefault(x => x.Id == _jewerlyItemViewModel.IdMet).MetalName}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Група виробу: {context.Prodgroups.FirstOrDefault(x => x.Id == _jewerlyItemViewModel.IdProdGr).ProdGroupName}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Розмір: {_jewerlyItemViewModel.ProdSize}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Вага: {_jewerlyItemViewModel.Weight}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Чиста Вага: {_jewerlyItemViewModel.ClearWeight}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Вставка: {context.Insertions.FirstOrDefault(x => x.Id == _jewerlyItemViewModel.IdIns).InsertName}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Ціна за роботу: {_jewerlyItemViewModel.PriceForTheWork}",
                        Margin = new Thickness(5),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 16,
                    });
                    ItemStackPanel.Children.Add(new TextBlock
                    {
                        Text = $"Ціна: {_jewerlyItemViewModel.Price}",
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

        #endregion
    }
}
