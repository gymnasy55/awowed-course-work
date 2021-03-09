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

        #region Constructor

        public JewerlyItem()
        {
            InitializeComponent();
        }

        #endregion
        //TODO: ACTIONS ON THESE BUTTS
        private void ExpandButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChangeButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PrintTagButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
