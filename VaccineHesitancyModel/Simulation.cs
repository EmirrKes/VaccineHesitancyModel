using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineHesitancyModel
{
    internal class Simulation
    {
        int generation = 0;
        List<Province> provinces;


        public Simulation(List<Province> provinces)
        {
            this.provinces = provinces;
        }

        public void Progress(int generations)
        {
            double baseVaccinations = 1000;
            int deniedvaccinations = 0;

            for (int i = 0; i < generations; i++)
            {
                foreach (Province province in provinces)
                    province.Update((double)province.inhabitants / TotalInhabitants() * baseVaccinations); 

                generation++;
                PrintStatus();
            }
        }

        public int TotalInhabitants()
        {
            int count = 0;
            foreach (Province province in provinces)
                count += province.inhabitants;

            return count;
        }
        public double TotalInfected()
        {
            double count = 0;
            foreach (Province province in provinces)
                count += province.infected;

            return count;
        }
        public double TotalRecovered()
        {
            double count = 0;
            foreach (Province province in provinces)
                count += province.recovered;

            return count;
        }
        public double TotalSusceptible()
        {
            double count = 0;
            foreach (Province province in provinces)
                count += province.susceptible;

            return count;
        }
        public double TotalVaccinated()
        {
            double count = 0;
            foreach (Province province in provinces)
                count += province.vaccinated;

            return count;
        }

        public void PrintStatus()
        {
            string status = 
                "generation: " + generation + ",\n" +
                "inhabitants: " + TotalInhabitants().ToString() + ",\n" +
                "infected: " + TotalInfected().ToString() + ",\n" +
                "recovered: " + TotalRecovered().ToString() + ",\n" +
                "susceptible: " + TotalSusceptible().ToString() + ",\n" +
                "vaccinated: " + TotalVaccinated().ToString() + ",\n" +
                '\n'
                ;

            Console.WriteLine(status);
        }
    }
}
