using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BarcodeLib;
using JewelryStore.Desktop.Models;
using JewelryStore.Desktop.ViewModels;
using Microsoft.Win32;
using Color = System.Drawing.Color;
using Size = System.Windows.Size;

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

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using var memory = new MemoryStream();
            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
            memory.Position = 0;
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memory;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            return bitmapImage;
        }

        private void ArticleWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var text = _vm.BarCode;
            var barcode = new Barcode();
            var barcodeImage = barcode.Encode(TYPE.EAN8, text, Color.Black, 
                Color.White, (int)BarcodeImage.Width, (int)BarcodeImage.Height);
            BarcodeImage.Source = BitmapToImageSource(new Bitmap(barcodeImage));

            TbArticle.Text = _vm.ProdItem;
            TbBarcode.Text = _vm.BarCode;
            TbWeight.Text = $"{_vm.Weight}";
            TbDate.Text = _vm.ArrivalDateString;
            TbSize.Text = $"{_vm.ProdSize}";
            TbPrice.Text = $"{_vm.Price}";
            using (var context = new AppDbContext())
            {
                TbMetal.Text = $"{context.Metals.First(x => x.Id == _vm.IdMet).MetalName}";
                TbSample.Text = $"{context.Metals.First(x => x.Id == _vm.IdMet).Sample}";
                TbGroup.Text = $"{context.Prodgroups.First(x => x.Id == _vm.IdProdGr).ProdGroupName}";
            }
            ResultTbArticle.Text = $"Арт: {TbArticle.Text}";
            ResultTbArticle2.Text = $"Арт: {TbArticle.Text}";
            ResultTbGroup.Text = TbGroup.Text;
            ResultTbMetalSample.Text = $"{TbMetal.Text} {TbSample.Text}°";
            ResultTbPrice.Text = $"Ціна: {TbPrice.Text} UAH";
            ResultTbPrice2.Text = $"Ціна: {TbPrice.Text} UAH";
            ResultTbSize.Text = $"Розмір: {TbSize.Text}";
            ResultTbWeight.Text = $"Вага: {TbWeight.Text}";
            ResultTbWeight2.Text = $"Вага: {TbWeight.Text}";
            ResultTbBarcode.Text = TbBarcode.Text;
            ResultTbDate.Text = TbDate.Text;
        }

        //TODO: Save like an image and change article design

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new SaveFileDialog
            {
                Filter = "PNG files|*.png|All Files|*.*", 
                Title = "Save diagram as PNG"
            };
            if (fileDialog.ShowDialog() != true) return;
            var bitmap = new RenderTargetBitmap((int)PrintGrid.ActualWidth, (int)PrintGrid.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(PrintGrid);
            using var stream = File.Create(fileDialog.FileName);
            var encoder = new JpegBitmapEncoder { QualityLevel = 300 };
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(stream);
        }
    }
}
