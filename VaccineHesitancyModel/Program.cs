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

            List<Province> provinces = ProvinceInit.CreateProvinces();

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