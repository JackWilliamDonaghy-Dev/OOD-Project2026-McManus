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

        internal ObservableCollection<string> messages = new ObservableCollection<string>();
        internal ObservableCollection<Person> people = new ObservableCollection<Person>();


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lsbxMessages.ItemsSource = messages;
            lsbxPeople.ItemsSource = people;

            
        }

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
            
            // new window and connection set
            AddMessagesWindow win1 = new AddMessagesWindow();
            win1.Owner = this;
            win1.ShowDialog();
        }


        private void btnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            // Create person and add to People list on main list page
            string name = txtbxName.Text;
            int frequency = int.Parse(txtbxFreq.Text);
            ObservableCollection<string> personMessages = messages;
            Person person = new Person(name, frequency, personMessages);
            people.Add(person);

            //clear/reset add window values
            txtbxName.Text = "";
            txtbxFreq.Text = "14";
            messages.Clear();

            //Sort people
            var sorted = people
                        .OrderByDescending(p => GetUrgency(p))
                        .ThenBy(p => p.DueDate)
                        .ToList();
            people = new ObservableCollection<Person>(sorted);
            lsbxPeople.ItemsSource = people;

        }

        //Manage People list

        private double GetUrgency(Person person)
        {
            var due = person.DueDate.Date;
            var difference = DateTime.Now - due;
            log.Info($"due -{due} now -{DateTime.Now} difference -{difference}");
            return difference.TotalDays;
        }

        private void btnContacted_Click(object sender, RoutedEventArgs e)
        {
            Person person = lsbxPeople.SelectedItem as Person;
            if (person != null)
            {

            }
            else
            {
                person.LastContacted = DateTime.Now;
            }
        }

        private void lsbxPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // new window and connection set
            PersonDetailsWindow win2 = new PersonDetailsWindow();
            win2.Owner = this;
            win2.ShowDialog();
        }
    }
}
