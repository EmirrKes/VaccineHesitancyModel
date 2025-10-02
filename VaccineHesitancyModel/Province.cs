using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineHesitancyModel
{
    internal class Province
    {
        public int id;
        public string name;
        public int inhabitants;
        public double susceptible;
        public double infected = 0;
        public double recovered = 0;
        public double vaccinated = 0;

        List<(Province, int)> neighbours = new List<(Province,int)>(); //province and incoming commuters

        public Province(int id, string name, int inhabitants)
        {
            this.id = id;
            this.name = name;
            this.inhabitants = inhabitants;
            susceptible = inhabitants;
        }

        public void AddNeighbour(Province neighbour, int commuters)
        {
            neighbours.Add((neighbour, commuters));
            //neighbour.neighbours.Add(this);
        }

        public void AddNeighbours(List<(Province, int)> neighbours)
        {
            foreach ((Province neighbour, int commuters) all in neighbours)
            {
                neighbours.Add(all);
                //neighbour.neighbours.Add(this);
            }
        }

        public bool IsInfected()
        {
            return infected > 0;
        }

        public void PrintStatus()
        {
            string status =
                "inhabitants: " + inhabitants + '\n' +
                "susceptible: " + susceptible + "\n" +
                "infected: " + infected + "\n" +
                "recovered: " + recovered + "\n" +
                "vaccinated: " + vaccinated;

            Console.WriteLine(status);
        }

        public void Update(double vaccinations)
        {
            // ---------- //
            // TODO Joris //
            // ---------- //


            double newInfected = (0.43 * susceptible * infected / inhabitants);
            double newRecovered = (0.2 * infected);

            susceptible -= newInfected;
            infected += newInfected - newRecovered;
            recovered += newRecovered;

            if (susceptible > vaccinations)
            {
                vaccinated += vaccinations;
                susceptible -= vaccinations;
            }
        }
    }

    // ---------------------------- //
    // Might not use "Person" class //
    // ---------------------------- //

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

        public void PrintStatus()
        {
            string status =
                "susceptible = " + susceptible + '\n' +
                "infected = " + infected + '\n' +
                "recovered = " + recovered + '\n' +
                "hesitant = " + hesitant;

            Console.WriteLine(status);
        }
    }
}
