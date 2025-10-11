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
        public double highestInfections = 0;
        public double highestRecovered = 0;

        public Simulation(List<Province> provinces)
        {
            this.provinces = provinces;
        }

        public void Progress(int generations, double VaccineHesitancy)
        {
            double averageVaccineAvailability = 32877;
            double absoluteVaccineRefusal = 0.3 * VaccineHesitancy; //subset of absolute refusing people (NOT FACTUAL)
            double averageVaccineHesitancy = 0.7 * VaccineHesitancy; //subset of hesitant people (NOT FACTUAL)

            double acceptenceRate = 0.3; //how often of the time hesitating people accept (NOT FACTUAL)

            //init subsets
            foreach (Province province in provinces)
            {
                province.nativeWorkers.vaccineAccepters = province.nativeWorkers.total * (1 - VaccineHesitancy);
                province.nativeWorkers.vaccineHesitators = province.nativeWorkers.total * averageVaccineHesitancy;
                province.nativeWorkers.vaccineRefusers = province.nativeWorkers.total * absoluteVaccineRefusal;

                foreach ( PopulationCluster cluster in province.outgoingCommuters.Select(e => e.Item1).ToList())
                {
                    cluster.vaccineAccepters = cluster.total * (1 - VaccineHesitancy);
                    cluster.vaccineHesitators = cluster.total * averageVaccineHesitancy;
                    cluster.vaccineRefusers = cluster.total * absoluteVaccineRefusal;
                }
            }

            for (int i = 0; i < generations; i++)
            {
                foreach (Province province in provinces)
                {               
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

                foreach (Province province in provinces)
                {
                    double baseVaccinations =  averageVaccineAvailability * (province.totalInhabitants / (double)ModelInhabitants());
                    double availableVaccinations = baseVaccinations + province.deniedVaccinations;

                    List<PopulationCluster> outgoingCommuters = province.outgoingCommuters.Select(x => x.Item1).ToList();
                    double percentageNative = province.nativeWorkers.susceptible / province.TotalSusceptible(outgoingCommuters);
                    double percentageEachCommutingCluster = (1 - percentageNative) / outgoingCommuters.Count();

                    double percentageNativeAccepters = province.nativeWorkers.vaccineAccepters / province.nativeWorkers.susceptible;
                    double percentageNativeHesitators = province.nativeWorkers.vaccineHesitators / province.nativeWorkers.susceptible;                  

                    if (province.TotalSusceptible(outgoingCommuters) >= availableVaccinations)
                    {
                        double toChange = availableVaccinations * percentageNativeAccepters + (percentageNativeHesitators * acceptenceRate);

                        province.nativeWorkers.susceptible -= toChange * percentageNative;
                        province.nativeWorkers.vaccineAccepters -= toChange * percentageNative * percentageNativeAccepters;
                        province.nativeWorkers.vaccineHesitators -= toChange * percentageNative * percentageNativeHesitators;
                        province.nativeWorkers.vaccinated += toChange * percentageNative;
                        foreach (PopulationCluster cluster in outgoingCommuters)
                        {
                            double percentageClusterAccepters = cluster.vaccineAccepters / cluster.susceptible;
                            double percentageClusterHesitators = cluster.vaccineHesitators / cluster.susceptible;

                            double sum = (cluster.susceptible / outgoingCommuters.Sum(x => x.susceptible));
                            cluster.susceptible -= Math.Min((toChange * sum * percentageEachCommutingCluster ), 10000 * percentageNative);
                            cluster.vaccineAccepters -= Math.Min((toChange * sum * percentageEachCommutingCluster * percentageClusterAccepters)
                                                                    , 10000 * percentageNative);
                            cluster.susceptible -= Math.Min((toChange * sum * percentageEachCommutingCluster * percentageClusterHesitators)
                                                                    , 10000 * percentageNative);
                            cluster.vaccinated += Math.Min((toChange * sum * percentageEachCommutingCluster ), 10000 * percentageNative);
                        }
                        
                        province.deniedVaccinations = availableVaccinations - toChange;
                    }
                    else
                    {
                        double toChange = province.TotalSusceptible(outgoingCommuters) * (1 - averageVaccineHesitancy);
                        
                        province.nativeWorkers.susceptible -= toChange * percentageNative;
                        province.nativeWorkers.vaccinated += toChange * percentageNative;                     
                        foreach (PopulationCluster cluster in outgoingCommuters)
                        {
                            double sum = (cluster.susceptible / outgoingCommuters.Sum(x => x.susceptible));
                            cluster.susceptible -= Math.Min((toChange * percentageEachCommutingCluster * sum), 10000 * percentageEachCommutingCluster);
                            cluster.vaccinated += Math.Min((toChange * percentageEachCommutingCluster * sum), 10000 * percentageEachCommutingCluster);
                        }

                        province.deniedVaccinations = province.TotalSusceptible(outgoingCommuters) * averageVaccineHesitancy;
                    }
                }

                double currentInfected = ModelInfected();
                highestInfections = currentInfected > highestInfections ? currentInfected : highestInfections;

                double currentRecovered = ModelRecovered();
                highestRecovered = currentRecovered > highestRecovered ? currentRecovered : highestRecovered;

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

                cluster.vaccineAccepters = Math.Max(0, cluster.vaccineAccepters + dS * (cluster.vaccineAccepters / cluster.susceptible));
                cluster.vaccineRefusers = Math.Max(0, cluster.vaccineRefusers + dS * ( cluster.vaccineRefusers / cluster.susceptible));
                cluster.vaccineHesitators = Math.Max(0, cluster.vaccineHesitators + dS * ( cluster.vaccineHesitators / cluster.susceptible));

                cluster.susceptible = Math.Max(0, cluster.susceptible + dS);
                cluster.infected = Math.Max(0, Math.Min(cluster.total, cluster.infected + dI));
                cluster.recovered = Math.Min(cluster.total, cluster.recovered + dR);
            }

            //Also for native population
            double dS2 = 0.5 * -0.43 * prov.TotalInfected(present) * (native.susceptible / prov.TotalPresent(present));
            double dR2 = 0.5 * 0.2 * native.infected;
            double dI2 = -dS2 - dR2;

            native.vaccineAccepters = Math.Max(0, native.vaccineAccepters + dS2 * (native.vaccineAccepters / native.susceptible));
            native.vaccineHesitators = Math.Max(0, native.vaccineHesitators + dS2 * (native.vaccineHesitators / native.susceptible));
            native.vaccineAccepters = Math.Max(0, native.vaccineAccepters + dS2 * (native.vaccineRefusers / native.susceptible));

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
                "Highest Infected: " + highestInfections.ToString() + ",\n" +
                "Highest Recovered: " + highestRecovered.ToString() + ",\n" +
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

        public void Reset()
        {
            generation = 0;
            provinces = ProvinceInit.CreateProvinces(); ;
            pastStatuses = new List<StatusPoint>();
            highestInfections = 0;
        }
    }
}
