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
        private List<String> identifierOfYears;

        private TagplanBearbeitenUserControl tagplanBearbeitenUC;

        private CSerialize serializer;
        private CDatabase databaseController;

        private String vacationCurrentYearUrl;
        private String vacationNextYearUrl;
        private String holidayCurrentYearUrl;
        private String holidayNextYearUrl;

        List<TextBox> listOfTextBoxes;
        List<CheckBox> listOfCheckBoxes;

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
            identifierOfYears = new List<String>();
            numberOfYears = 1;
            InitializeComponent();

            CDatabase.GetInstance().FillFederalStateComboBox(comboBox_Bundesland);

            dGV = new DataGridView();

            vacationCurrentYearUrl = "";
            vacationNextYearUrl = "";
            holidayCurrentYearUrl = "";
            holidayNextYearUrl = "";

            listOfCheckBoxes = new List<CheckBox>();
            listOfTextBoxes = new List<TextBox>();

            FillCheckBoxTextBoxLists();
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

            textBox_ErsterJahrgangBezeichnung.Visible = true;
            textBox_ZweiterJahrgangBezeichnung.Visible = false;
            textBox_DritterJahrgangBezeichnung.Visible = false;
            textBox_VierterJahrgangBezeichnung.Visible = false;

            textBox_ZweiterJahrgangBezeichnung.Text = "";
            textBox_DritterJahrgangBezeichnung.Text = "";
            textBox_VierterJahrgangBezeichnung.Text = "";

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

            textBox_ErsterJahrgangBezeichnung.Visible = true;
            textBox_ZweiterJahrgangBezeichnung.Visible = true;
            textBox_DritterJahrgangBezeichnung.Visible = false;
            textBox_VierterJahrgangBezeichnung.Visible = false;

            textBox_DritterJahrgangBezeichnung.Text = "";
            textBox_VierterJahrgangBezeichnung.Text = "";

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

            textBox_ErsterJahrgangBezeichnung.Visible = true;
            textBox_ZweiterJahrgangBezeichnung.Visible = true;
            textBox_DritterJahrgangBezeichnung.Visible = true;
            textBox_VierterJahrgangBezeichnung.Visible = false;

            textBox_VierterJahrgangBezeichnung.Text = "";

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

            textBox_ErsterJahrgangBezeichnung.Visible = true;
            textBox_ZweiterJahrgangBezeichnung.Visible = true;
            textBox_DritterJahrgangBezeichnung.Visible = true;
            textBox_VierterJahrgangBezeichnung.Visible = true;

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
            CheckCheckboxes(checkBox_ErsterJahrgangAE);
            CheckCheckboxes(checkBox_ErsterJahrgangSI);

            //Ausbildungsgänge zweiter Jahrgang checken
            CheckCheckboxes(checkBox_ZweiterJahrgangAE);
            CheckCheckboxes(checkBox_ZweiterJahrgangSI);

            //Ausbildungsgänge dritter Jahrgang checken
            CheckCheckboxes(checkBox_DritterJahrgangAE);
            CheckCheckboxes(checkBox_DritterJahrgangSI);

            //Ausbildungsgänge dritter Jahrgang checken
            CheckCheckboxes(checkBox_VierterJahrgangAE);
            CheckCheckboxes(checkBox_VierterJahrgangSI);

            //Liste mit Jahrgangsbezeichnungen füllen
            FillIdentifierOfYearsList();

            //Liste der Speciality muss geleert werden, damit es nicht zu Überschneidungen mit gespeicherten Kalendern kommt
            MCalendar.GetInstance().Speciality.Clear();

            MCalendar.GetInstance().FillCalendarInitial(this.dateTimePicker_Von.Value, this.dateTimePicker_Bis.Value, numberOfYears, identifierOfYears, typeOfClasses, vacationCurrentYearUrl, vacationNextYearUrl, holidayCurrentYearUrl, holidayNextYearUrl);
            calendarWithDays = MCalendar.GetInstance();
        }

        /// <summary>
        /// Füllt die GridView
        /// </summary>
        /// <param name="calendarDayList">Liste mit Kalendertagen in Zeitraum</param>
        public void FillFormWithDataGridView(List<MCalendarDay> calendarDayList)
        {
            tagplanBearbeitenUC.CreateDataGridViews(CountCheckedCheckboxes());
            tagplanBearbeitenUC.FillGrids(calendarDayList);
            formInit.EnableBearbeitenStatistikTabPage();
            NextTabPage();
        }

        /// <summary>
        /// Erstellt ein Kalenderobjekt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Weiter_Click(object sender, EventArgs e)
        {
            if (vacationCurrentYearUrl != null && vacationNextYearUrl != null && holidayCurrentYearUrl != null && holidayNextYearUrl != null && CheckDateTimePickerValues() && CheckIdentificationAndClassesChoosen())
            {
                //Werte aus Datepicker werden an Kalenderobjekt übergeben
                CreateCalendarWithDates();

                //DataGridview wird erstellt, befüllt und übergeben
                FillFormWithDataGridView(calendarWithDays.CalendarList);

                //Liste mit Jahrgangsbezeichnungen und Klassenarten leeren
                identifierOfYears.Clear();
                typeOfClasses.Clear();
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
            using (ferienFeiertageAuswaehlenForm = new FerienFeiertageAuswaehlenForm(dateTimePicker_Von.Value, dateTimePicker_Bis.Value, vacationCurrentYearUrl, vacationNextYearUrl, holidayCurrentYearUrl, holidayNextYearUrl))
            {
                DialogResult dr = ferienFeiertageAuswaehlenForm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    holidayCurrentYearUrl = ferienFeiertageAuswaehlenForm.TextForBox1;
                    holidayNextYearUrl = ferienFeiertageAuswaehlenForm.TextForBox2;
                    vacationCurrentYearUrl = ferienFeiertageAuswaehlenForm.TextForBox3;
                    vacationNextYearUrl = ferienFeiertageAuswaehlenForm.TextForBox4;

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
        public void CheckCheckboxes(CheckBox checkBox)
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

        /// <summary>
        /// Füllt die Liste der Jahrgangsbezeichnungen und überprüft ob die nötigen Textfelder gesetzt wurden
        /// </summary>
        public void FillIdentifierOfYearsList()
        {
            if (radioButton_1Jahrgang.Checked)
            {
                identifierOfYears.Add(textBox_ErsterJahrgangBezeichnung.Text);

            }

            if (radioButton_2Jahrgaenge.Checked)
            {
                identifierOfYears.Add(textBox_ErsterJahrgangBezeichnung.Text);
                identifierOfYears.Add(textBox_ZweiterJahrgangBezeichnung.Text);
            }

            if (radioButton_3Jahrgaenge.Checked)
            {
                identifierOfYears.Add(textBox_ErsterJahrgangBezeichnung.Text);
                identifierOfYears.Add(textBox_ZweiterJahrgangBezeichnung.Text);
                identifierOfYears.Add(textBox_DritterJahrgangBezeichnung.Text);
            }

            if (radioButton_4Jahrgaenge.Checked)
            {
                identifierOfYears.Add(textBox_ErsterJahrgangBezeichnung.Text);
                identifierOfYears.Add(textBox_ZweiterJahrgangBezeichnung.Text);
                identifierOfYears.Add(textBox_DritterJahrgangBezeichnung.Text);
                identifierOfYears.Add(textBox_VierterJahrgangBezeichnung.Text);
            }
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
        private bool CheckDateTimePickerValues()
        {
            if (this.dateTimePicker_Von.Value > this.dateTimePicker_Bis.Value)
            {
                formInit.ShowMessageInStatusbar(MMessage.ERROR_STARTDATE_BIGGER_ENDDATE);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Überprüft ob für die ausgewählte Anzahl an Jahrgängen Klassen ausgewählt und Jahrgangsbezeichnungen eingetragen wurden
        /// </summary>
        /// <returns>Wahrheitswert ob alle nötigen Auswahlen vorgenommen wurden</returns>
        private bool CheckIdentificationAndClassesChoosen()
        {
            //TextBoxes und CheckBoxes werden weiß
            WhitenControls();

            switch (numberOfYears)
            {
                //Es wird überprüft ob mindestens eine entsprechende CheckBox angewählt ist und ob die zugehörige TextBox gefüllt ist
                case 1:
                    //Überprüfung erster Jahrgang
                    if (!CheckClassesChoosen(checkBox_ErsterJahrgangAE, checkBox_ErsterJahrgangSI) || !CheckIdentifierSet(textBox_ErsterJahrgangBezeichnung))
                    {
                        return false;
                    }
                    else return true;
                case 2:
                    //Überprüfung erster Jahrgang
                    if (!CheckClassesChoosen(checkBox_ErsterJahrgangAE, checkBox_ErsterJahrgangSI) || !CheckIdentifierSet(textBox_ErsterJahrgangBezeichnung))
                    {
                        return false;
                    }
                    //Überprüfung zweiter Jahrgang
                    else if
                        (!CheckClassesChoosen(checkBox_ZweiterJahrgangAE, checkBox_ZweiterJahrgangSI) || !CheckIdentifierSet(textBox_ZweiterJahrgangBezeichnung))
                    {
                        return false;
                    }
                    //Vergleich erster mit zweitem Jahrgang
                    else if
                        (!CheckIdentifierOfYearsNotEqual(textBox_ErsterJahrgangBezeichnung, textBox_ZweiterJahrgangBezeichnung))
                    {
                        return false;
                    }
                    else return true;
                case 3:
                    //Überprüfung erster Jahrgang
                    if (!CheckClassesChoosen(checkBox_ErsterJahrgangAE, checkBox_ErsterJahrgangSI) || !CheckIdentifierSet(textBox_ErsterJahrgangBezeichnung))
                    {
                        return false;
                    }
                    //Überprüfung zweiter Jahrgang
                    else if
                        (!CheckClassesChoosen(checkBox_ZweiterJahrgangAE, checkBox_ZweiterJahrgangSI) || !CheckIdentifierSet(textBox_ZweiterJahrgangBezeichnung))
                    {
                        return false;
                    }
                    //Überprüfung dritter Jahrgang
                    else if
                        (!CheckClassesChoosen(checkBox_DritterJahrgangAE, checkBox_DritterJahrgangSI) || !CheckIdentifierSet(textBox_DritterJahrgangBezeichnung))
                    {
                        return false;
                    }
                    //Vergleich erster mit zweitem/erster mit drittem/zweiter mit drittem Jahrgang
                    else if
                        (!CheckIdentifierOfYearsNotEqual(textBox_ErsterJahrgangBezeichnung, textBox_ZweiterJahrgangBezeichnung)
                        ||
                        !CheckIdentifierOfYearsNotEqual(textBox_ErsterJahrgangBezeichnung, textBox_DritterJahrgangBezeichnung)
                        ||
                        !CheckIdentifierOfYearsNotEqual(textBox_ZweiterJahrgangBezeichnung, textBox_DritterJahrgangBezeichnung))
                    {
                        return false;
                    }
                    else return true;
                case 4:
                    //Überprüfung erster Jahrgang
                    if (!CheckClassesChoosen(checkBox_ErsterJahrgangAE, checkBox_ErsterJahrgangSI) || !CheckIdentifierSet(textBox_ErsterJahrgangBezeichnung))
                    {
                        return false;
                    }
                    //Überprüfung zweiter Jahrgang
                    else if
                        (!CheckClassesChoosen(checkBox_ZweiterJahrgangAE, checkBox_ZweiterJahrgangSI) || !CheckIdentifierSet(textBox_ZweiterJahrgangBezeichnung))
                    {
                        return false;
                    }
                    //Überprüfung dritter Jahrgang
                    else if
                        (!CheckClassesChoosen(checkBox_DritterJahrgangAE, checkBox_DritterJahrgangSI) || !CheckIdentifierSet(textBox_DritterJahrgangBezeichnung))
                    {
                        return false;
                    }
                    //Überprüfung vierter Jahrgang
                    else if
                    (!CheckClassesChoosen(checkBox_VierterJahrgangAE, checkBox_VierterJahrgangSI) || !CheckIdentifierSet(textBox_VierterJahrgangBezeichnung))
                    {
                        return false;
                    }
                    //Vergleich erster mit zweitem/erster mit drittem/erster mit viertem/zweiter mit drittem/zweiter mit viertem/dritter mit viertem Jahrgang
                    else if
                        (!CheckIdentifierOfYearsNotEqual(textBox_ErsterJahrgangBezeichnung, textBox_ZweiterJahrgangBezeichnung)
                        ||
                        !CheckIdentifierOfYearsNotEqual(textBox_ErsterJahrgangBezeichnung, textBox_DritterJahrgangBezeichnung)
                        ||
                        !CheckIdentifierOfYearsNotEqual(textBox_ErsterJahrgangBezeichnung, textBox_VierterJahrgangBezeichnung)
                        ||
                        !CheckIdentifierOfYearsNotEqual(textBox_ZweiterJahrgangBezeichnung, textBox_DritterJahrgangBezeichnung)
                        ||
                        !CheckIdentifierOfYearsNotEqual(textBox_ZweiterJahrgangBezeichnung, textBox_VierterJahrgangBezeichnung)
                        ||
                        !CheckIdentifierOfYearsNotEqual(textBox_DritterJahrgangBezeichnung, textBox_VierterJahrgangBezeichnung))
                    {
                        return false;
                    }
                    else return true;
                default:
                    return true;
            }
        }

        /// <summary>
        /// Überprüft ob CheckBoxes ausgewählt sind
        /// </summary>
        /// <param name="checkBoxAE">Zu überprüfende CheckBox</param>
        /// <param name="checkBoxSI">Zu überprüfende CheckBox</param>
        /// <returns>Wahrheitswert ob eine der CheckBoxes ausgewählt ist </returns>
        public bool CheckClassesChoosen(CheckBox checkBoxAE, CheckBox checkBoxSI)
        {
            if (!checkBoxAE.Checked && !checkBoxSI.Checked)
            {
                checkBoxAE.BackColor = Color.FromArgb(255, 127, 80);
                checkBoxSI.BackColor = Color.FromArgb(255, 127, 80);
                formInit.ShowMessageInStatusbar(MMessage.WARNING_NO_CLASSES_CHOOSEN);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Überprüft ob in einer TextBox ein Text steht
        /// </summary>
        /// <param name="textBox">Zu überprüfende TextBox</param>
        /// <returns>Wahrheitswert ob ein Text in der TextBox steht</returns>
        public bool CheckIdentifierSet(TextBox textBox)
        {
            if (String.IsNullOrEmpty(textBox.Text))
            {
                formInit.ShowMessageInStatusbar(MMessage.WARNING_NO_IDENTIFICATION_SET);
                textBox.BackColor = Color.FromArgb(255, 127, 80);
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckIdentifierOfYearsNotEqual(TextBox textBoxA, TextBox textBoxB)
        {
            if (String.Equals(textBoxA.Text.ToUpper(), textBoxB.Text.ToUpper()))
            {
                textBoxA.BackColor = Color.FromArgb(255, 127, 80);
                textBoxB.BackColor = Color.FromArgb(255, 127, 80);
                formInit.ShowMessageInStatusbar(MMessage.WARNING_IDENTIFICATION_IS_EQUAL);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Weist den Controls die Hintergrundfarbe Weiß zu
        /// </summary>
        public void WhitenControls()
        {
            foreach (TextBox textBox in listOfTextBoxes)
            {
                textBox.BackColor = Color.White;
            }
            foreach (CheckBox checkBox in listOfCheckBoxes)
            {
                checkBox.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Füllt die Liste der Checkboxes und die Liste der Textboxes
        /// </summary>
        public void FillCheckBoxTextBoxLists()
        {
            //Liste wird mit allen Checkboxes gefüllt - Dient der Farbgestaltung bei Fehleingaben
            listOfCheckBoxes.Add(checkBox_ErsterJahrgangAE);
            listOfCheckBoxes.Add(checkBox_ErsterJahrgangSI);

            listOfCheckBoxes.Add(checkBox_ZweiterJahrgangAE);
            listOfCheckBoxes.Add(checkBox_ZweiterJahrgangSI);

            listOfCheckBoxes.Add(checkBox_DritterJahrgangAE);
            listOfCheckBoxes.Add(checkBox_DritterJahrgangSI);

            listOfCheckBoxes.Add(checkBox_VierterJahrgangAE);
            listOfCheckBoxes.Add(checkBox_VierterJahrgangSI);

            //Liste wird mit allen Textboxes gefüllt - Dient der Farbgestaltung bei Fehleingaben
            listOfTextBoxes.Add(textBox_ErsterJahrgangBezeichnung);
            listOfTextBoxes.Add(textBox_ZweiterJahrgangBezeichnung);
            listOfTextBoxes.Add(textBox_DritterJahrgangBezeichnung);
            listOfTextBoxes.Add(textBox_VierterJahrgangBezeichnung);
        }
    }
}