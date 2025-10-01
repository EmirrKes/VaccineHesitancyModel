using System.Runtime.InteropServices;
using System.Threading;

namespace VaccineHesitancyModel
{
    internal static class Program
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [STAThread]
        static void Main()
        {
            AllocConsole();

            Console.WriteLine("\"p\" followed by the number of generations to advance the simulation, for example \"p 100\".\n" +
                    "\"e\" to exit application\n");

            List<Province> provinces = new List<Province>
            {
                    // !!! INHABITANTS NEEDS TO BE UPDATED !!! // Joris ;)

                new Province(id: 0, name: "Limburg", inhabitants: 0),
                new Province(id: 1, name: "Zeeland", inhabitants: 0),
                new Province(id: 2, name: "North Brabant", inhabitants: 0),
                new Province(id: 3, name: "South Holland", inhabitants: 0),
                new Province(id: 4, name: "Utrecht", inhabitants: 0),
                new Province(id: 5, name: "Gelderland", inhabitants: 0),
                new Province(id: 6, name: "North Holland", inhabitants: 0),
                new Province(id: 7, name: "Flevoland", inhabitants: 0),
                new Province(id: 8, name: "Overijssel", inhabitants: 0),
                new Province(id: 9, name: "Friesland", inhabitants: 0),
                new Province(id: 10, name: "Drenthe", inhabitants: 0),
                new Province(id: 11, name: "Groningen", inhabitants: 0)
            };


            Simulation simulation = new Simulation(provinces);
            simulation.PrintStatus();

            Thread consoleThread = new Thread(() =>
            {
                while (true)
                {
                    Console.Write("command: ");
                    string input = Console.ReadLine();
                    string[] inputs = input.Split(' ');
                    switch (inputs[0])
                    {
                        case "e": Application.Exit();
                            break;
                        case "p":  if (inputs.Count() >= 2) simulation.Progress(Convert.ToInt32(inputs[1]));
                                    else Console.WriteLine("Missing generation number");
                            break;
                        default: Console.WriteLine("invalid command");
                            break;
                    }
                    
                }
            }
            );
            consoleThread.IsBackground = true;
            consoleThread.Start();

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}