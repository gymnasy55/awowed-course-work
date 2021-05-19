using System.Windows;
using System.Windows.Controls;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                IsEnabled = false;
                var printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(MainGrid, "Grid");
                }
            }
            finally
            {
                IsEnabled = true;
            }
        }
    }
}
