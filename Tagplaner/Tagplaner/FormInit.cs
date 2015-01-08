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
            // Init tabpages
            addTabPage(new TagplanAnlegenUserControl(), "Tagplan anlegen");
        }

        /// <summary>
        /// Adds an userControl to tabControl1
        /// </summary>
        /// <param name="userControl"></param>
        /// <param name="pageName"></param>
        private void addTabPage(UserControl userControl, string pageName) {
            userControl.Dock = DockStyle.Fill;
            userControl.BackColor = Color.White;

            // Init tabPage
            TabPage tabPage = new TabPage();
            tabPage.Text = pageName;
            tabPage.Controls.Add(userControl);

            // Add tabpage to tabControl1
            tabControl1.TabPages.Add(tabPage);
        }
    }
}
