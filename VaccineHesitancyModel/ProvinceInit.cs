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
            //niet echt inwoners aantal, omdat van een deel niet bekend is waar ze werken
            Province Limburg = new Province(id: 11, name: "Limburg", inhabitants: 506000);
            Province Zeeland = new Province(id: 11, name: "Zeeland", inhabitants: 173200);
            Province NorthBrabant = new Province(id: 11, name: "NorthBrabant", inhabitants: 1277200);
            Province SouthHolland = new Province(id: 11, name: "SouthHolland", inhabitants: 1758200);
            Province Utrecht = new Province(id: 11, name: "Utrecht", inhabitants: 680800);
            Province Gelderland = new Province(id: 11, name: "Gelderland", inhabitants: 1021500);
            Province NorthHolland = new Province(id: 11, name: "NorthHolland", inhabitants: 1379400);
            Province Flevoland = new Province(id: 11, name: "Flevoland", inhabitants: 214200);
            Province Overijssel = new Province(id: 11, name: "Overijssel", inhabitants: 581500);
            Province Friesland = new Province(id: 11, name: "Friesland", inhabitants: 301000);
            Province Drenthe = new Province(id: 11, name: "Drenthe", inhabitants: 227200);
            Province Groningen = new Province(id: 11, name: "Groningen", inhabitants: 273800);

            Groningen.AddNeighbour(Friesland, 11600);
            Groningen.AddNeighbour(Drenthe, 25600);
            Groningen.AddNeighbour(Overijssel, 3800);
            Groningen.AddNeighbour(Flevoland, 1000);
            Groningen.AddNeighbour(Gelderland, 2200);
            Groningen.AddNeighbour(Utrecht, 3100);
            Groningen.AddNeighbour(NorthHolland, 5100);
            Groningen.AddNeighbour(SouthHolland, 2600);
            Groningen.AddNeighbour(Zeeland, 100);
            Groningen.AddNeighbour(NorthBrabant, 1300);
            Groningen.AddNeighbour(Limburg, 200);

            Friesland.AddNeighbour(Groningen, 13300);
            Friesland.AddNeighbour(Drenthe, 8000);
            Friesland.AddNeighbour(Overijssel, 5800);
            Friesland.AddNeighbour(Flevoland, 4400);
            Friesland.AddNeighbour(Gelderland, 2800);
            Friesland.AddNeighbour(Utrecht, 3400);
            Friesland.AddNeighbour(NorthHolland, 9900);
            Friesland.AddNeighbour(SouthHolland, 3600);
            Friesland.AddNeighbour(Zeeland, 100);
            Friesland.AddNeighbour(NorthBrabant, 2000);
            Friesland.AddNeighbour(Limburg, 200);

            Drenthe.AddNeighbour(Groningen, 34000);
            Drenthe.AddNeighbour(Friesland, 6300);
            Drenthe.AddNeighbour(Overijssel, 21100);
            Drenthe.AddNeighbour(Flevoland, 1400);
            Drenthe.AddNeighbour(Gelderland, 3700);
            Drenthe.AddNeighbour(Utrecht, 3000);
            Drenthe.AddNeighbour(NorthHolland, 3500);
            Drenthe.AddNeighbour(SouthHolland, 2600);
            Drenthe.AddNeighbour(Zeeland, 0);
            Drenthe.AddNeighbour(NorthBrabant, 1700);
            Drenthe.AddNeighbour(Limburg, 200);

            Overijssel.AddNeighbour(Groningen, 2300);
            Overijssel.AddNeighbour(Friesland, 3900);
            Overijssel.AddNeighbour(Drenthe, 14500);
            Overijssel.AddNeighbour(Flevoland, 8500);
            Overijssel.AddNeighbour(Gelderland, 47000);
            Overijssel.AddNeighbour(Utrecht, 10700);
            Overijssel.AddNeighbour(NorthHolland, 8300);
            Overijssel.AddNeighbour(SouthHolland, 5400);
            Overijssel.AddNeighbour(Zeeland, 100);
            Overijssel.AddNeighbour(NorthBrabant, 4700);
            Overijssel.AddNeighbour(Limburg, 800);

            Flevoland.AddNeighbour(Groningen, 400);
            Flevoland.AddNeighbour(Friesland, 1900);
            Flevoland.AddNeighbour(Drenthe, 800);
            Flevoland.AddNeighbour(Overijssel, 7600);
            Flevoland.AddNeighbour(Gelderland, 11400);
            Flevoland.AddNeighbour(Utrecht, 15200);
            Flevoland.AddNeighbour(NorthHolland, 57000);
            Flevoland.AddNeighbour(SouthHolland, 4900);
            Flevoland.AddNeighbour(Zeeland, 100);
            Flevoland.AddNeighbour(NorthBrabant, 2200);
            Flevoland.AddNeighbour(Limburg, 300);

            Gelderland.AddNeighbour(Groningen, 1100);
            Gelderland.AddNeighbour(Friesland, 1500);
            Gelderland.AddNeighbour(Drenthe, 2500);
            Gelderland.AddNeighbour(Overijssel, 45100);
            Gelderland.AddNeighbour(Flevoland, 8400);
            Gelderland.AddNeighbour(Utrecht, 88800);
            Gelderland.AddNeighbour(NorthHolland, 24600);
            Gelderland.AddNeighbour(SouthHolland, 20000);
            Gelderland.AddNeighbour(Zeeland, 400);
            Gelderland.AddNeighbour(NorthBrabant, 43000);
            Gelderland.AddNeighbour(Limburg, 6600);

            Utrecht.AddNeighbour(Groningen, 900);
            Utrecht.AddNeighbour(Friesland, 700);
            Utrecht.AddNeighbour(Drenthe, 900);
            Utrecht.AddNeighbour(Overijssel, 3700);
            Utrecht.AddNeighbour(Flevoland, 5600);
            Utrecht.AddNeighbour(Gelderland, 44300);
            Utrecht.AddNeighbour(NorthHolland, 84700);
            Utrecht.AddNeighbour(SouthHolland, 38600);
            Utrecht.AddNeighbour(Zeeland, 300);
            Utrecht.AddNeighbour(NorthBrabant, 13300);
            Utrecht.AddNeighbour(Limburg, 1500);

            NorthHolland.AddNeighbour(Groningen, 1700);
            NorthHolland.AddNeighbour(Friesland, 2200);
            NorthHolland.AddNeighbour(Drenthe, 1200);
            NorthHolland.AddNeighbour(Overijssel, 3900);
            NorthHolland.AddNeighbour(Flevoland, 14500);
            NorthHolland.AddNeighbour(Gelderland, 10500);
            NorthHolland.AddNeighbour(Utrecht, 62800);
            NorthHolland.AddNeighbour(SouthHolland, 58000);
            NorthHolland.AddNeighbour(Zeeland, 400);
            NorthHolland.AddNeighbour(NorthBrabant, 12200);
            NorthHolland.AddNeighbour(Limburg, 1600);

            SouthHolland.AddNeighbour(Groningen, 1000);
            SouthHolland.AddNeighbour(Friesland, 1100);
            SouthHolland.AddNeighbour(Drenthe, 1000);
            SouthHolland.AddNeighbour(Overijssel, 3000);
            SouthHolland.AddNeighbour(Flevoland, 2300);
            SouthHolland.AddNeighbour(Gelderland, 12700);
            SouthHolland.AddNeighbour(Utrecht, 61400);
            SouthHolland.AddNeighbour(NorthHolland, 117900);
            SouthHolland.AddNeighbour(Zeeland, 3600);
            SouthHolland.AddNeighbour(NorthBrabant, 40100);
            SouthHolland.AddNeighbour(Limburg, 2500);

            Zeeland.AddNeighbour(Groningen, 100);
            Zeeland.AddNeighbour(Friesland, 100);
            Zeeland.AddNeighbour(Drenthe, 100);
            Zeeland.AddNeighbour(Overijssel, 200);
            Zeeland.AddNeighbour(Flevoland, 100);
            Zeeland.AddNeighbour(Gelderland, 800);
            Zeeland.AddNeighbour(Utrecht, 1200);
            Zeeland.AddNeighbour(NorthHolland, 1900);
            Zeeland.AddNeighbour(SouthHolland, 11300);
            Zeeland.AddNeighbour(NorthBrabant, 12500);
            Zeeland.AddNeighbour(Limburg, 200);

            NorthBrabant.AddNeighbour(Groningen, 500);
            NorthBrabant.AddNeighbour(Friesland, 700);
            NorthBrabant.AddNeighbour(Drenthe, 700);
            NorthBrabant.AddNeighbour(Overijssel, 3200);
            NorthBrabant.AddNeighbour(Flevoland, 1300);
            NorthBrabant.AddNeighbour(Gelderland, 36200);
            NorthBrabant.AddNeighbour(Utrecht, 26400);
            NorthBrabant.AddNeighbour(NorthHolland, 20800);
            NorthBrabant.AddNeighbour(SouthHolland, 54900);
            NorthBrabant.AddNeighbour(Zeeland, 5700);
            NorthBrabant.AddNeighbour(Limburg, 24100);

            Limburg.AddNeighbour(Groningen, 300);
            Limburg.AddNeighbour(Friesland, 200);
            Limburg.AddNeighbour(Drenthe, 200);
            Limburg.AddNeighbour(Overijssel, 1300);
            Limburg.AddNeighbour(Flevoland, 400);
            Limburg.AddNeighbour(Gelderland, 10200);
            Limburg.AddNeighbour(Utrecht, 4500);
            Limburg.AddNeighbour(NorthHolland, 5900);
            Limburg.AddNeighbour(SouthHolland, 5400);
            Limburg.AddNeighbour(Zeeland, 200);
            Limburg.AddNeighbour(NorthBrabant, 43000);

            NorthHolland.infected = 10;

            return new List<Province> { Groningen, Friesland, Drenthe, Overijssel, Gelderland, Utrecht, NorthHolland, SouthHolland, Zeeland, NorthBrabant, Limburg };
        }
    }
}
