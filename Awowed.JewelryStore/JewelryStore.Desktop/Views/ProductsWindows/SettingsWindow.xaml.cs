using System;
using System.Globalization;
using System.Windows;
using JewelryStore.Desktop.Models;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Зберегти параметри?", "Увага!", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Cancel:
                    return;
                case MessageBoxResult.Yes:
                    Settings.GramWorkPrice = Convert.ToSingle(PricePerGramWorkTb.Text == string.Empty
                        ? Settings.GramWorkPrice.ToString(CultureInfo.InvariantCulture)
                        : PricePerGramWorkTb.Text);
                    Settings.GramSalePrice = Convert.ToSingle(PricePerGramSaleTb.Text == string.Empty
                        ? Settings.GramSalePrice.ToString(CultureInfo.InvariantCulture)
                        : PricePerGramSaleTb.Text);
                    Settings.WriteConfig();
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            this.Close();
        }

        private void SettingsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Settings.ReadConfig();
            PricePerGramWorkTb.Text = Settings.GramWorkPrice.ToString(CultureInfo.InvariantCulture);
            PricePerGramSaleTb.Text = Settings.GramSalePrice.ToString(CultureInfo.InvariantCulture);
        }

        private void AddMetWindowBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var addMetWindow = new AddMetWindow();
            addMetWindow.ShowDialog();
        }

        private void AddSuppWindowBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var addSuppWindow = new AddSuppWindow();
            addSuppWindow.ShowDialog();
        }
    }
}
