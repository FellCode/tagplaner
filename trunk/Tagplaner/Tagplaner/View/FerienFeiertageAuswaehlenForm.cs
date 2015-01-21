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
    /// Form zum Auswählen von Dateien deren Inhalt Daten zu Ferien (.ics) und Feiertagen (.csv) enthält
    /// </summary>
    public partial class FerienFeiertageAuswaehlenForm : Form
    {
        private Boolean file1Choosen;
        private Boolean file2Choosen;
        private Boolean file3Choosen;
        private Boolean file4Choosen;

        private String textFromTextBox1;
        private String textFromTextBox2;
        private String textFromTextBox3;
        private String textFromTextBox4;

        private DateTime startDate;
        private DateTime endDate;

        private CICalCSVConverter csvComverter;

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

        /// <summary>
        /// Erzeugt ein Objekt vom Typ FerienFeiertageAuswaehlenForm
        /// </summary>
        /// <param name="startDate">Gewähltes Startdatum</param>
        /// <param name="endDate">Gewähltes Enddatum</param>
        /// <param name="vacationCurrentYearUrl">Url zu einer Feriendatei</param>
        /// <param name="vacationNextYearUrl">Url zu einer Feriendatei</param>
        /// <param name="holidayCurrentYearUrl">Url zu einer Feiertagedatei</param>
        /// <param name="holidayNextYearUrl">Url zu einer Feiertagedatei</param>
        public FerienFeiertageAuswaehlenForm(DateTime startDate, DateTime endDate, String vacationCurrentYearUrl, String vacationNextYearUrl, String holidayCurrentYearUrl, String holidayNextYearUrl)
        {
            InitializeComponent();
            this.startDate = startDate;
            this.endDate = endDate;
            csvComverter = new CICalCSVConverter();

            if (!String.IsNullOrEmpty(holidayCurrentYearUrl))
            {
                this.textFromTextBox1 = holidayCurrentYearUrl;
                file1Choosen = true;
                textBox_HolidayCurrentYear.Text = SplitUrl(holidayCurrentYearUrl);
                CheckAllFilesChoosen();
            }
            if (!String.IsNullOrEmpty(holidayNextYearUrl))
            {
                this.textFromTextBox2 = holidayNextYearUrl;
                file2Choosen = true;
                textBox_HolidayNextYear.Text = SplitUrl(holidayNextYearUrl);
                CheckAllFilesChoosen();
            }
            if (!String.IsNullOrEmpty(vacationCurrentYearUrl))
            {
                this.textFromTextBox3 = vacationCurrentYearUrl;
                file3Choosen = true;
                textBox_VacationCurrentYear.Text = SplitUrl(vacationCurrentYearUrl);
                CheckAllFilesChoosen();
            }
            if (!String.IsNullOrEmpty(vacationNextYearUrl))
            {
                this.textFromTextBox4 = vacationNextYearUrl;
                file4Choosen = true;
                textBox_VacationNextYear.Text = SplitUrl(vacationNextYearUrl);
                CheckAllFilesChoosen();
            }
        }

        /// <summary>
        /// Öffnet den Dialog zum Auswählen der Feiertagedatei für das aktuelle Jahr 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_HolidayCurrentYear_Click(object sender, EventArgs e)
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
                this.textBox_HolidayCurrentYear.Text = SplitUrl(openFileDialog1.FileName);
                this.textFromTextBox1 = openFileDialog1.FileName;
                file1Choosen = true;
                CheckAllFilesChoosen();

                //Überprüfung ob in der Datei im ausgewählten Zeitraum Feiertage vorhanden sind
                if (!csvComverter.CheckCsvFile(startDate, endDate, openFileDialog1.FileName))
                {
                    this.toolStripStatusLabel1.Text = "Keine Feiertage in '" + SplitUrl(openFileDialog1.FileName) + "' für den Zeitraum " + startDate.ToShortDateString() + " - " + endDate.ToShortDateString() + " enthalten";
                }
                else this.toolStripStatusLabel1.Text = "";
            }
        }

        /// <summary>
        /// Öffnet den Dialog zum Auswählen der Feiertagedatei für das nächste Jahr 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_HolidayNextYear_Click(object sender, EventArgs e)
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
                this.textBox_HolidayNextYear.Text = SplitUrl(openFileDialog2.FileName);
                this.textFromTextBox2 = openFileDialog2.FileName;
                file2Choosen = true;
                CheckAllFilesChoosen();

                //Überprüfung ob in der Datei im ausgewählten Zeitraum Feiertage vorhanden sind
                if (!csvComverter.CheckCsvFile(startDate, endDate, openFileDialog2.FileName))
                {
                    this.toolStripStatusLabel1.Text = "Keine Feiertage in '" + SplitUrl(openFileDialog2.FileName) + "' für den Zeitraum " + startDate.ToShortDateString() + " - " + endDate.ToShortDateString() + " enthalten";
                }
                else this.toolStripStatusLabel1.Text = "";
            }
        }

        /// <summary>
        /// Öffnet den Dialog zum Auswählen der Feriendatei für das aktuelle Jahr  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_VacationCurrentYear_Click(object sender, EventArgs e)
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
                this.textBox_VacationCurrentYear.Text = SplitUrl(openFileDialog3.FileName);
                file3Choosen = true;
                CheckAllFilesChoosen();

                //Überprüfung ob in der Datei im ausgewählten Zeitraum Ferien vorhanden sind
                if (!csvComverter.CheckICSFile(new DateTime(startDate.Year, startDate.Month, startDate.Day), new DateTime(startDate.Year, 12, 31), openFileDialog3.FileName))
                {
                    this.toolStripStatusLabel1.Text = "Keine Ferien in '" + SplitUrl(openFileDialog3.FileName) + "' für den Zeitraum " + startDate.ToShortDateString() + " - " + endDate.ToShortDateString() + " enthalten";
                }
                else this.toolStripStatusLabel1.Text = "";
            }
        }

        /// <summary>
        /// Öffnet den Dialog zum Auswählen der Feriendatei für das nächste Jahr  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_VacationNextYear_Click(object sender, EventArgs e)
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
                this.textBox_VacationNextYear.Text = SplitUrl(openFileDialog4.FileName);
                file4Choosen = true;
                CheckAllFilesChoosen();

                //Überprüfung ob in der Datei im ausgewählten Zeitraum Ferien vorhanden sind
                if (!csvComverter.CheckICSFile(new DateTime(endDate.Year, 1, 1), new DateTime(endDate.Year, endDate.Month, endDate.Day), openFileDialog4.FileName))
                {
                    this.toolStripStatusLabel1.Text = "Keine Ferien in '" + SplitUrl(openFileDialog4.FileName) + "' für den Zeitraum " + startDate.ToShortDateString() + " - " + endDate.ToShortDateString() + " enthalten";
                }
                else this.toolStripStatusLabel1.Text = "";
            }
        }

        /// <summary>
        /// Überprüft ob alle Nötigen Dateien ausgewählt wurden. OK-Button wird enabled falls dies der Falls sein sollte
        /// </summary>
        public void CheckAllFilesChoosen()
        {
            if (file1Choosen == true && file2Choosen == true && file3Choosen == true && file4Choosen == true)
            {
                this.button_OK.Enabled = true;
            }
        }

        /// <summary>
        /// Setzt DialogResult auf OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        //Fenster schließen bei Button-Click
        private void button_Abbrechen_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Splittet eine Url. Nur der Dateiname wird zurückgeliefert
        /// </summary>
        /// <param name="url">Pfad zur Datei</param>
        /// <returns>Dateiname</returns>
        public String SplitUrl(String url)
        {
            String[] substrings = url.Split('\\');
            return substrings[substrings.Length - 1];
        }
    }
}
