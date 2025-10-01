using System;
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
        public int infected = 0;
        List<Province> neighbours = new List<Province>();

        public Province(int id, string name, int inhabitants)
        {
            this.id = id;
            this.name = name;
            this.inhabitants = inhabitants;
        }

        public void AddNeighbour(Province neighbour)
        {
            neighbours.Add(neighbour);
            neighbour.neighbours.Add(this);
        }

        public void AddNeighbours(List<Province> neighbours)
        {
            foreach (Province neighbour in neighbours)
            {
                neighbours.Add(neighbour);
                neighbour.neighbours.Add(this);
            }
        }

        public bool IsInfected()
        {
            return infected > 0;
        }

        public void PrintStatus()
        {
            string status =
                "inhabitants = " + inhabitants + '\n' +
                "infected = " + infected;

            Console.WriteLine(status);
        }

        public void Update()
        {
            // ---------- //
            // TODO Joris //
            // ---------- //
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
