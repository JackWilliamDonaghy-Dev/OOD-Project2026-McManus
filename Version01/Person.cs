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
        public DateTime? LastContacted { get; set; }
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
        public DateTime? DueDate
        {
            get
            {
               //checks if empty
            if (LastContacted == null)
                return null;

               //checks if empty
            return LastContacted.Value.Date.AddDays(Frequency);
                
            }
        }



        public Person(string name, int frequency, ObservableCollection<string> messages, DateTime lastContacted)
        {
            Name = name;
            Frequency = frequency;
            Messages = messages;
            LastContacted = lastContacted;

        }

        public Person(string name, int frequency, ObservableCollection<string> messages)
        {
            Name = name;
            Frequency = frequency;
            Messages = messages;

            //Set to null that is recognised as never contacted
            LastContacted = null;
        }

        public Person(string name, int frequency)
        {
            Name = name;
            Frequency = frequency;

            Messages = new ObservableCollection<string>();
            LastContacted = DateTime.Now;
        }


        public Person() { }


        public override string ToString()
        {
            if (DueDate == null || LastContacted == null)
                return $"{Name} - Never";

            int due = (DueDate.Value - LastContacted.Value).Days-13;
            due = due > 0 ? due : 0;

            return $"{Name} - {due}";
        }


    }//end of class

    public class PersonData : DbContext
    {
        public PersonData() : base("People2026_03_11_1500") { }

        public DbSet<Person> People { get; set; }
    }

}
