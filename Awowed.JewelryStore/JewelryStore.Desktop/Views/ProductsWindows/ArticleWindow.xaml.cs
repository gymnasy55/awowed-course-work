using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using JewelryStore.Desktop.Models;
using JewelryStore.Desktop.ViewModels;
using Microsoft.Win32;
using ZXing;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Xceed.Words.NET;
using Document = Spire.Doc.Document;


namespace JewelryStore.Desktop.Views
{
    /// <summary>
    /// Interaction logic for ArticleWindow.xaml
    /// </summary>
    public partial class ArticleWindow : System.Windows.Window
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
            var writer = new BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
            var img = writer.Write(text);
            BarcodeImage.Source = BitmapToImageSource(img);

            TbArticle.Text = _vm.ProdItem;
            TbBarcode.Text = _vm.BarCode;
            TbWeight.Text = $"{_vm.Weight}";
            TbDate.Text = _vm.ArrivalDateString;
            TbSize.Text = $"{_vm.ProdSize}";
            TbPrice.Text = $"{_vm.Price}";
            TbPriceWork.Text = $"{_vm.PriceForTheWork}";
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
            ResultTbPriceWork.Text = $"Ціна за роботу: {TbPriceWork.Text} UAH";
            ResultTbSize.Text = $"Розмір: {TbSize.Text}";
            ResultTbWeight.Text = $"Вага: {TbWeight.Text}";
            ResultTbWeight2.Text = $"Вага: {TbWeight.Text}";
            ResultTbDate.Text = TbDate.Text;

            var folderPath = @"d:\Label_App_Folder";
            if (!Directory.Exists(folderPath)) 
                Directory.CreateDirectory(folderPath);
        }

        public Dictionary<string, string> GetReplaceDictionary1()
        {
            var replaceDict = new Dictionary<string, string>
            {
                {"#prod_group#", TbGroup.Text},
                {"#metal#", $"{TbMetal.Text} {TbSample.Text}°"},
                {"#article#", TbArticle.Text.Trim()},
                {"#size#", $"Розмір: {TbSize.Text}"},
                {"#weight#", $"Вага: {TbWeight.Text}"},
                {"#work_price#", $"{TbPriceWork.Text} UAH"}
            };
            return replaceDict;
        }

        public Dictionary<string, string> GetReplaceDictionary2()
        {
            var replaceDict = new Dictionary<string, string>
            {
                {"#date#", $"{TbDate.Text}"},
                {"#article#", $"{TbArticle.Text}"},
                {"#weight#", $"Вага: {TbWeight.Text}"},
                {"#price#", $"{TbPrice.Text} UAH"},
            };
            return replaceDict;
        }

        private void FirstPartButton_OnClick(object sender, RoutedEventArgs e)
        {
            var newPath = Directory.GetCurrentDirectory();
            var document1 = new Document();
            var document2 = new Document();
            var samplePath1 = $@"{newPath}\Label1.docx";
            var samplePath2 = $@"{newPath}\Label2.docx";
            document1.LoadFromFile(samplePath1);
            document2.LoadFromFile(samplePath2);

            var folderItemPath = $@"d:\Label_App_Folder\Товар_{TbBarcode.Text.Trim()}";
            if (!Directory.Exists(folderItemPath))
                Directory.CreateDirectory(folderItemPath);

            var dictReplace1 = GetReplaceDictionary1();
            var dictReplace2 = GetReplaceDictionary2();

            foreach (var keyValuePair in dictReplace1)
            {
                document1.Replace(keyValuePair.Key, keyValuePair.Value, true, true);
            }
            foreach (var keyValuePair in dictReplace2)
            {
                document2.Replace(keyValuePair.Key, keyValuePair.Value, true, true);
            }

            #region Image

            var text = _vm.BarCode;
            var writer = new BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
            var img = writer.Write(text);
            BarcodeImage.Source = BitmapToImageSource(img);

            var imagePath = $@"{folderItemPath}\img_{TbBarcode.Text.Trim()}.jpg";
            img.Save(imagePath);

            if (File.Exists(imagePath))
            {
                Section section = document2.Sections[0];
                Paragraph paragraph = section.AddParagraph();
                DocPicture picture = paragraph.AppendPicture(System.Drawing.Image.FromFile(imagePath));
                picture.Width = 66;
                picture.Height = 50;
            }
            #endregion

            var fileName1 = $@"{folderItemPath}\{TbBarcode.Text.Trim()}_1.docx";
            var fileName2 = $@"{folderItemPath}\{TbBarcode.Text.Trim()}_2.docx";
            if (!File.Exists(fileName1) || !File.Exists(fileName2))
            {
                document1.SaveToFile(fileName1);
                document2.SaveToFile(fileName2);
                MessageBox.Show("Створено 2 файли-бірки", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                document1.Close();
                document2.Close();
                return;
            }
            MessageBox.Show("Вже є така бирка!", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
            document1.Close();
            document2.Close();
        }
    }
}
