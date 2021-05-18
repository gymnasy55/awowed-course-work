using System;
using System.Windows;
using System.IO.Ports;
using System.Windows.Input;
using System.Windows.Threading;

namespace JewelryStore.Desktop.Views.ProductsWindows
{
    /// <summary>
    /// Логика взаимодействия для ScanWindow.xaml
    /// </summary>
    public partial class ScanWindow : Window
    {
        private SerialPort _serialPort;
        public ScanWindow()
        {
            InitializeComponent();

            _serialPort = new SerialPort("COM1")
            {
                BaudRate = 9600,
                Parity = Parity.None,
                StopBits = StopBits.One,
                ReceivedBytesThreshold = 8,
                DataBits = 8,
                Handshake = Handshake.None,
                RtsEnable = true,
                DtrEnable = true
            };
            _serialPort.Open();
        }

        private void IntPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox.Text != string.Empty && TextBox.Text.Length == 8)
            {
                ListBox.Items.Add(TextBox.Text);
                TextBox.Text = string.Empty;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
