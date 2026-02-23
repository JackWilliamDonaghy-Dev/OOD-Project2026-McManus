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

        internal Person mainPerson = new Person();
        internal MainWindow main = new MainWindow();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            main = this.Owner as MainWindow;

            mainPerson = main.lsbxPeople.SelectedItem as Person;
            txtblckName.Text = mainPerson.Name;
            lsbxMessages.ItemsSource = mainPerson.Messages;

            //Add Last Contacted loadup
            if (mainPerson.LastContacted == new DateTime(1, 1, 1))
            {
                txtblckLastContact.Text = "Never Contacted";
            }
            else if (mainPerson.LastContacted.Date == DateTime.Now.Date)
            {
                txtblckLastContact.Text = "Today";
            }
            else
            {
                txtblckLastContact.Text = mainPerson.LastContacted.ToString();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            main.SavePeople();
            main.LoadPeople();
            Close();
        }

        private void btnContacted_Click(object sender, RoutedEventArgs e)
        {
            //Add change Contacted last
            mainPerson.LastContacted = DateTime.Now;
            txtblckLastContact.Text = "Today";
        }
    }
}
