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

namespace Tagplaner
{
    public partial class TagplanAnlegenUserControl : UserControl
    {
        Tagplaner.View.FerienFeiertageAuswaehlenForm ferienFeiertageAuswaehlenForm;
        private DataGridView dGV;

        private FormInit formInit;

        MCalendar calendarWithDays;

        int numberOfYears;
        List<String> typeOfClasses;

        TagplanBearbeitenUserControl tagplanBearbeitenUC;

        CSerialize serializer;
        CDatabase databaseController;

        String vacationCurrentYearUrl;
        String vacationNextYearUrl;
        String holidayCurrentYearUrl;
        String holidayNextYearUrl;

        /// <summary>
        /// Konstruktor für TagplanAnlegenUserControl
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
            databaseController.FillFederalStateComboBox(comboBoxBundesland);

            dGV = new DataGridView();
        }

        /// <summary>
        /// Registriert Änderungen des RadioButtons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox5.Visible = false;
            numberOfYears = 1;
        }
        
        /// <summary>
        /// Registriert Änderungen des RadioButtons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            groupBox3.Visible = false;
            groupBox5.Visible = false;
            numberOfYears = 2;
        }

        /// <summary>
        /// Registriert Änderungen des RadioButtons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            groupBox3.Visible = true;
            groupBox5.Visible = false;
            numberOfYears = 3;
        }

        /// <summary>
        /// Registriert Änderungen des RadioButtons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            groupBox3.Visible = true;
            groupBox5.Visible = true;
            numberOfYears = 4;
        }

        /// <summary>
        /// Veranlasst den Sprung auf die nächste TabPage
        /// </summary>
        public void nextTabPage()
        {
            formInit.tabPageChange(1);
        }

        /// <summary>
        /// Initialisieren des Kalenderobjekts
        /// </summary>
        public void CreateCalendarWithDates()
        {
            //Ausbildungsgänge erster Jahrgang checken
            CeckCheckboxes(checkBoxErsterJahrgangAE);
            CeckCheckboxes(checkBoxErsterJahrgangSI);

            //Ausbildungsgänge zweiter Jahrgang checken
            CeckCheckboxes(checkBoxZweiterJahrgangAE);
            CeckCheckboxes(checkBoxZweiterJahrgangSI);

            //Ausbildungsgänge dritter Jahrgang checken
            CeckCheckboxes(checkBoxZweiterJahrgangAE);
            CeckCheckboxes(checkBoxZweiterJahrgangSI);

            //Ausbildungsgänge dritter Jahrgang checken
            CeckCheckboxes(checkBoxZweiterJahrgangAE);
            CeckCheckboxes(checkBoxZweiterJahrgangSI);

            MCalendar.getInstance().FillCalendarInitial(this.dateTimePickerVon.Value, this.dateTimePickerBis.Value, numberOfYears, typeOfClasses, vacationCurrentYearUrl, vacationNextYearUrl, holidayCurrentYearUrl, holidayNextYearUrl);
            calendarWithDays = MCalendar.getInstance();
        }

        /// <summary>
        /// Füllen der GridView
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
                nextTabPage();
            }
        }

        /// <summary>
        /// Erstellt ein Kalenderobjekt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWeiter_Click(object sender, EventArgs e)
        {
            if (vacationCurrentYearUrl != null && vacationNextYearUrl != null && holidayCurrentYearUrl != null && holidayNextYearUrl != null)
            {
                formInit.EnableBearbeitenStatistikTabPage();

                //Werte aus Datepicker werden an Kalenderobjekt übergeben
                CreateCalendarWithDates();

                //DataGridview wird erstellt, befüllt und übergeben
                FillFormWithDataGridView(calendarWithDays.CalendarList);
            }
        }

        //Laden der Ferien und Feiertage
        /// <summary>
        /// Laden der Ferien und Feiertage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            //Modaler Dialog zum Laden der Dateien wird aufgerufen
            using (ferienFeiertageAuswaehlenForm = new Tagplaner.View.FerienFeiertageAuswaehlenForm(dateTimePickerVon.Value, dateTimePickerBis.Value))
            {
                DialogResult dr = ferienFeiertageAuswaehlenForm.ShowDialog();
                if (dr == DialogResult.OK)
               {
                    holidayCurrentYearUrl = AppDomain.CurrentDomain.BaseDirectory + "Feiertage\\Nordrhein-Westfalen2015.csv";
                    holidayNextYearUrl = AppDomain.CurrentDomain.BaseDirectory + "Feiertage\\Nordrhein-Westfalen2016.csv";
                    vacationCurrentYearUrl = AppDomain.CurrentDomain.BaseDirectory + "Ferien\\Ferien_Hessen_2015.ics";
                    vacationNextYearUrl = AppDomain.CurrentDomain.BaseDirectory + "Ferien\\Ferien_Hessen_2016.ics";

                    this.label4.Text = "Feriendatei (Von): " + splitUrl(holidayCurrentYearUrl) + "\n" +
                                       "Feriendatei (Bis): " + splitUrl(holidayNextYearUrl) + "\n" +
                                       "Feiertagdatei (Von): " + splitUrl(vacationCurrentYearUrl) + "\n" +
                                       "Feiertagdatei (Bis): " + splitUrl(vacationNextYearUrl) + " geöffnet";
                    this.label4.Visible = true;
                    this.buttonWeiter.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Methode zum Überprüfen ob eine Checkbox ausgewählt ist. Fügt der Liste typeOfClasses Werte hinzu
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
            if (checkBoxErsterJahrgangAE.Checked) checkedBoxesCount++;
            if (checkBoxErsterJahrgangSI.Checked) checkedBoxesCount++;
            if (checkBoxZweiterJahrgangAE.Checked) checkedBoxesCount++;
            if (checkBoxZweiterJahrgangSI.Checked) checkedBoxesCount++;
            if (checkBoxDritterJahrgangAE.Checked) checkedBoxesCount++;
            if (checkBoxDritterJahrgangSI.Checked) checkedBoxesCount++;
            if (checkBoxVierterJahrgangAE.Checked) checkedBoxesCount++;
            if (checkBoxVierterJahrgangSI.Checked) checkedBoxesCount++;

            return checkedBoxesCount;
        }

        // 
        /// <summary>
        /// Splittet eine Url. Nur der Dateiname wird zurückgeliefert
        /// </summary>
        /// <param name="url">Pfad zur Datei</param>
        /// <returns>Dateiname</returns>
        public String splitUrl(String url)
        {
            String[] substrings = url.Split('\\');
            return substrings[substrings.Length - 1];
        }

        /// <summary>
        /// Überprüfen ob End- vor Anfangsdatum liegt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePickerBis_ValueChanged(object sender, EventArgs e)
        {
            if (this.dateTimePickerBis.Value < this.dateTimePickerVon.Value)
            {
                MessageBox.Show("Das Enddatum kann nicht vor dem Anfangsdatum liegen");
                this.dateTimePickerBis.Value = this.dateTimePickerVon.Value;
            }
        }
    }
}