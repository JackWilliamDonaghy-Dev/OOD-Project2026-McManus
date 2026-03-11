using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace Version01
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public DateTime LastContacted { get; set; }
        public int Frequency { get; set; }

        [NotMapped]
        public ObservableCollection<string> Messages { get; set; } = new ObservableCollection<string>();


        private string combinedMessages;
        public string CombinedMessages
        {
            //convert the ob coll to a comma delimited string
            get => string.Join(",", Messages);

            //convert the comma delimited string to an observable collection
            set
            {
                combinedMessages = value;
                Messages.Clear();

                if (string.IsNullOrWhiteSpace(combinedMessages))
                {
                    return;
                }

                //break into parts and add to ob collection
                string[] indivMessages = combinedMessages.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var message in indivMessages)
                {
                    Messages.Add(message);

                }

            }

        }


        [NotMapped]
        public DateTime DueDate
        {
            get
            {
                return LastContacted.Date.AddDays(Frequency);
            }
        }


        private static int lastID = 1;

        public Person(string name, int frequency, ObservableCollection<string> messages, DateTime lastContacted)
        {
            Name = name;
            Frequency = frequency;
            Messages = messages;
            LastContacted = lastContacted;

            PersonID = lastID;
            lastID++;
        }

        public Person(string name, int frequency, ObservableCollection<string> messages)
        {
            Name = name;
            Frequency = frequency;
            Messages = messages;

            //Set to very specific date that is recognised as never contacted
            LastContacted = new DateTime(1, 1, 1);
            PersonID = lastID;
            lastID++;
        }

        public Person(string name, int frequency)
        {
            Name = name;
            Frequency = frequency;

            Messages = new ObservableCollection<string>();
            LastContacted = DateTime.Now;
            PersonID = lastID;
            lastID++;
        }


        public Person() { }

        public override string ToString()
        {
            int due = DueDate.Day - LastContacted.Day > 0 ? DueDate.Day - LastContacted.Day : 0;
            return $"{this.Name} - {due}";
        }


    }//end of class

    public class PersonData : DbContext
    {
        public PersonData() : base("People2026_03_11_1500") { }

        public DbSet<Person> People { get; set; }
    }

}
