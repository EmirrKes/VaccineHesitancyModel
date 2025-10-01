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
            for (int i = 0; i < generations; i++)
            {
                foreach (Province province in provinces)              
                    province.Update();

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
        public int TotalInfected()
        {
            int count = 0;
            foreach (Province province in provinces)
                count += province.infected;

            return count;
        }

        public void PrintStatus()
        {
            string status = 
                "generation: " + generation + ",\n" +
                "inhabitants: " + TotalInfected().ToString() + ",\n" +
                "infected: " + TotalInfected().ToString() + ",\n" +
                '\n'
                ;

            Console.WriteLine(status);
        }
    }
}
