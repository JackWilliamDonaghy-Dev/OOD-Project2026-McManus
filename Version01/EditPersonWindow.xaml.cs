using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EditPersonWindow.xaml
    /// </summary>
    public partial class EditPersonWindow : Window
    {
        public EditPersonWindow()
        {
            InitializeComponent();
        }

        internal Person mainPerson = new Person();
        internal MainWindow main = new MainWindow();
        internal ObservableCollection<string> messages = new ObservableCollection<string>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            main = this.Owner as MainWindow;

            mainPerson = main.lsbxPeople.SelectedItem as Person;
            txtbxName.Text = mainPerson.Name;
            lsbxMessages.ItemsSource = mainPerson.Messages;
            messages = mainPerson.Messages;

            //Add Last Contacted loadup
            if (mainPerson.LastContacted == null)
            {
                txtblckLastContact.Text = "Never Contacted";
            }
            else if (mainPerson.LastContacted?.Date == DateTime.Now.Date)
            {
                txtblckLastContact.Text = "Today";
            }
            else
            {
                txtblckLastContact.Text = mainPerson.LastContacted.ToString();
            }
        }

        private void btnAddMessage_Click(object sender, RoutedEventArgs e)
        {

            // new window and connection set
            AddMessagesWindow win3 = new AddMessagesWindow(mainPerson);
            win3.ShowDialog();
        }
    

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //Change the made changes
            mainPerson.Name = txtbxName.Text;


            //classic save all work
            main.SavePeople();
            main.LoadPeople();
            Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            main.people.Remove(mainPerson);
            main.SavePeople();
            main.LoadPeople();
            Close();
        }
    }
}
