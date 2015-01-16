using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Tagplaner.View;

namespace Tagplaner
{
    /// <summary>
    /// UserControl zum erzeugen von Kalenderobjekten.
    /// Start-, Enddatum, Bundesland, Anzahl und Art der Jahrgänge können über die Oberfläche gewählt werden 
    /// </summary>
    public partial class TagplanAnlegenUserControl : UserControl
    {
        private FerienFeiertageAuswaehlenForm ferienFeiertageAuswaehlenForm;
        private DataGridView dGV;

        private FormInit formInit;

        private MCalendar calendarWithDays;

        private int numberOfYears;
        private List<String> typeOfClasses;

        private TagplanBearbeitenUserControl tagplanBearbeitenUC;

        private CSerialize serializer;
        private CDatabase databaseController;

        private String vacationCurrentYearUrl;
        private String vacationNextYearUrl;
        private String holidayCurrentYearUrl;
        private String holidayNextYearUrl;

        /// <summary>
        /// Erzeugt ein Objekt vom Typ TagplanAnlegenUserControl
        /// </summary>
        /// <param name="formInit">Form auf der das UserControl eingebunden werden soll</param>
        /// <param name="tagplanBearbeitenUC">UserControl das die GridView enthält</param>
        public TagplanAnlegenUserControl(FormInit formInit, TagplanBearbeitenUserControl tagplanBearbeitenUC)
        {
            this.formInit = formInit;
            this.tagplanBearbeitenUC = tagplanBearbeitenUC;
            serializer = new CSerialize();
            typeOfClasses = new List<String>();
            numberOfYears = 1;
            InitializeComponent();

            databaseController = CDatabase.GetInstance();
            databaseController.FillAllList();
            databaseController.FillFederalStateComboBox(comboBox_Bundesland);

            dGV = new DataGridView();
        }

        /// <summary>
        /// Registriert Änderungen des RadioButtons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_1Jahrgang_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_Jahrgang1.Visible = true;
            groupBox_Jahrgang2.Visible = false;
            groupBox_Jahrgang3.Visible = false;
            groupBox_Jahrgang4.Visible = false;

            checkBox_ZweiterJahrgangAE.Checked = false;
            checkBox_ZweiterJahrgangSI.Checked = false;

            checkBox_DritterJahrgangAE.Checked = false;
            checkBox_DritterJahrgangSI.Checked = false;

            checkBox_VierterJahrgangAE.Checked = false;
            checkBox_VierterJahrgangSI.Checked = false;

            numberOfYears = 1;
        }

        /// <summary>
        /// Registriert Änderungen des RadioButtons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_2Jahrgaenge_CheckedChanged_1(object sender, EventArgs e)
        {
            groupBox_Jahrgang1.Visible = true;
            groupBox_Jahrgang2.Visible = true;
            groupBox_Jahrgang3.Visible = false;
            groupBox_Jahrgang4.Visible = false;

            checkBox_DritterJahrgangAE.Checked = false;
            checkBox_DritterJahrgangSI.Checked = false;

            checkBox_VierterJahrgangAE.Checked = false;
            checkBox_VierterJahrgangSI.Checked = false;

            numberOfYears = 2;
        }

        /// <summary>
        /// Registriert Änderungen des RadioButtons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_3Jahrgaenge_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_Jahrgang1.Visible = true;
            groupBox_Jahrgang2.Visible = true;
            groupBox_Jahrgang3.Visible = true;
            groupBox_Jahrgang4.Visible = false;

            checkBox_VierterJahrgangAE.Checked = false;
            checkBox_VierterJahrgangSI.Checked = false;

            numberOfYears = 3;
        }

        /// <summary>
        /// Registriert Änderungen des RadioButtons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_4Jahrgaenge_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_Jahrgang1.Visible = true;
            groupBox_Jahrgang2.Visible = true;
            groupBox_Jahrgang3.Visible = true;
            groupBox_Jahrgang4.Visible = true;
            numberOfYears = 4;
        }

        /// <summary>
        /// Veranlasst den Sprung auf die nächste TabPage
        /// </summary>
        public void NextTabPage()
        {
            formInit.TabPageChange(1);
        }

        /// <summary>
        /// Initialisiert das Kalenderobjekts
        /// </summary>
        public void CreateCalendarWithDates()
        {
            //Ausbildungsgänge erster Jahrgang checken
            CeckCheckboxes(checkBox_ErsterJahrgangAE);
            CeckCheckboxes(checkBox_ErsterJahrgangSI);

            //Ausbildungsgänge zweiter Jahrgang checken
            CeckCheckboxes(checkBox_ZweiterJahrgangAE);
            CeckCheckboxes(checkBox_ZweiterJahrgangSI);

            //Ausbildungsgänge dritter Jahrgang checken
            CeckCheckboxes(checkBox_DritterJahrgangAE);
            CeckCheckboxes(checkBox_DritterJahrgangSI);

            //Ausbildungsgänge dritter Jahrgang checken
            CeckCheckboxes(checkBox_VierterJahrgangAE);
            CeckCheckboxes(checkBox_VierterJahrgangSI);

            MCalendar.getInstance().FillCalendarInitial(this.dateTimePicker_Von.Value, this.dateTimePicker_Bis.Value, numberOfYears, typeOfClasses, vacationCurrentYearUrl, vacationNextYearUrl, holidayCurrentYearUrl, holidayNextYearUrl);
            calendarWithDays = MCalendar.getInstance();
        }

        /// <summary>
        /// Füllt die GridView
        /// </summary>
        /// <param name="calendarDayList">Liste mit Kalendertagen in Zeitraum</param>
        public void FillFormWithDataGridView(List<MCalendarDay> calendarDayList)
        {
            //Je nach Anzahl der ausgewählten Jahrgänge werden Spalten angelegt
            if (CountCheckedCheckboxes() == 0)
            {
                MessageBox.Show("Bitte einen Jahrgang auswählen");
            }
            else
            {
                tagplanBearbeitenUC.CreateDataGridViews(CountCheckedCheckboxes());
                tagplanBearbeitenUC.FillGrids(calendarDayList);
                formInit.EnableBearbeitenStatistikTabPage();
                NextTabPage();
            }
        }

        /// <summary>
        /// Erstellt ein Kalenderobjekt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Weiter_Click(object sender, EventArgs e)
        {
            if (vacationCurrentYearUrl != null && vacationNextYearUrl != null && holidayCurrentYearUrl != null && holidayNextYearUrl != null)
            {
                //Werte aus Datepicker werden an Kalenderobjekt übergeben
                CreateCalendarWithDates();

                //DataGridview wird erstellt, befüllt und übergeben
                FillFormWithDataGridView(calendarWithDays.CalendarList);
            }
        }

        /// <summary>
        /// Öffnet den Dialog zum Laden der Ferien- und Feiertagedatei
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Oeffnen_Click(object sender, EventArgs e)
        {
            //Modaler Dialog zum Laden der Dateien wird aufgerufen
            using (ferienFeiertageAuswaehlenForm = new Tagplaner.View.FerienFeiertageAuswaehlenForm(dateTimePicker_Von.Value, dateTimePicker_Bis.Value))
            {
                DialogResult dr = ferienFeiertageAuswaehlenForm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    holidayCurrentYearUrl = AppDomain.CurrentDomain.BaseDirectory + "Feiertage\\Nordrhein-Westfalen2015.csv";
                    holidayNextYearUrl = AppDomain.CurrentDomain.BaseDirectory + "Feiertage\\Nordrhein-Westfalen2016.csv";
                    vacationCurrentYearUrl = AppDomain.CurrentDomain.BaseDirectory + "Ferien\\Ferien_Hessen_2015.ics";
                    vacationNextYearUrl = AppDomain.CurrentDomain.BaseDirectory + "Ferien\\Ferien_Hessen_2016.ics";

                    this.label_GeoeffneteDateienAnzeigen.Text = "Feriendatei (Von): " + SplitUrl(holidayCurrentYearUrl) + "\n" +
                                       "Feriendatei (Bis): " + SplitUrl(holidayNextYearUrl) + "\n" +
                                       "Feiertagdatei (Von): " + SplitUrl(vacationCurrentYearUrl) + "\n" +
                                       "Feiertagdatei (Bis): " + SplitUrl(vacationNextYearUrl) + " geöffnet";
                    this.label_GeoeffneteDateienAnzeigen.Visible = true;
                    this.button_Weiter.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Überprüft ob eine Checkbox ausgewählt ist. Fügt der Liste typeOfClasses Werte hinzu
        /// </summary>
        /// <param name="checkBox">Zu überprüfende Checkbox</param>
        public void CeckCheckboxes(CheckBox checkBox)
        {
            if (checkBox.Checked)
            {
                typeOfClasses.Add(checkBox.Text);
            }
            else
                typeOfClasses.Add("");
        }

        /// <summary>
        /// Zählt die ausgewählten Checkboxen
        /// </summary>
        /// <returns>Anzahl der ausgewählten Checkboxen</returns>
        public int CountCheckedCheckboxes()
        {
            int checkedBoxesCount = 0;
            if (checkBox_ErsterJahrgangAE.Checked) checkedBoxesCount++;
            if (checkBox_ErsterJahrgangSI.Checked) checkedBoxesCount++;
            if (checkBox_ZweiterJahrgangAE.Checked) checkedBoxesCount++;
            if (checkBox_ZweiterJahrgangSI.Checked) checkedBoxesCount++;
            if (checkBox_DritterJahrgangAE.Checked) checkedBoxesCount++;
            if (checkBox_DritterJahrgangSI.Checked) checkedBoxesCount++;
            if (checkBox_VierterJahrgangAE.Checked) checkedBoxesCount++;
            if (checkBox_VierterJahrgangSI.Checked) checkedBoxesCount++;

            return checkedBoxesCount;
        }

        // 
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

        /// <summary>
        /// Überprüfen ob End- vor Anfangsdatum liegt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker_Bis_ValueChanged(object sender, EventArgs e)
        {
            if (this.dateTimePicker_Bis.Value < this.dateTimePicker_Von.Value)
            {
                MessageBox.Show("Das Enddatum kann nicht vor dem Anfangsdatum liegen");
                this.dateTimePicker_Bis.Value = this.dateTimePicker_Von.Value;
            }
        }
    }
}