using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineHesitancyModel
{
    internal class StatusPoint
    {
        public int generation;
        public double susceptible;
        public double infected;
        public double recovered;
        public double vaccinated;

        public StatusPoint(int generation, double susceptible, double infected, double recovered, double vaccinated)
        {
            this.generation = generation;
            this.susceptible = susceptible;
            this.infected = infected;
            this.recovered = recovered;
            this.vaccinated = vaccinated;
        }
    }
}
