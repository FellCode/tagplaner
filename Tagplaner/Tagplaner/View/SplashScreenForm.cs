using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tagplaner
{
    /// <summary>
    /// SplashScreen Form wird beim Start der Applikation angezeigt um Daten vorab zuladen
    /// </summary>
    public partial class SplashScreenForm : Form
    {
        private Timer timer;

        /// <summary>
        /// Erstellt eine Instanz von SplashScreenForm
        /// </summary>
        public SplashScreenForm()
        {
            InitializeComponent();

            CDatabase.GetInstance().CheckDBForBug();
        }

        private void SplashScreenFormcs_Shown(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 3000;
            timer.Start();
            timer.Tick += timer_tick;
        }

        private void timer_tick(object sender, EventArgs e)
        {
            timer.Stop();
            FormInit formInit = new FormInit();
            formInit.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Stop();
            FormInit formInit = new FormInit();
            formInit.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            timer.Stop();
            FormInit formInit = new FormInit();
            formInit.Show();
            this.Hide();
        }

        private void SplashScreenForm_Load(object sender, EventArgs e)
        {
            this.Width = pictureBox1.Width;
            this.Height = pictureBox1.Height;

            InitLabelNames();
        }

        /// <summary>
        /// Konfiguriert das Label für die Namen der ersteller der Tagplan Applikation
        /// </summary>
        private void InitLabelNames()
        {
            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;
            label1.Text = "Arnold Bechtold, Thomas Bender, Stefan Geißler,\n"
                + "Christopher Holler, Matthias Ohm, Isabella Pfeuster,\n"
                + "Felix Smuda, Alexander Theis, Maximilian Thill,\n"
                + "Daniel Valero Moreno, Niklas Wazal";
            label1.AutoSize = true;
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;  
        }
    }
}
