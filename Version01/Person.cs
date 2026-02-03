using System;
using System.Collections.Generic;
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
        

        private int lastID = 1;

        public Person(string name, int frequency, DateTime lastContacted)
        {
            Name = name;
            Frequency = frequency;
            LastContacted = lastContacted;

            PersonID = lastID;
            lastID++;
        }

        public Person(string name, int frequency)
        {
            Name = name;
            Frequency = frequency;

            LastContacted = DateTime.Now;
            PersonID = lastID;
            lastID++;
        }


        public Person() { }



    }
}
