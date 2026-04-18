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
using System.CodeDom.Compiler;

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
            using (PersonData db = new PersonData())
            {
                var results = db.People.ToList();

                people.Clear();

                foreach (var person in results)
                {
                    people.Add(person);
                }
            }

            lsbxPeople.SelectedIndex = -1;

            sortPeopleList();

            // Refresh Clickable Buttons
            HomeScreenUsabilityFeatures();
        }

        public void SavePeople()
        {
            using (PersonData db = new PersonData())
            {
                foreach (var person in people.ToList())
                {
                    if (person.PersonID == 0)
                    {
                        db.People.Add(person);
                    }
                    else
                    {
                        var existingPerson = db.People.Find(person.PersonID);

                        if (existingPerson == null)
                        {
                            db.People.Add(person);
                        }
                        else
                        {
                            db.Entry(existingPerson).CurrentValues.SetValues(person);
                        }
                    }
                }

                db.SaveChanges();

                var currentPersonIds = people
                    .Where(person => person.PersonID != 0)
                    .Select(person => person.PersonID)
                    .ToList();

                var removedPeople = db.People
                    .Where(dbPerson => !currentPersonIds.Contains(dbPerson.PersonID))
                    .ToList();

                foreach (var removedPerson in removedPeople)
                {
                    db.People.Remove(removedPerson);
                }

                db.SaveChanges();
            }

            LoadPeople();
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
        private void btnAddMessage_Click(object sender, RoutedEventArgs e)
        {
            AddMessagesWindow win1 = new AddMessagesWindow(messages);
            win1.ShowDialog();
        }


        private void btnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            NotificationService notificationService = new NotificationService(this);
            // Create person and add to People list on main list page
            string name = txtbxName.Text;
            if (name.Length > 0)
            {
                int frequency = int.Parse(txtbxFreq.Text);
                if (frequency > 0)
                {
                    ObservableCollection<string> personMessages = new ObservableCollection<string>(messages);
                    Person person = new Person(name, frequency, personMessages);
                    people.Add(person);

                    //clear/reset add window values
                    txtbxName.Text = "";
                    txtbxFreq.Text = "14";
                    messages.Clear();

                    sortPeopleList();
                    SavePeople();
                
                    notificationService.ShowSuccess($"{name} has been added");

                }
                else
                {
                    notificationService.ShowWarning("Frequency below 1");
                }
            }
            else
            {
                notificationService.ShowWarning($"No name entered");
            }
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
            if (person.DueDate == null)
                return 0; // or maybe -1 depending on meaning

            var due = person.DueDate.Value.Date;
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
        }

        private void btn_EditPerson(object sender, RoutedEventArgs e)
        {
            //Open edit person window opened
            if (selectedPerson != null)
            {
                EditPersonWindow win2 = new EditPersonWindow();
                win2.Owner = this;
                win2.ShowDialog();
            }
        }
    }
}
