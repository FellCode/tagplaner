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
    public partial class SplashScreenFormcs : Form
    {
        private Timer timer;

        public SplashScreenFormcs()
        {
            //InitializeComponent();
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

        private void SplashScreenFormcs_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); 
        }
    }
}
