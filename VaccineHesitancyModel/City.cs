using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineHesitancyModel
{
    internal class Province
    {
        List<Person> inhabitants = new List<Person>();
        List<Province> neighbours = new List<Province>();
    }

    internal class Person
    {
        bool susceptible = true;
        bool infected;
        bool recovered = false;
        bool hesitant;

        public Person(bool infected, bool hesitant)
        {
            this.infected = infected;
            this.hesitant = hesitant;
        }

        public string PrintStatus()
        {
            string status =
                "susceptible = " + susceptible + '\n' +
                "infected = " + infected + '\n' +
                "recovered = " + recovered + '\n' +
                "hesitant = " + hesitant;

            return status;
        }
    }
}
