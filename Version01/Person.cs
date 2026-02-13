using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace Version01
{
    internal class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public DateTime LastContacted { get; set; }
        public int Frequency { get; set; }
        public ObservableCollection<string> Messages { get; set; }

        public DateTime DueDate
        {
            get
            {
                if ()
                return LastContacted.Date.AddDays(Frequency);
            }
        }
        

        protected int lastID = 1;

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
            LastContacted = new DateTime(1,1,1);
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
            return $"{Name} - {Frequency}";
        }


    }
}
