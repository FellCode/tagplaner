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
    public partial class SplashScreenForm : Form
    {
        private Timer timer;

        public SplashScreenForm()
        {
            InitializeComponent();
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

        }

        private void SplashScreenForm_Load(object sender, EventArgs e)
        {
            this.Width = pictureBox1.Width;
            this.Height = pictureBox1.Height;
        }
    }
}
