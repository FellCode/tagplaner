using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tagplaner.View
{
    public partial class FerienFeiertageAuswaehlenForm : Form
    {
        Boolean file1Choosen;
        Boolean file2Choosen;
        Boolean file3Choosen;
        Boolean file4Choosen;

        String textFromTextBox1;
        String textFromTextBox2;
        String textFromTextBox3;
        String textFromTextBox4;

        DateTime startDate;
        DateTime endDate;

        CICalCSVConverter csvComverter;

        public String TextForBox1
        {
            get { return textFromTextBox1; }
        }
        public String TextForBox2
        {
            get { return textFromTextBox2; }
        }
        public String TextForBox3
        {
            get { return textFromTextBox3; }
        }
        public String TextForBox4
        {
            get { return textFromTextBox4; }
        }
        public FerienFeiertageAuswaehlenForm(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            this.startDate = startDate;
            this.endDate = endDate;
            csvComverter = new CICalCSVConverter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            this.openFileDialog1.AutoUpgradeEnabled = false;

            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Feiertage");

            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Feiertage";
            openFileDialog1.Title = "Feiertagedatei aktuelles Jahr öffnen";
            openFileDialog1.Filter = "Feiertage|*.csv";

            DialogResult fileChoiceResult = openFileDialog1.ShowDialog();

            if (fileChoiceResult == DialogResult.OK)
            {
                this.textBox1.Text = SplitUrl(openFileDialog1.FileName);
                this.textFromTextBox1 = openFileDialog1.FileName;
                file1Choosen = true;
                CheckAllFilesChoosen();
                if (!csvComverter.CheckCsvFile(startDate, endDate, openFileDialog1.FileName))
                {
                    this.toolStripStatusLabel1.Text = "Keine Feiertage in '" + SplitUrl(openFileDialog1.FileName) + "' für den Zeitraum " + startDate.ToShortDateString() + " - " + endDate.ToShortDateString() + " enthalten";
                }
                else this.toolStripStatusLabel1.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            this.openFileDialog2.AutoUpgradeEnabled = false;

            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Feiertage");

            openFileDialog2.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Feiertage";
            openFileDialog2.Title = "Feiertagdatei nächstes Jahr öffnen";
            openFileDialog2.Filter = "Feiertage|*.csv";

            DialogResult fileChoiceResult = openFileDialog2.ShowDialog();

            if (fileChoiceResult == DialogResult.OK)
            {
                this.textBox2.Text = SplitUrl(openFileDialog2.FileName);
                this.textFromTextBox2 = openFileDialog2.FileName;
                file2Choosen = true;
                CheckAllFilesChoosen();
                if (!csvComverter.CheckCsvFile(startDate, endDate, openFileDialog2.FileName))
                {
                    this.toolStripStatusLabel1.Text = "Keine Feiertage in '" + SplitUrl(openFileDialog2.FileName) + "' für den Zeitraum " + startDate.ToShortDateString() + " - " + endDate.ToShortDateString() + " enthalten";
                }
                else this.toolStripStatusLabel1.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog3 = new OpenFileDialog();
            this.openFileDialog3.AutoUpgradeEnabled = false;

            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Ferien");

            openFileDialog3.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Ferien";
            openFileDialog3.Title = "Feriendatei aktuelles Jahr öffnen";
            openFileDialog3.Filter = "Ferien|*.ics";

            DialogResult fileChoiceResult = openFileDialog3.ShowDialog();

            if (fileChoiceResult == DialogResult.OK)
            {
                this.textFromTextBox3 = openFileDialog3.FileName;
                this.textBox3.Text = SplitUrl(openFileDialog3.FileName);
                file3Choosen = true;
                CheckAllFilesChoosen();

                if (!csvComverter.CheckICSFile(new DateTime(startDate.Year, startDate.Month, startDate.Day), new DateTime(startDate.Year, 12, 31), openFileDialog3.FileName))
                {
                    this.toolStripStatusLabel1.Text = "Keine Ferien in '" + SplitUrl(openFileDialog3.FileName) + "' für den Zeitraum " + startDate.ToShortDateString() + " - " + endDate.ToShortDateString() + " enthalten";
                }
                else this.toolStripStatusLabel1.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog4 = new OpenFileDialog();
            this.openFileDialog4.AutoUpgradeEnabled = false;

            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Ferien");

            openFileDialog4.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Ferien";
            openFileDialog4.Title = "Feriendatei nächstes Jahr öffnen";
            openFileDialog4.Filter = "Ferien|*.ics";

            DialogResult fileChoiceResult = openFileDialog4.ShowDialog();

            if (fileChoiceResult == DialogResult.OK)
            {
                this.textFromTextBox4 = openFileDialog4.FileName;
                this.textBox4.Text = SplitUrl(openFileDialog4.FileName);
                file4Choosen = true;
                CheckAllFilesChoosen();

                if (!csvComverter.CheckICSFile(new DateTime(endDate.Year, 1, 1), new DateTime(endDate.Year, endDate.Month, endDate.Day), openFileDialog4.FileName))
                {
                    this.toolStripStatusLabel1.Text = "Keine Ferien in '" + SplitUrl(openFileDialog4.FileName) + "' für den Zeitraum " + startDate.ToShortDateString() + " - " + endDate.ToShortDateString() + " enthalten";
                }
                else this.toolStripStatusLabel1.Text = "";
            }
        }

        //OK-Button enablen wenn alles nötigen Dateien ausgewählt wurden
        public void CheckAllFilesChoosen()
        {
            if (file1Choosen == true && file2Choosen == true && file3Choosen == true && file4Choosen == true)
            {
                this.button5.Enabled = true;
            }
        }

        //OK-Result bei Button-Click setzen
        private void button5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        //Fenster schließen bei Button-Click
        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        public String SplitUrl(String url)
        {
            String[] substrings = url.Split('\\');
            return substrings[substrings.Length - 1];
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
