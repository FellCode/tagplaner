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
    public partial class FormInit : Form
    {
        public FormInit()
        {
            InitializeComponent();
        }

        private void Init_Load(object sender, EventArgs e)
        {
            // Initialize tabpages
            initTabPages();
        }

        private void initTabPages() {

            TagplanAnlegenUserControl tagplanAnlegenUserControl = new TagplanAnlegenUserControl();
            tagplanAnlegenUserControl.Dock = DockStyle.Fill;
            tagplanAnlegenUserControl.BackColor = Color.White;

            TabPage daySchedule = new TabPage();
            daySchedule.Text = "Tagplan erstellen";
            daySchedule.Controls.Add(tagplanAnlegenUserControl);
            tabControl1.TabPages.Add(daySchedule);
        }
    }
}
