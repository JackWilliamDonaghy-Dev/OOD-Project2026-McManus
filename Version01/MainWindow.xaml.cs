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
using System.IO;
using System.Text.Json;

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
        internal Person selectedPerson = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lsbxMessages.ItemsSource = messages;
            lsbxPeople.ItemsSource = people;

            LoadPeople();
        }

        public void LoadPeople()
        {
            if (File.Exists("../../people.json"))
            {
                string json = File.ReadAllText("../../people.json");

                var loadedPeople = JsonSerializer.Deserialize<ObservableCollection<Person>>(json);

                if (loadedPeople != null)
                {
                    people.Clear();

                    foreach (var p in loadedPeople)
                    {
                        people.Add(p);
                    }
                }
            }

            lsbxPeople.SelectedIndex = -1;

            // Refresh Clickable Buttons
            HomeScreenUsabilityFeatures();
        }

        public void SavePeople()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonString2 = JsonSerializer.Serialize(people, options);
            File.WriteAllText("../../people.json", jsonString2);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SavePeople();
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
            ObservableCollection<string> personMessages = new ObservableCollection<string>(messages);
            Person person = new Person(name, frequency, personMessages);
            people.Add(person);

            //clear/reset add window values
            txtbxName.Text = "";
            txtbxFreq.Text = "14";
            messages.Clear();

            sortPeopleList();

        }

        public void sortPeopleList()
        {
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



        private void lsbxPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Person is now applicable
            selectedPerson = lsbxPeople.SelectedItem as Person;

            // Change buttons to look clickable
            HomeScreenUsabilityFeatures();
        }

        public void HomeScreenUsabilityFeatures()
        {
            // Change buttons to look clickable
            if (selectedPerson != null)
            {
                btnPersonDetails.Foreground = Brushes.White;
                btnAccountEdit.Foreground = Brushes.White;
                btnQuickContact.Foreground = Brushes.White;
            }
            else
            {
                btnPersonDetails.Foreground = Brushes.Gray;
                btnAccountEdit.Foreground = Brushes.Gray;
                btnQuickContact.Foreground = Brushes.Gray;
            }
        }

        private void btnPersonDetails_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPerson != null)
            {
                PersonDetailsWindow win2 = new PersonDetailsWindow();
                win2.Owner = this;
                win2.ShowDialog();
            }
        }

        private void btnQuickContact_Click(object sender, RoutedEventArgs e)
        {
            selectedPerson.LastContacted = DateTime.Now;
            SavePeople();
            LoadPeople();
        }
    }
}
