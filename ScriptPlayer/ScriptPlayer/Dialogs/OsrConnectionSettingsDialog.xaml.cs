using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScriptPlayer.Dialogs
{
    /// <summary>
    /// Interaction logic for OsrConnectionSettingsDialog.xaml
    /// </summary>
    public partial class OsrConnectionSettingsDialog : Window
    {
        public static readonly DependencyProperty ComPortProperty = DependencyProperty.Register(
            "ComPort", typeof(string), typeof(OsrConnectionSettingsDialog), new PropertyMetadata(default(string)));

        public string ComPort
        {
            get { return (string) GetValue(ComPortProperty); }
            set { SetValue(ComPortProperty, value); }
        }

        public static readonly DependencyProperty BaudRateProperty = DependencyProperty.Register(
            "BaudRate", typeof(string), typeof(VlcConnectionSettingsDialog), new PropertyMetadata(default(string)));

        public string BaudRate
        {
            get { return (string) GetValue(BaudRateProperty); }
            set { SetValue(BaudRateProperty, value); }
        }

        private string[] portnames;
        public List<string> PortsList { get; set; }

        public OsrConnectionSettingsDialog(string ipAndPort, string password)
        {
            InitializeComponent();

            using (var searcher = new ManagementObjectSearcher
                ("SELECT * FROM WIN32_SerialPort"))
            {
                portnames = SerialPort.GetPortNames();
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();
                PortsList = (from n in portnames
                             join p in ports on n equals p["DeviceID"].ToString()
                             select n + " - " + p["Caption"]).ToList();
            }
            cmbComPort.ItemsSource = PortsList;

            //Force 115200 for now...
            txtBaudRate.IsEnabled = false;
            txtBaudRate.Text = "115200";
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ((Button) sender).Focus();

            ComPort = portnames[cmbComPort.SelectedIndex];
            BaudRate = txtBaudRate.Text;

            DialogResult = true;
        }
    }
}
