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
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BarcodeLib;
using JewelryStore.Desktop.ViewModels;

namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Interaction logic for ArticleWindow.xaml
    /// </summary>
    public partial class ArticleWindow : Window
    {
        private readonly JewerlyItemViewModel _vm;

        public ArticleWindow(JewerlyItemViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
        }

        private void ArticleWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var text = txtText.Text;
            var barcode = new Barcode();
            var barcodeImage = barcode.Encode(TYPE.EAN8, text, Color.Black, Color.White, pictureBox1.Width - 20, pictureBox1.Height - 20);
        }
    }
}
