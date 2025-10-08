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
        public int commuters = 0;

        public List<(Province, int)> neighbours = new List<(Province,int)>(); //province and incoming commuters

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
            neighbour.commuters += commuters;
            //neighbour.neighbours.Add(this);
        }

        public void AddNeighbours(List<(Province, int)> neighbours)
        {
            foreach ((Province neighbour, int commuters) all in neighbours)
            {
                neighbours.Add(all);
                all.neighbour.commuters += commuters;
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

        public (double, double, double, double) Update(double vaccinations)
        {

            Dictionary<string, double> provinceData = GetInhabitantProbabilities();
            double combined_infected = infected - commuters * provinceData["infected"];
            double combined_susceptible = susceptible - commuters * provinceData["susceptible"];
            double combined_recovered = recovered - commuters * provinceData["recovered"];
            double combined_vaccinated = vaccinated - commuters * provinceData["vaccinated"];
            double total_commuters = 0;

            foreach ((Province, int) neighbour_data in neighbours)
            {
                Dictionary<string, double> dict = neighbour_data.Item1.GetInhabitantProbabilities();
                combined_infected += neighbour_data.Item2 * dict["infected"];
                combined_susceptible += neighbour_data.Item2 * dict["susceptible"];
                combined_recovered += neighbour_data.Item2 * dict["recovered"];
                combined_vaccinated += neighbour_data.Item2 * dict["vaccinated"];
                total_commuters += neighbour_data.Item2;
            }


            double newInfected = 0.43 * combined_susceptible * combined_infected / (inhabitants - commuters + total_commuters);
            double newRecovered = (0.2 * combined_infected);

            double inhabitantsPercentage = 1 - (total_commuters / (inhabitants - commuters + total_commuters));
            double CommutersPercentage_divided = (1 - inhabitantsPercentage) / neighbours.Count();

            susceptible -= newInfected * inhabitantsPercentage;
            infected += newInfected * inhabitantsPercentage - newRecovered * inhabitantsPercentage;
            recovered += newRecovered * inhabitantsPercentage;



            return (newInfected, newRecovered, CommutersPercentage_divided, total_commuters);
        }

        public Dictionary<string, double> GetInhabitantProbabilities()
        {
            Dictionary<string, double> dict = new Dictionary<string, double>();
            dict.Add("susceptible", susceptible / inhabitants);
            dict.Add("recovered", recovered / inhabitants);
            dict.Add("infected", infected / inhabitants);
            dict.Add("vaccinated", vaccinated / inhabitants);
            return dict;
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
