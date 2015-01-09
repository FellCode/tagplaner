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
        TagplanAnlegenUserControl tagplanAnlegenUC;
        TagplanBearbeitenUserControl tagplanBearbeitenUC;

        public FormInit()
        {
            InitializeComponent();
            tagplanBearbeitenUC = new TagplanBearbeitenUserControl();
            tagplanAnlegenUC = new TagplanAnlegenUserControl(this, tagplanBearbeitenUC);
        }

        private void Init_Load(object sender, EventArgs e)
        {
            // Show date and time
            showDateTimeAsTitle();

            // Init tabpages
            addTabPage(tagplanAnlegenUC, "Tagplan anlegen");
            addTabPage(tagplanBearbeitenUC, "Tagplan bearbeiten");
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

        public void tabPageChange(int pageIndex)
        {
            tabControl1.SelectedIndex = pageIndex;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            showDateTimeAsTitle();
        }

        private void showDateTimeAsTitle() {
            string time = DateTime.Now.ToShortTimeString();
            string date = DateTime.Now.ToShortDateString();

            this.Text = date + " - " + time;
        }
    }
}
