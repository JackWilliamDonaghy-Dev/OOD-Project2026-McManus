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
using System.Windows.Shapes;

namespace Version01
{
    /// <summary>
    /// Interaction logic for PersonDetailsWindow.xaml
    /// </summary>
    public partial class PersonDetailsWindow : Window
    {
        public PersonDetailsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow main = this.Owner as MainWindow;

            Person person = main.lsbxPeople.SelectedItem as Person;
            txtblckName.Text = person.Name;
            lsbxMessages.ItemsSource = person.Messages;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
