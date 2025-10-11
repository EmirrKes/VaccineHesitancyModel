using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineHesitancyModel
{
    internal class Simulation
    {
        int generation = 0;
        List<Province> provinces;
        public List<StatusPoint> pastStatuses = new List<StatusPoint>();

        public Simulation(List<Province> provinces)
        {
            this.provinces = provinces;
        }

        public void Progress(int generations)
        {
            double averageVaccineAvailability = 32877;
            double averageVaccineHesitancy = 0.42;

            for (int i = 0; i < generations; i++)
            {
                foreach (Province province in provinces)
                {
                    // !!! Vaccinations need to be added + correct split of commuted/not commuted for graphs
                    
                    

                    //At home
                    Update(province, province.nativeWorkers, province.outgoingCommuters.Select(e => e.Item1).ToList());

                    //At work
                    List<(PopulationCluster, Province)> incoming = [];
                    foreach (Province province2 in provinces)
                    {
                        if (province2.id != province.id)
                            incoming.Add(province2.outgoingCommuters.Find(e => e.Item2 == province));
                    }
                    Update(province, province.nativeWorkers, incoming.Select(e => e.Item1).ToList());

                    
        
                }

                //second iteration, such that all native and commuters can update before changing vaccination status

                //int hallo = 5;
                foreach (Province province in provinces)
                {
                    int hallo = ModelInhabitants();
                    double baseVaccinations =  averageVaccineAvailability * (province.totalInhabitants / (double)hallo);
                    double availableVaccinations = baseVaccinations + province.deniedVaccinations;
                    if (province.nativeWorkers.susceptible >= availableVaccinations)
                    {
                        province.nativeWorkers.susceptible -= availableVaccinations * (1 - averageVaccineHesitancy);
                        province.nativeWorkers.vaccinated += availableVaccinations * (1 - averageVaccineHesitancy);
                        province.deniedVaccinations = availableVaccinations * averageVaccineHesitancy;
                    }
                    else
                    {
                        province.deniedVaccinations = province.nativeWorkers.susceptible * averageVaccineHesitancy;
                        province.nativeWorkers.vaccinated += province.nativeWorkers.susceptible * (1 - averageVaccineHesitancy);
                        province.nativeWorkers.susceptible -= province.nativeWorkers.susceptible * (1 - averageVaccineHesitancy);                     
                    }
                }

                generation++;
                PrintStatus();
                SaveStatus();
            }
        }


        //Given the provinces NativeWorkers and the commuters(incoming or at home)
        //current province, native workers, at home workers/incoming commuters, vaccinations
        public void Update(Province prov, PopulationCluster native, List<PopulationCluster> present)
        {

            //for each cluster, increase values with this percentage. If maxed out, cap it
            foreach (PopulationCluster cluster in present)
            {
                double dS = 0.5 * -0.43 * prov.TotalInfected(present) * (cluster.susceptible / prov.TotalPresent(present));
                double dR = 0.5 * 0.2 * cluster.infected;
                double dI = -dS - dR;

                cluster.susceptible = Math.Max(0, cluster.susceptible + dS);
                cluster.infected = Math.Max(0, Math.Min(cluster.total, cluster.infected + dI));
                cluster.recovered = Math.Min(cluster.total, cluster.recovered + dR);
            }

            //Also for native population
            double dS2 = 0.5 * -0.43 * prov.TotalInfected(present) * (native.susceptible / prov.TotalPresent(present));
            double dR2 = 0.5 * 0.2 * native.infected;
            double dI2 = -dS2 - dR2;

            native.susceptible = Math.Max(0, native.susceptible + dS2);
            native.infected = Math.Max(0, Math.Min(native.total, native.infected + dI2));
            native.recovered = Math.Min(native.total, native.recovered + dR2);
        }



        
        public int ModelInhabitants()
        {
            int count = 0;
            foreach (Province province in provinces)
                count += province.totalInhabitants;

            return count;
        }
        public double ModelInfected()
        {
            double count = 0;
            foreach (Province province in provinces)
                count += province.TotalInfected(province.outgoingCommuters.Select(e => e.Item1).ToList());

            return count;
        }
        public double ModelRecovered()
        {
            double count = 0;
            foreach (Province province in provinces)
                count += province.TotalRecovered(province.outgoingCommuters.Select(e => e.Item1).ToList());

            return count;
        }
        public double ModelSusceptible()
        {
            double count = 0;
            foreach (Province province in provinces)
                count += province.TotalSusceptible(province.outgoingCommuters.Select(e => e.Item1).ToList());

            return count;
        }
        public double ModelVaccinated()
        {
            double count = 0;
            foreach (Province province in provinces)
                count += province.TotalVaccinated(province.outgoingCommuters.Select(e => e.Item1).ToList());

            return count;
        }
        

        public void PrintStatus()
        {
            
            string status = 
                "generation: " + generation + ",\n" +
                "inhabitants: " + ModelInhabitants().ToString() + ",\n" +
                "infected: " + ModelInfected().ToString() + ",\n" +
                "recovered: " + ModelRecovered().ToString() + ",\n" +
                "susceptible: " + ModelSusceptible().ToString() + ",\n" +
                "vaccinated: " + ModelVaccinated().ToString() + ",\n" +
                '\n'
                ;
            Console.WriteLine(status);
            
            //Console.WriteLine(ModelRecovered());

        }

        private void SaveStatus()
        {
            StatusPoint SP = new StatusPoint(generation, ModelSusceptible(), ModelInfected(),
                                                                ModelRecovered(), ModelVaccinated());
            pastStatuses.Add(SP);
        }
    }
}
