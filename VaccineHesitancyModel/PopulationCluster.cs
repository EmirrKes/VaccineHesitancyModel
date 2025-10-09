using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineHesitancyModel
{
    internal class PopulationCluster
    {
        public double total = 0;
        public double susceptible = 0;
        public double infected = 0;
        public double recovered = 0;
        public double vaccinated = 0;

        public PopulationCluster(double sus, double inf, double rec, double vac)
        {
            total = sus + inf + rec + vac;
            susceptible = sus;
            infected = inf;
            recovered = rec;
            vaccinated = vac;
        }
    }
}
