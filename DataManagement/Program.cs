using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Version01;

namespace DataManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PersonData db = new PersonData();

            using(db)
            {
                //create the seed data
                ObservableCollection<string> testMessages = new ObservableCollection<string>();
                testMessages.Add("Hello");
                testMessages.Add("What the dog doing");
                testMessages.Add("Beanz");
                Console.WriteLine("Person created");

                Person p1 = new Person() {PersonID = 1, Frequency = 14, LastContacted = DateTime.Now, Name = "Tom", CombinedMessages = "Beanz,Hi,Yo" };

                //add to table
                db.People.Add(p1);
                Console.WriteLine("Person added to DB");

                //save changes
                db.SaveChanges();
                Console.WriteLine("Database saved");



            }
        }
    }
}
