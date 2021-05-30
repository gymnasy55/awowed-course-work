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
    /// Логика взаимодействия для MetalItem.xaml
    /// </summary>
    public partial class MetalItem : UserControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty TextBlockContentProperty = DependencyProperty.Register(
            "TextBlockContent", typeof(string), typeof(MetalItem), new PropertyMetadata(default(string)));

        public string TextBlockContent
        {
            get => (string)GetValue(TextBlockContentProperty);
            set => SetValue(TextBlockContentProperty, value);
        }

        #endregion

        #region Private Fields

        private readonly MetalItemViewModel _vm;
        private bool _isExpanded;

        #endregion

        #region Constructor

        public MetalItem(MetalItemViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            _isExpanded = false;
            using var context = new AppDbContext();
            TextBlockContent = _vm.MetalName + $" {_vm.Sample}";
        }

        #endregion

        #region Buttons

            private void ExpandButton_OnClick(object sender, RoutedEventArgs e)
            {
                if (!_isExpanded)
                {
                    using (var context = new AppDbContext())
                    {
                        ItemStackPanel.Children.Add(new TextBlock
                        {
                            Text = $"Ціна за грам: {_vm.Price}",
                            Margin = new Thickness(5),
                            TextAlignment = TextAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            FontFamily = new FontFamily("Calibri"),
                            FontSize = 16,
                        });
                        ItemStackPanel.Children.Add(new TextBlock
                        {
                            Text = $"Ціна за роботу: {_vm.WorkPrice}",
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
                var editMetWindow = new EditMetWindow(_vm);
                editMetWindow.ShowDialog();
            }

            private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
            {
                using var context = new AppDbContext();
                var metal = context.Metals.FirstOrDefault(x => x.Id == _vm.Id);
                if (metal != null)
                {
                    var result = MessageBox.Show("Чи впевнені Ви, що бажаєте видалити метал?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            context.Metals.Remove(metal);
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
