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
        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox5.Visible = false;
            numberOfYears = 1;
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox3.Visible = false;
            groupBox5.Visible = false;
            numberOfYears = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            groupBox5.Visible = false;
            numberOfYears = 3;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            groupBox5.Visible = true;
            numberOfYears = 4;
        }
        
        //Zum nächsten Tab springen
        public void nextTabPage()
        {
            formInit.tabPageChange(1);
        }

        //Initialisieren des Kalenderobjekts
        public void GetCalendarWithDates()
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

        //Füllen der Listview mit Tagen, Ferien und Feiertagen
        public void FillFormWithDataGridView(List<MCalendarDay> calendarDayList)
        {
            //Je nach Anzahl der ausgewählten Jahrgänge werden Spalten angelegt
            if (CountCheckedCheckboxes() == 0)
            {
                MessageBox.Show("Bitte einen Jahrgang auswählen");
            }
            else
            {
                CEditPlan cEditPlan = new CEditPlan();
                dGV = cEditPlan.createDataGridViews(CountCheckedCheckboxes());
                dGV = cEditPlan.fillGrids(dGV, calendarDayList);
                tagplanBearbeitenUC.AddDGV(dGV);
                nextTabPage();
            }
        }

        private void buttonWeiter_Click(object sender, EventArgs e)
        {
            if (vacationCurrentYearUrl != null && vacationNextYearUrl != null && holidayCurrentYearUrl != null && holidayNextYearUrl != null)
            {
                formInit.EnableBearbeitenStatistikTabPage();

                //Werte aus Datepicker werden an Kalenderobjekt übergeben
                GetCalendarWithDates();

                //DataGridview wird erstellt, befüllt und übergeben
                FillFormWithDataGridView(calendarWithDays.CalendarList);
            }
        }

        //Laden der Ferien und Feiertage
        private void button6_Click(object sender, EventArgs e)
        {
            //Modaler Dialog zum Laden der Dateien wird aufgerufen
            using (ferienFeiertageAuswaehlenForm = new Tagplaner.View.FerienFeiertageAuswaehlenForm(dateTimePickerVon.Value, dateTimePickerBis.Value))
            {
                DialogResult dr = ferienFeiertageAuswaehlenForm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    holidayCurrentYearUrl = ferienFeiertageAuswaehlenForm.TextForBox1;
                    holidayNextYearUrl = ferienFeiertageAuswaehlenForm.TextForBox2;
                    vacationCurrentYearUrl = ferienFeiertageAuswaehlenForm.TextForBox3;
                    vacationNextYearUrl = ferienFeiertageAuswaehlenForm.TextForBox4;

                    this.label4.Text = "Feriendatei (Von): " + splitUrl(holidayCurrentYearUrl) + "\n" +
                                       "Feriendatei (Bis): " + splitUrl(holidayNextYearUrl) + "\n" +
                                       "Feiertagdatei (Von): " + splitUrl(vacationCurrentYearUrl) + "\n" +
                                       "Feiertagdatei (Bis): " + splitUrl(vacationNextYearUrl) + " geöffnet";
                    this.label4.Visible = true;
                    this.buttonWeiter.Enabled = true;
                }
            }
        }

        //Methode zum Überprüfen ob eine Checkbox ausgewählt ist. Fügt der Liste typeOfClasses Werte hinzu
        public void CeckCheckboxes(CheckBox checkBox)
        {
            if (checkBox.Checked)
            {
                typeOfClasses.Add(checkBox.Text);
            }
            else
                typeOfClasses.Add("");
        }

        //Zählt die ausgewählten Checkboxen
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

        //Splitet eine Url. Nur der Dateiname wird zurückgeliefert 
        public String splitUrl(String url)
        {
            String[] substrings = url.Split('\\');
            return substrings[substrings.Length - 1];
        }

        //Überprüfen ob End- vor Anfangsdatum liegt
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