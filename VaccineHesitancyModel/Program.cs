using System.Runtime.InteropServices;
using System.Threading;
using OxyPlot;
using OxyPlot.WindowsForms;

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

            ApplicationConfiguration.Initialize();
            Form1 form = new Form1();

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
                        case "p":  if  (inputs.Count() >= 3)
                            {
                                //simulation.Progress(Convert.ToInt32(inputs[1]), Convert.ToDouble(inputs[2]), VaccineSuccess: 10);
                                //form.drawBaseModel(simulation);
                                //form.drawInfectedModel(simulation);
                                //form.drawDeltaI(simulation);
                                form.drawInfectionsGrowth(simulation);
                                //form.drawVaccineAcceptancy(simulation);
                            }                   
                            else Console.WriteLine("Missing generation number");
                            break;
                        case "r": simulation.Reset();
                            break;
                        default: Console.WriteLine("invalid command");
                            break;
                    }
                    
                }
            }
            );
            consoleThread.IsBackground = true;
            consoleThread.Start();

            Application.Run(form);
        }
    }
}