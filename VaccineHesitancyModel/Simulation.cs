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
        public double highestDeltaI = 0;
        public double usedHesitation = 0;
        List<double> DeltaIList = new List<double>();

        public Simulation(List<Province> provinces)
        {
            this.provinces = provinces;
        }

        public void Progress(int generations, double VaccineHesitancy)
        {
            usedHesitation = VaccineHesitancy;
            double averageVaccineAvailability = 32877;  // 2million/365days
            double totalVaccinationHesitancyRate = VaccineHesitancy / 100; // Total percentage of people doubting/refusing vaccinations
            double acceptanceRate = 0.5;                // 50% of hesitant are refusers
            double hesitantRate = 1 - acceptanceRate;   // 50% of hesitant are actually hesitant

            double refusedVaccinationsPrev = 0;
            double refusedVaccinations = 0;

            //init subsets with Hesitant people
            if (highestInfections == 0) //First iteration
                InitProvHesitancy(totalVaccinationHesitancyRate, acceptanceRate, hesitantRate);
            
            //Run the 2 Sub-Steps
            for (int i = 0; i < generations; i++)
            {
                refusedVaccinationsPrev = refusedVaccinations;
                refusedVaccinations = 0;
                DeltaIList = new List<double>();

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

                double currentHighestDeltaI = DeltaIList.Sum() / DeltaIList.Count();
                highestDeltaI = currentHighestDeltaI > highestDeltaI ? currentHighestDeltaI : highestDeltaI;

                //Now run all vaccinations
                //This will be done each day, when people are at home
                foreach (Province province in provinces)
                {
                    double people = ModelInhabitants();
                    double totalPrc = province.totalInhabitants / people;
                    refusedVaccinations += Vaccinate(province, (averageVaccineAvailability) * totalPrc); //maybe use fraction of susceptible ?? All refused vaccinations added: + refusedVaccinationsPrev
                }

                //GRAPHS
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

                DeltaIList.Add(dI);

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

            DeltaIList.Add(dI2);

            native.vaccineRefusers = Math.Max(0, native.vaccineRefusers + dS2 * (native.vaccineRefusers / native.susceptible));
            native.vaccineHesitators = Math.Max(0, native.vaccineHesitators + dS2 * (native.vaccineHesitators / native.susceptible));

            native.susceptible = Math.Max(0, native.susceptible + dS2);
            native.infected = Math.Max(0, Math.Min(native.total, native.infected + dI2));
            native.recovered = Math.Min(native.total, native.recovered + dR2);
        } 

        public void InitProvHesitancy(double hesitancyRate, double refusalRate, double hesitantAcceptanceRate)
        {
            foreach (Province province in provinces)
            {
                //province.nativeWorkers.vaccineAccepters = province.nativeWorkers.total * (1 - VaccineHesitancy);
                province.nativeWorkers.vaccineHesitators = province.nativeWorkers.total * hesitancyRate * hesitantAcceptanceRate;
                province.nativeWorkers.vaccineRefusers = province.nativeWorkers.total * hesitancyRate * refusalRate;

                foreach (PopulationCluster cluster in province.outgoingCommuters.Select(e => e.Item1).ToList())
                {
                    //cluster.vaccineAccepters = cluster.total * (1 - VaccineHesitancy);
                    cluster.vaccineHesitators = cluster.total * hesitancyRate * hesitantAcceptanceRate;
                    cluster.vaccineRefusers = cluster.total * hesitancyRate * refusalRate;
                }
            }
        }

        public double Vaccinate(Province toVaccinate, double availableVaccinations)
        {
            /* This function vaccinates an entire province(city) with given vaccination(hesitancy) rates.
             * To properly update this, hesitant and refusal people need to be updated proportionally.
             */

            double refusedVaccinations = 0;
            Random rnd = new Random(); //A random change for hesitant people to accept a vaccination

            //================================\\
            //=== Update the NativeWorkers ===\\
            //================================\\

            double nativeVaccinations = availableVaccinations * (toVaccinate.nativeWorkers.total / toVaccinate.totalInhabitants);   //Maybe relative to susceptible??

            if (toVaccinate.nativeWorkers.susceptible > 0)
            {
                // Non-hesitant people. This needs to be calculated before hesitant count is changed
                double nonHesitant =
                    (toVaccinate.nativeWorkers.susceptible - toVaccinate.nativeWorkers.vaccineHesitators -
                     toVaccinate.nativeWorkers.vaccineRefusers) / toVaccinate.nativeWorkers.susceptible;

                //Certain Refusal
                refusedVaccinations += nativeVaccinations *
                                       (toVaccinate.nativeWorkers.vaccineRefusers /
                                        toVaccinate.nativeWorkers.susceptible); //native refusal percentage

                //Hesitant people
                double hesitantVaccinations = nativeVaccinations *
                                              (toVaccinate.nativeWorkers.vaccineHesitators /
                                               toVaccinate.nativeWorkers.susceptible);
                for (int i = 0; i < (int)hesitantVaccinations; i++)
                {
                    int rndNum = rnd.Next(1, 11); //Dice roll
                    if (rndNum >= 10 && toVaccinate.nativeWorkers.vaccineHesitators > 0 && toVaccinate.nativeWorkers.susceptible > 0)
                    {
                        toVaccinate.nativeWorkers.vaccineHesitators--;
                        toVaccinate.nativeWorkers.vaccinated++;
                        toVaccinate.nativeWorkers.susceptible--;
                    }
                    else
                        refusedVaccinations++;
                }

                //Certain Acceptance(Non-Hesitant group)
                if ((toVaccinate.nativeWorkers.susceptible - toVaccinate.nativeWorkers.vaccineHesitators - toVaccinate.nativeWorkers.vaccineRefusers) - nonHesitant * nativeVaccinations < 0)
                {
                    toVaccinate.nativeWorkers.vaccinated += (toVaccinate.nativeWorkers.susceptible - toVaccinate.nativeWorkers.vaccineHesitators - toVaccinate.nativeWorkers.vaccineRefusers);
                    toVaccinate.nativeWorkers.susceptible = Math.Max(0, toVaccinate.nativeWorkers.susceptible - (toVaccinate.nativeWorkers.susceptible - toVaccinate.nativeWorkers.vaccineHesitators - toVaccinate.nativeWorkers.vaccineRefusers));

                }
                else
                {
                    toVaccinate.nativeWorkers.vaccinated += nonHesitant * nativeVaccinations;
                    toVaccinate.nativeWorkers.susceptible = Math.Max(0, toVaccinate.nativeWorkers.susceptible - nonHesitant * nativeVaccinations);
                }

            }
            //else
            //    refusedVaccinations += nativeVaccinations;



            //===============================\\
            //=== Update commuting people ===\\
            //===============================\\

            foreach (PopulationCluster cluster in toVaccinate.outgoingCommuters.Select(e => e.Item1).ToList())
            {
                double clusterVaccinations = availableVaccinations * (cluster.total / toVaccinate.totalInhabitants);                    // Relative to susceptible ??? 
                if (cluster.susceptible > 0)
                {
                    // Non-hesitant people. This needs to be calculated before hesitant count is changed
                    double nonHesitantCluster = (cluster.susceptible - cluster.vaccineHesitators - cluster.vaccineRefusers) / cluster.susceptible;

                    //Certain Refusal
                    refusedVaccinations += clusterVaccinations * (cluster.vaccineRefusers / cluster.susceptible);                         //cluster refusal percentage

                    //Hesitant people
                    double hesitantVaccinationsCluster = clusterVaccinations * (cluster.vaccineHesitators / cluster.susceptible);
                    for (int i = 0; i < (int)hesitantVaccinationsCluster; i++)
                    {
                            int rndNum = rnd.Next(1, 11); //Dice roll
                            if (rndNum >= 10 && cluster.vaccineHesitators > 0 && cluster.susceptible > 0)
                            {
                                cluster.vaccineHesitators--;
                                cluster.vaccinated++;
                                cluster.susceptible--;
                            }
                            else
                                refusedVaccinations++;
                    }

                    //Certain Acceptance (Non-Hesitant group)
                    if ((cluster.susceptible - cluster.vaccineHesitators - cluster.vaccineRefusers) - nonHesitantCluster * clusterVaccinations < 0)
                    {
                        cluster.vaccinated += (cluster.susceptible - cluster.vaccineHesitators - cluster.vaccineRefusers);
                        cluster.susceptible = Math.Max(0, cluster.susceptible - (cluster.susceptible - cluster.vaccineHesitators - cluster.vaccineRefusers));
                    }
                    else
                    {
                        cluster.vaccinated += nonHesitantCluster * clusterVaccinations;
                        cluster.susceptible = Math.Max(0, cluster.susceptible - nonHesitantCluster * clusterVaccinations);
                    }
                }
                //else
                    //refusedVaccinations += clusterVaccinations;
            }
            return refusedVaccinations; //return the unused vaccinations
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
            highestRecovered = 0;
            highestDeltaI = 0;
            usedHesitation = 0;
            highestInfections = 0;
        }
    }
}
