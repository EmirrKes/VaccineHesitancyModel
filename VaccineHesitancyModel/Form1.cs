namespace VaccineHesitancyModel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var screen = Screen.PrimaryScreen.WorkingArea;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(screen.Width / 2, screen.Top + 300);
        }
    }
}
