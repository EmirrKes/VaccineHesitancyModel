using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Security.Cryptography.X509Certificates;

namespace VaccineHesitancyModel
{
    public partial class Form1 : Form
    {
        PlotView plotView;
        internal Form1()
        {

            InitializeComponent();

            var screen = Screen.PrimaryScreen.WorkingArea;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(screen.Width / 2, screen.Top + 300);

            plotView = new PlotView
            {
                Dock = DockStyle.Fill,
            };
            this.Controls.Add(plotView);
  
        }

        internal void drawBaseModel(Simulation simulation)
        {
            // Holds your chart data
            var model = new PlotModel { Title = "Baseline model. Hesitancy: " + simulation.usedHesitation };

            // Create lines
            var susceptibleLine = new LineSeries
            {
                Title = "Susceptible",
                //MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(255, 165, 0)
            };
            var infectionsLine = new LineSeries
            {
                Title = "Infected",
                //MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(123, 0, 0)
            };
            var recoveredLine = new LineSeries
            {
                Title = "Recovered",
                //MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(0, 0, 123)
            };
            var vaccinatedLine = new LineSeries
            {
                Title = "Vaccinated",
                //MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(0, 123, 0)
            };

            List<StatusPoint> values = simulation.pastStatuses;

            // Add values to the lines
            for (int i = 0; i < values.Count; i++)
            {
                susceptibleLine.Points.Add(new DataPoint(values[i].generation, values[i].susceptible));
                infectionsLine.Points.Add(new DataPoint(values[i].generation, values[i].infected));
                recoveredLine.Points.Add(new DataPoint(values[i].generation, values[i].recovered));
                vaccinatedLine.Points.Add(new DataPoint(values[i].generation, values[i].vaccinated));
            }

            model.Series.Add(susceptibleLine);
            model.Series.Add(infectionsLine);
            model.Series.Add(recoveredLine);
            model.Series.Add(vaccinatedLine);

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Generation" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Individuals" });

            model.Legends.Add(new Legend()
            {
                LegendTitle = "Legend",
                LegendPosition = LegendPosition.TopRight,
            });
            plotView.Model = model;
        }

        internal void drawInfectedModel(Simulation simulation)
        {
            simulation.Reset();

            List<List<StatusPoint>> statusPoints = new List<List<StatusPoint>>();

            int generations = 100;
            double hesitancyIncrement = 10;
            for (int i = 0; i < 5; i++)
            {
                simulation.Progress(generations, hesitancyIncrement * (i + 1));
                statusPoints.Add(simulation.pastStatuses);
                simulation.Reset();
            }

            var model = new PlotModel { Title = "Incrementing hesitancies" };
            List<LineSeries> lines = new List<LineSeries>();

            int j = 0;
            foreach (List<StatusPoint> SP in statusPoints)
            {
                var line = new LineSeries
                {
                    Title = "Hesitancy: " + hesitancyIncrement * (j + 1)
                };

                foreach (StatusPoint sp in SP)
                    line.Points.Add(new DataPoint(sp.generation, sp.infected));

                model.Series.Add(line);

                j++;
            }

            plotView.Model = model;
        }

        internal void drawDeltaI(Simulation simulation)
        {
            simulation.Reset();

            int generations = 100;
            double hesitancyIncrement = 1;

            List<double> highestDeltaIs = new List<double>();

            for (int i = 0; i < 100 / hesitancyIncrement; i++)
            {
                simulation.Progress(generations, hesitancyIncrement * (i + 1));
                highestDeltaIs.Add(simulation.highestDeltaI);
                simulation.Reset();
            }

            var model = new PlotModel { Title = "Infection rate based on hesitancy" };

            var line = new LineSeries
            {
                Title = "Infection rating"
            };

            for (int i = 0; i < highestDeltaIs.Count(); i++)
                line.Points.Add(new DataPoint(hesitancyIncrement * (i + 1), highestDeltaIs[i]));

            model.Series.Add(line);
            plotView.Model = model;
        }
    }
}
