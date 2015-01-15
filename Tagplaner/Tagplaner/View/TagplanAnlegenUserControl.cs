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
        private DataGridView dGV = new DataGridView();

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

            //DatenbankController wird später auf Startfenster verschoben
            databaseController = CDatabase.GetInstance();
          //  databaseController.CreateDB();
          //  databaseController.FillDB();
            databaseController.FillAllList();
            databaseController.FillFederalStateComboBox(comboBoxBundesland);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TagplanAnlegenUserControl_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            numberOfYears = 1;
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            numberOfYears = 2;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        //Zum nächsten Tab springen
        public void nextTabPage()
        {
            formInit.tabPageChange(1);
        }

        //Initialisieren des Kalenderobjekts
        public void GetCalendarWithDates()
        {
            CeckCheckboxes();
            MCalendar.getInstance().FillCalendarInitial(this.dateTimePickerVon.Value, this.dateTimePickerBis.Value, numberOfYears, typeOfClasses, vacationCurrentYearUrl, vacationNextYearUrl, holidayCurrentYearUrl, holidayNextYearUrl);
            calendarWithDays = MCalendar.getInstance();
        }

        //Füllen der Listview mit Tagen, Ferien und Feiertagen
        public void FillFormWithDataGridView(List<MCalendarDay> calendarDayList)
        {
            
            int x = 0;
            if (checkBoxErsterJahrgangAE.Checked == true)
                x += 1;
            if (checkBoxErsterJahrgangSI.Checked == true)
                x += 1;
            if (checkBoxZweiterJahrgangAE.Checked == true)
                x += 1;
            if (checkBoxZweiterJahrgangSI.Checked == true)
                x += 1;
            if (x == 0)
            {
                MessageBox.Show("Bitte einen Jahrgang auswählen");
            }
            else
            {
                CEditPlan cEditPlan = new CEditPlan();
                dGV = cEditPlan.createDataGridViews(x);
                dGV = cEditPlan.fillGrids(dGV, calendarDayList);
                tagplanBearbeitenUC.AddDGV(dGV);
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void buttonWeiter_Click(object sender, EventArgs e)
        {
            if (vacationCurrentYearUrl != null && vacationNextYearUrl != null && holidayCurrentYearUrl != null && holidayNextYearUrl != null)
            {
               

                //Werte aus Datepicker werden an Kalenderobjekt übergeben
                GetCalendarWithDates();

                //DataGridview wird erstellt, befüllt und übergeben
                FillFormWithDataGridView(calendarWithDays.CalendarList);

                //TagplanBearbeitenTab wird angezeigt
                nextTabPage();
            }
        }

        //Laden der Ferien und Feiertage
        private void button6_Click(object sender, EventArgs e)
        {

            holidayCurrentYearUrl = @"C:\Users\" + Environment.UserName + @"\Documents\Visual Studio 2013\Projects\Tagplaner\Tagplaner\Tagplaner\bin\Debug\Feiertage\Nordrhein-Westfalen2015.csv";
            holidayNextYearUrl = @"C:\Users\"+ Environment.UserName +@"\Documents\Visual Studio 2013\Projects\Tagplaner\Tagplaner\Tagplaner\bin\Debug\Feiertage\Nordrhein-Westfalen2016.csv";
            vacationCurrentYearUrl = @"C:\Users\" + Environment.UserName + @"\Documents\Visual Studio 2013\Projects\Tagplaner\Tagplaner\Tagplaner\bin\Debug\Ferien\Ferien_Hessen_2015.ics";
            vacationNextYearUrl = @"C:\Users\" + Environment.UserName + @"\Documents\Visual Studio 2013\Projects\Tagplaner\Tagplaner\Tagplaner\bin\Debug\Ferien\Ferien_Hessen_2016.ics";

            this.label4.Text = "Feriendatei (Von): " + splitUrl(holidayCurrentYearUrl) + "\n" +
                               "Feriendatei (Bis): " + splitUrl(holidayNextYearUrl) + "\n" +
                               "Feiertagdatei (Von): " + splitUrl(vacationCurrentYearUrl) + "\n" +
                               "Feiertagdatei (Bis): " + splitUrl(vacationNextYearUrl) + " geöffnet";
            this.label4.Visible = true;
            this.buttonWeiter.Enabled = true;
            

            /*using (ferienFeiertageAuswaehlenForm = new Tagplaner.View.FerienFeiertageAuswaehlenForm(dateTimePickerVon.Value, dateTimePickerBis.Value))
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
            }*/

        }

        private void btn_feiertageOeffnen_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        public void CeckCheckboxes()
        {
            if (checkBoxErsterJahrgangAE.Checked)
            {
                typeOfClasses.Add("AE");
            }
            else
                typeOfClasses.Add("");

            if (checkBoxErsterJahrgangSI.Checked)
            {
                typeOfClasses.Add("SI");
            }
            else
                typeOfClasses.Add("");

            if (checkBoxZweiterJahrgangAE.Checked)
            {
                typeOfClasses.Add("AE");
            }
            else
                typeOfClasses.Add("");

            if (checkBoxZweiterJahrgangSI.Checked)
            {
                typeOfClasses.Add("SI");
            }
            else
                typeOfClasses.Add("");
        }

        public String splitUrl(String url)
        {
            String[] substrings = url.Split('\\');
            return substrings[substrings.Length - 1];
        }

        private void dateTimePickerVon_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}