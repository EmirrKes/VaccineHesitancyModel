using OxyPlot;
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

        internal void drawModel(Simulation simulation)
        {
            // Holds your chart data
            var model = new PlotModel { Title = "My Calculated Values" };

            // Create lines
            var susceptibleLine = new LineSeries
            {
                Title = "Values",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(255, 165, 0)
            };
            var infectionsLine = new LineSeries
            {
                Title = "Values",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(123, 0, 0)
            };
            var recoveredLine = new LineSeries
            {
                Title = "Values",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(0, 0, 123)
            };
            var vaccinatedLine = new LineSeries
            {
                Title = "Values",
                MarkerType = MarkerType.Circle,
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

            plotView.Model = model;
        }
    }
}
