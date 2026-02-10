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
using System.Xml.Schema;

namespace Version01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //error logging initialization
        log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        // Frequency logic
        private void btnFreqSub_Click(object sender, RoutedEventArgs e)
        {
            bool total = int.TryParse(txtbxFreq.Text, out int freq);
            txtbxFreq.Text = (freq - 1).ToString();
        }

        private void btnFreqAdd_Click(object sender, RoutedEventArgs e)
        {
            bool total = int.TryParse(txtbxFreq.Text, out int freq);
            txtbxFreq.Text = (freq + 1).ToString();
        }
        // Messages
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Owner = this;
            win1.ShowDialog();
        }

    }
}
