using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineHesitancyModel
{
    static class ProvinceInit
    {

        public static List<Province> CreateProvinces()
        {
            //Totaal inwoner aantal bestaat enkel uit werkende mensen
            Province Limburg = new Province(id: 0, name: "Limburg", new PopulationCluster(434400,0,0,0));
            Province Zeeland = new Province(id: 1, name: "Zeeland", new PopulationCluster(144700, 0, 0, 0));
            Province NorthBrabant = new Province(id: 2, name: "NorthBrabant", new PopulationCluster(1102700, 0, 0, 0));
            Province SouthHolland = new Province(id: 3, name: "SouthHolland", new PopulationCluster(1511600, 0, 0, 0));
            Province Utrecht = new Province(id: 4, name: "Utrecht", new PopulationCluster(486300, 0, 0, 0));
            Province Gelderland = new Province(id: 5, name: "Gelderland", new PopulationCluster(779500, 0, 0, 0));
            Province NorthHolland = new Province(id: 6, name: "NorthHolland", new PopulationCluster(1210400, 0, 0, 0));
            Province Flevoland = new Province(id: 7, name: "Flevoland", new PopulationCluster(112400, 0, 0, 0));
            Province Overijssel = new Province(id: 8, name: "Overijssel", new PopulationCluster(475300, 0, 0, 0));
            Province Friesland = new Province(id: 9, name: "Friesland", new PopulationCluster(247500, 0, 0, 0));
            Province Drenthe = new Province(id: 10, name: "Drenthe", new PopulationCluster(149700, 0, 0, 0));
            Province Groningen = new Province(id: 11, name: "Groningen", new PopulationCluster(217200, 0, 0, 0));

            // Groningen
            Groningen.AddCommuters(new PopulationCluster(11600,0,0,0), Friesland);
            Groningen.AddCommuters(new PopulationCluster(25600, 0, 0, 0), Drenthe);
            Groningen.AddCommuters(new PopulationCluster(3800, 0, 0, 0), Overijssel);
            Groningen.AddCommuters(new PopulationCluster(1000, 0, 0, 0), Flevoland);
            Groningen.AddCommuters(new PopulationCluster(2200, 0, 0, 0), Gelderland);
            Groningen.AddCommuters(new PopulationCluster(3100, 0, 0, 0), Utrecht);
            Groningen.AddCommuters(new PopulationCluster(5100, 0, 0, 0), NorthHolland);
            Groningen.AddCommuters(new PopulationCluster(2600, 0, 0, 0), SouthHolland);
            Groningen.AddCommuters(new PopulationCluster(100, 0, 0, 0), Zeeland);
            Groningen.AddCommuters(new PopulationCluster(1300, 0, 0, 0), NorthBrabant);
            Groningen.AddCommuters(new PopulationCluster(200, 0, 0, 0), Limburg);


            // Friesland
            Friesland.AddCommuters(new PopulationCluster(13300, 0, 0, 0), Groningen);
            Friesland.AddCommuters(new PopulationCluster(8000, 0, 0, 0), Drenthe);
            Friesland.AddCommuters(new PopulationCluster(5800, 0, 0, 0), Overijssel);
            Friesland.AddCommuters(new PopulationCluster(4400, 0, 0, 0), Flevoland);
            Friesland.AddCommuters(new PopulationCluster(2800, 0, 0, 0), Gelderland);
            Friesland.AddCommuters(new PopulationCluster(3400, 0, 0, 0), Utrecht);
            Friesland.AddCommuters(new PopulationCluster(9900, 0, 0, 0), NorthHolland);
            Friesland.AddCommuters(new PopulationCluster(3600, 0, 0, 0), SouthHolland);
            Friesland.AddCommuters(new PopulationCluster(100, 0, 0, 0), Zeeland);
            Friesland.AddCommuters(new PopulationCluster(2000, 0, 0, 0), NorthBrabant);
            Friesland.AddCommuters(new PopulationCluster(200, 0, 0, 0), Limburg);

            // Drenthe
            Drenthe.AddCommuters(new PopulationCluster(34000, 0, 0, 0), Groningen);
            Drenthe.AddCommuters(new PopulationCluster(6300, 0, 0, 0), Friesland);
            Drenthe.AddCommuters(new PopulationCluster(21100, 0, 0, 0), Overijssel);
            Drenthe.AddCommuters(new PopulationCluster(1400, 0, 0, 0), Flevoland);
            Drenthe.AddCommuters(new PopulationCluster(3700, 0, 0, 0), Gelderland);
            Drenthe.AddCommuters(new PopulationCluster(3000, 0, 0, 0), Utrecht);
            Drenthe.AddCommuters(new PopulationCluster(3500, 0, 0, 0), NorthHolland);
            Drenthe.AddCommuters(new PopulationCluster(2600, 0, 0, 0), SouthHolland);
            Drenthe.AddCommuters(new PopulationCluster(0, 0, 0, 0), Zeeland);
            Drenthe.AddCommuters(new PopulationCluster(1700, 0, 0, 0), NorthBrabant);
            Drenthe.AddCommuters(new PopulationCluster(200, 0, 0, 0), Limburg);

            // Overijssel
            Overijssel.AddCommuters(new PopulationCluster(2300, 0, 0, 0), Groningen);
            Overijssel.AddCommuters(new PopulationCluster(3900, 0, 0, 0), Friesland);
            Overijssel.AddCommuters(new PopulationCluster(14500, 0, 0, 0), Drenthe);
            Overijssel.AddCommuters(new PopulationCluster(8500, 0, 0, 0), Flevoland);
            Overijssel.AddCommuters(new PopulationCluster(47000, 0, 0, 0), Gelderland);
            Overijssel.AddCommuters(new PopulationCluster(10700, 0, 0, 0), Utrecht);
            Overijssel.AddCommuters(new PopulationCluster(8300, 0, 0, 0), NorthHolland);
            Overijssel.AddCommuters(new PopulationCluster(5400, 0, 0, 0), SouthHolland);
            Overijssel.AddCommuters(new PopulationCluster(100, 0, 0, 0), Zeeland);
            Overijssel.AddCommuters(new PopulationCluster(4700, 0, 0, 0), NorthBrabant);
            Overijssel.AddCommuters(new PopulationCluster(800, 0, 0, 0), Limburg);

            // Flevoland
            Flevoland.AddCommuters(new PopulationCluster(400, 0, 0, 0), Groningen);
            Flevoland.AddCommuters(new PopulationCluster(1900, 0, 0, 0), Friesland);
            Flevoland.AddCommuters(new PopulationCluster(800, 0, 0, 0), Drenthe);
            Flevoland.AddCommuters(new PopulationCluster(7600, 0, 0, 0), Overijssel);
            Flevoland.AddCommuters(new PopulationCluster(11400, 0, 0, 0), Gelderland);
            Flevoland.AddCommuters(new PopulationCluster(15200, 0, 0, 0), Utrecht);
            Flevoland.AddCommuters(new PopulationCluster(57000, 0, 0, 0), NorthHolland);
            Flevoland.AddCommuters(new PopulationCluster(4900, 0, 0, 0), SouthHolland);
            Flevoland.AddCommuters(new PopulationCluster(100, 0, 0, 0), Zeeland);
            Flevoland.AddCommuters(new PopulationCluster(2200, 0, 0, 0), NorthBrabant);
            Flevoland.AddCommuters(new PopulationCluster(300, 0, 0, 0), Limburg);

            // Gelderland
            Gelderland.AddCommuters(new PopulationCluster(1100, 0, 0, 0), Groningen);
            Gelderland.AddCommuters(new PopulationCluster(1500, 0, 0, 0), Friesland);
            Gelderland.AddCommuters(new PopulationCluster(2500, 0, 0, 0), Drenthe);
            Gelderland.AddCommuters(new PopulationCluster(45100, 0, 0, 0), Overijssel);
            Gelderland.AddCommuters(new PopulationCluster(8400, 0, 0, 0), Flevoland);
            Gelderland.AddCommuters(new PopulationCluster(88800, 0, 0, 0), Utrecht);
            Gelderland.AddCommuters(new PopulationCluster(24600, 0, 0, 0), NorthHolland);
            Gelderland.AddCommuters(new PopulationCluster(20000, 0, 0, 0), SouthHolland);
            Gelderland.AddCommuters(new PopulationCluster(400, 0, 0, 0), Zeeland);
            Gelderland.AddCommuters(new PopulationCluster(43000, 0, 0, 0), NorthBrabant);
            Gelderland.AddCommuters(new PopulationCluster(6600, 0, 0, 0), Limburg);

            // Utrecht
            Utrecht.AddCommuters(new PopulationCluster(900, 0, 0, 0), Groningen);
            Utrecht.AddCommuters(new PopulationCluster(700, 0, 0, 0), Friesland);
            Utrecht.AddCommuters(new PopulationCluster(900, 0, 0, 0), Drenthe);
            Utrecht.AddCommuters(new PopulationCluster(3700, 0, 0, 0), Overijssel);
            Utrecht.AddCommuters(new PopulationCluster(5600, 0, 0, 0), Flevoland);
            Utrecht.AddCommuters(new PopulationCluster(44300, 0, 0, 0), Gelderland);
            Utrecht.AddCommuters(new PopulationCluster(84700, 0, 0, 0), NorthHolland);
            Utrecht.AddCommuters(new PopulationCluster(38600, 0, 0, 0), SouthHolland);
            Utrecht.AddCommuters(new PopulationCluster(300, 0, 0, 0), Zeeland);
            Utrecht.AddCommuters(new PopulationCluster(13300, 0, 0, 0), NorthBrabant);
            Utrecht.AddCommuters(new PopulationCluster(1500, 0, 0, 0), Limburg);

            // North Holland
            NorthHolland.AddCommuters(new PopulationCluster(1700, 0, 0, 0), Groningen);
            NorthHolland.AddCommuters(new PopulationCluster(2200, 0, 0, 0), Friesland);
            NorthHolland.AddCommuters(new PopulationCluster(1200, 0, 0, 0), Drenthe);
            NorthHolland.AddCommuters(new PopulationCluster(3900, 0, 0, 0), Overijssel);
            NorthHolland.AddCommuters(new PopulationCluster(14500, 0, 0, 0), Flevoland);
            NorthHolland.AddCommuters(new PopulationCluster(10500, 0, 0, 0), Gelderland);
            NorthHolland.AddCommuters(new PopulationCluster(62800, 0, 0, 0), Utrecht);
            NorthHolland.AddCommuters(new PopulationCluster(58000, 0, 0, 0), SouthHolland);
            NorthHolland.AddCommuters(new PopulationCluster(400, 0, 0, 0), Zeeland);
            NorthHolland.AddCommuters(new PopulationCluster(12200, 0, 0, 0), NorthBrabant);
            NorthHolland.AddCommuters(new PopulationCluster(1600, 0, 0, 0), Limburg);

            // South Holland
            SouthHolland.AddCommuters(new PopulationCluster(1000, 0, 0, 0), Groningen);
            SouthHolland.AddCommuters(new PopulationCluster(1100, 0, 0, 0), Friesland);
            SouthHolland.AddCommuters(new PopulationCluster(1000, 0, 0, 0), Drenthe);
            SouthHolland.AddCommuters(new PopulationCluster(3000, 0, 0, 0), Overijssel);
            SouthHolland.AddCommuters(new PopulationCluster(2300, 0, 0, 0), Flevoland);
            SouthHolland.AddCommuters(new PopulationCluster(12700, 0, 0, 0), Gelderland);
            SouthHolland.AddCommuters(new PopulationCluster(61400, 0, 0, 0), Utrecht);
            SouthHolland.AddCommuters(new PopulationCluster(117900, 0, 0, 0), NorthHolland);
            SouthHolland.AddCommuters(new PopulationCluster(3600, 0, 0, 0), Zeeland);
            SouthHolland.AddCommuters(new PopulationCluster(40100, 0, 0, 0), NorthBrabant);
            SouthHolland.AddCommuters(new PopulationCluster(2500, 0, 0, 0), Limburg);

            // Zeeland
            Zeeland.AddCommuters(new PopulationCluster(100, 0, 0, 0), Groningen);
            Zeeland.AddCommuters(new PopulationCluster(100, 0, 0, 0), Friesland);
            Zeeland.AddCommuters(new PopulationCluster(100, 0, 0, 0), Drenthe);
            Zeeland.AddCommuters(new PopulationCluster(200, 0, 0, 0), Overijssel);
            Zeeland.AddCommuters(new PopulationCluster(100, 0, 0, 0), Flevoland);
            Zeeland.AddCommuters(new PopulationCluster(800, 0, 0, 0), Gelderland);
            Zeeland.AddCommuters(new PopulationCluster(1200, 0, 0, 0), Utrecht);
            Zeeland.AddCommuters(new PopulationCluster(1900, 0, 0, 0), NorthHolland);
            Zeeland.AddCommuters(new PopulationCluster(11300, 0, 0, 0), SouthHolland);
            Zeeland.AddCommuters(new PopulationCluster(12500, 0, 0, 0), NorthBrabant);
            Zeeland.AddCommuters(new PopulationCluster(200, 0, 0, 0), Limburg);

            // North Brabant
            NorthBrabant.AddCommuters(new PopulationCluster(500, 0, 0, 0), Groningen);
            NorthBrabant.AddCommuters(new PopulationCluster(700, 0, 0, 0), Friesland);
            NorthBrabant.AddCommuters(new PopulationCluster(700, 0, 0, 0), Drenthe);
            NorthBrabant.AddCommuters(new PopulationCluster(3200, 0, 0, 0), Overijssel);
            NorthBrabant.AddCommuters(new PopulationCluster(1300, 0, 0, 0), Flevoland);
            NorthBrabant.AddCommuters(new PopulationCluster(36200, 0, 0, 0), Gelderland);
            NorthBrabant.AddCommuters(new PopulationCluster(26400, 0, 0, 0), Utrecht);
            NorthBrabant.AddCommuters(new PopulationCluster(20800, 0, 0, 0), NorthHolland);
            NorthBrabant.AddCommuters(new PopulationCluster(54900, 0, 0, 0), SouthHolland);
            NorthBrabant.AddCommuters(new PopulationCluster(5700, 0, 0, 0), Zeeland);
            NorthBrabant.AddCommuters(new PopulationCluster(24100, 0, 0, 0), Limburg);

            // Limburg
            Limburg.AddCommuters(new PopulationCluster(300, 0, 0, 0), Groningen);
            Limburg.AddCommuters(new PopulationCluster(200, 0, 0, 0), Friesland);
            Limburg.AddCommuters(new PopulationCluster(200, 0, 0, 0), Drenthe);
            Limburg.AddCommuters(new PopulationCluster(1300, 0, 0, 0), Overijssel);
            Limburg.AddCommuters(new PopulationCluster(400, 0, 0, 0), Flevoland);
            Limburg.AddCommuters(new PopulationCluster(10200, 0, 0, 0), Gelderland);
            Limburg.AddCommuters(new PopulationCluster(4500, 0, 0, 0), Utrecht);
            Limburg.AddCommuters(new PopulationCluster(5900, 0, 0, 0), NorthHolland);
            Limburg.AddCommuters(new PopulationCluster(5400, 0, 0, 0), SouthHolland);
            Limburg.AddCommuters(new PopulationCluster(200, 0, 0, 0), Zeeland);
            Limburg.AddCommuters(new PopulationCluster(43000, 0, 0, 0), NorthBrabant);

            NorthHolland.nativeWorkers.infected = 100;
            NorthHolland.nativeWorkers.susceptible -= 100;

            return new List<Province> { Groningen, Friesland, Drenthe, Overijssel, Gelderland, Utrecht, NorthHolland, SouthHolland, Zeeland, NorthBrabant, Limburg, Flevoland };
        }
    }
}
