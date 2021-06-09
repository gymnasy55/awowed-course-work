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

        private void SettingsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Settings.ReadConfig();
            cbOpen.Items.Add("Вставки");
            cbOpen.Items.Add("Групи Виробів");
            cbOpen.Items.Add("Метали");
            cbOpen.Items.Add("Постачальники");

        }

        private void OpenFromCbWindowBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var result = cbOpen.SelectionBoxItem;
            switch (result)
            {
                case "Вставки":
                    var insertionsMainWindow = new InsertionsMainWindow();
                    insertionsMainWindow.ShowDialog();
                    break;
                case "Групи Виробів":
                    var prodGroupsMainwWindow = new ProdGroupsMainWindow();
                    prodGroupsMainwWindow.ShowDialog();
                    break;
                case "Метали":
                    var metalsMainWindow = new MetalsMainWindow();
                    metalsMainWindow.ShowDialog();
                    break;
                case "Постачальники":
                    var suppliersMainWindow = new SuppliersMainWindow();
                    suppliersMainWindow.ShowDialog();
                    break;
            }
        }
    }
}
