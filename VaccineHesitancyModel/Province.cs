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
        public int totalInhabitants; //Calculated at initialisation
        public double deniedVaccinations;

        public PopulationCluster nativeWorkers; //People only work is same province
        public List<(PopulationCluster, Province)> outgoingCommuters = []; //commuters from THIS province and its outgoing province

        public Province(int id, string name, PopulationCluster native)
        {
            this.id = id;
            this.name = name;
            nativeWorkers = native;
            this.totalInhabitants = (int)nativeWorkers.total; //commuters added later on
        }

        public void AddCommuters(PopulationCluster pop, Province prov)
        {
            outgoingCommuters.Add((pop,prov));
            totalInhabitants += (int)pop.total;
        }

        //Use native workers + commuters present at the moment
        public double TotalPresent(List<PopulationCluster> present)
        {
            return nativeWorkers.susceptible + present.Sum(e => e.total);
        }
        public double TotalSusceptible(List<PopulationCluster> present)
        {
            return nativeWorkers.susceptible + present.Sum(e => e.susceptible);
        }
        public double TotalInfected(List<PopulationCluster> present)
        {
            return nativeWorkers.infected + present.Sum(e => e.infected);
        }
        public double TotalRecovered(List<PopulationCluster> present)
        {
            return nativeWorkers.recovered + present.Sum(e => e.recovered);
        }
        public double TotalVaccinated(List<PopulationCluster> present)
        {
            return nativeWorkers.vaccinated + present.Sum(e => e.vaccinated);
        }



        public bool IsInfected()
        {
            return TotalInfected(outgoingCommuters.Select(e => e.Item1).ToList()) > 0;
        }

        public void PrintStatus()
        {
            string status =
                "inhabitants: " + totalInhabitants + '\n' +
                "susceptible: " + TotalSusceptible(outgoingCommuters.Select(e => e.Item1).ToList()) + "\n" +
                "infected: " + TotalInfected(outgoingCommuters.Select(e => e.Item1).ToList()) + "\n" +
                "recovered: " + TotalRecovered(outgoingCommuters.Select(e => e.Item1).ToList()) + "\n" +
                "vaccinated: " + TotalVaccinated(outgoingCommuters.Select(e => e.Item1).ToList());

            Console.WriteLine(status);
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
