﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tagplaner;

namespace Tagplaner
{
    // TODO:
    // - Farbe der Seminarinfos -> Teilweise fertig
    // - Variablen für die Objekte anlegen um den Code zu verkleinern

    /// <summary>
    /// Autor: Niklas Wazal, Felix Smuda
    /// Datum: 13.01.15
    /// Diese Klasse enthält alle nötigen Methoden zum Erstellen der DatenTabelle und zum füllen, lesen und ändern von Einträgen.
    /// innerhalb der Tabelle
    /// </summary>
    /// 
    public partial class TagplanBearbeitenUserControl : UserControl
    {
        TagplanChangepanelUserControl tagplanChangePanelUserControl = new TagplanChangepanelUserControl();
        int x_Coord = 0;
        int y_Coord = 0;
        Color colorHoliday = Color.GreenYellow;
        Color colorSchool = Color.Blue;
        Color colorPractice = Color.Gold;
        Color colorSeminar = Color.LightSkyBlue;
        Color colorWeekend = Color.FromArgb(255, 191, 191, 191);

        MCalendar mCalendar;
        private static TagplanBearbeitenUserControl instance;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Erstellt eine Instanz der Klasse
        /// </summary>
        /// <returns></returns>
        public static TagplanBearbeitenUserControl GetInstance()
        {
            if (instance == null)
            {
                instance = new TagplanBearbeitenUserControl();
            } return instance;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Malt alle Elemente auf das UserControl
        /// </summary>
        /// 
        private TagplanBearbeitenUserControl()
        {
            InitializeComponent();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Fügt dem Panel das TagplanChangepanelUserControl hinzu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TagplanBearbeitenUserControl_Load(object sender, EventArgs e)
        {
            panel1.Controls.Add(tagplanChangePanelUserControl);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //------------------------------------------------------------------------------------------------------------
        // Im folgenden Bereich wird mehrfach eine Rechnung angewendet um für jede Zelle das entsprechende Objekt zu ermitteln.
        // Die ersten 4 Spalten sind für Kalender-Daten reserviert: Kalenderwoche, Datum, Ferien, Feiertage
        // Durch die folgenden Zahlen (4-9) werden die einzelnen Spalten für einen Jahrgang für eine Fachausrichtung belegt
        // Die Zahl 6 legt konstant fest, wie viele Zellen eine Fachrichtung besitzt, sprich zB ein Jahrgang AE.
        // Der ColumnCounter iteriert mit der Schleife durch jede Spalte durch und gewährleistet eine dynamische Menge von Jahrgängen.
        // Dieser wird dafür mit der Festgelegten Zahl, hier: 6, multipliziert.
        //
        // Verwendung in: 
        // - CreateDataGridViews()
        // - FillDataGrids()
        // - ApplyChangesToGrid()
        //------------------------------------------------------------------------------------------------------------------

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Diese Methode fügt dem DataGridView alle benötigten Columns hinzu entsprechend der gewählten
        /// Optionen im TagplanAnlegen-Fenster.
        /// Dazu erwartet sie einen Int-Wert als Parameter.
        /// </summary>
        /// <param name="countGrid"></param>
        public void CreateDataGridViews(int countGrid)
        {
            mCalendar = MCalendar.GetInstance();
            int columnCount = 6 * countGrid + 4;

            dGV.DataSource = null;
            dGV.Rows.Clear();
            dGV.MultiSelect = false;
            dGV.ColumnCount = columnCount;


            // Beschriftet alle ColumnHeader und setzt sie auf nicht-Sortierbar und ReadOnly
            ConfigureColumnHeader(countGrid);

        }//End CreateDataGridViews

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Diese Methode befüllt die Tabelle Initial mit allen Werten die im MCalendar Objekt stehen
        /// </summary>
        /// <param name="calendarDays"></param>
        public void FillGrids(List<MCalendarDay> calendarDays)
        {

            //TEST
            int columnCount = dGV.ColumnCount;
            //Testdaten
            /*                MTrainer trainer = new MTrainer("Arnold", "Bechtold", "AB", false, false);
                        MTrainer trainer_co = new MTrainer("Arnold", "Bechtold", "AB", false, true);
                        MSeminar seminar = new MSeminar("SEMINARRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR", "Subtitel", "SAP", "false", "commment");
                        MPractice practice = new MPractice("Praxis");
                        List<MRoom> room = new List<MRoom>();
                        room.Add(new MRoom("209"));
                        MPlace ort = new MPlace("Koeln", "Arnold", room);*/



            // Durchläuft jeden Kalendertag und schreibt jeden Entry in die DataGridView
            for (int rowCounter = 0; rowCounter < calendarDays.Count; rowCounter++)
            {
                if (calendarDays[rowCounter].CalendarDate.DayOfWeek.ToString() != "Saturday" && calendarDays[rowCounter].CalendarDate.DayOfWeek.ToString() != "Sunday")
                {
                    //Testdaten
                    /*                      trainer = new MTrainer("Arnold - " + rowCounter.ToString(), "Bechtold", "AB", false, false);
                                            trainer_co = new MTrainer("CO-Arnold - " + rowCounter.ToString(), "Bechtold", "AB", false, true);
                                            seminar = new MSeminar("SEMINARR - " + rowCounter.ToString(), "Subtitel", "SAP", "false", "commment");
                                            practice = new MPractice("Praxis - " + rowCounter.ToString());
                                            room.Add(new MRoom("209 - " + rowCounter.ToString()));
                                            ort = new MPlace("Koeln - " + rowCounter.ToString(), "Arnold", room);*/

                    dGV.Rows.Add();

                    dGV[0, rowCounter].Value = calendarDays[rowCounter].CalendarWeek;
                    dGV[1, rowCounter].Value = calendarDays[rowCounter].GetCalendarDatePrintDate();
                    if (calendarDays[rowCounter].HolidayName != null)
                        dGV[2, rowCounter].Value = calendarDays[rowCounter].HolidayName;
                    if (calendarDays[rowCounter].VacationName != null)
                        dGV[3, rowCounter].Value = calendarDays[rowCounter].VacationName;



                    if (calendarDays[rowCounter].HolidayName == null)
                    {

                        //Durchläuft alle Spalten der Tabelle und trägt alle Werte ein
                        for (int columnCounter = 0; columnCounter < columnCount / 6; columnCounter++)
                        {
                            //Testdaten
                            /*
                                                        if (rowCounter < 100)
                                                        {
                                                            if (columnCounter % 2 == 0)
                                                                mCalendar.CalendarList[rowCounter].CalendarEntry.Add(new MCalendarEntry(practice));
                                                            else
                                                                mCalendar.CalendarList[rowCounter].CalendarEntry.Add(new MCalendarEntry(trainer, trainer_co, seminar, ort, room[0]));
                                                        }
                                                        else
                                                        {
                                                            mCalendar.CalendarList[rowCounter].CalendarEntry.Add(new MCalendarEntry(new MSchool("HAUPTSCHULEE - " + rowCounter.ToString())));
                                                        }*/
                            //END Testdaten
                            if (mCalendar.CalendarList[rowCounter].CalendarEntry.ElementAtOrDefault(columnCounter) != null)
                            {
                                //Ab hier wird Unterschieden ob der CalendarEntry ein SchulObjekt, SeminarObjekt oder ein PraxisObjekt enthält
                                if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].School != null)
                                {
                                    FillSchool(columnCounter, rowCounter);
                                }
                                //Case: Ist ein Practice-Objekt vorhanden?
                                if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Practice != null)
                                {
                                    FillPractice(columnCounter, rowCounter);
                                }
                                //Case: Ist ein Seminarobjekt vorhanden?
                                if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Seminar != null)
                                {
                                    FillSeminar(mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter], columnCounter, rowCounter);

                                }
                                //Case: Sowohl Seminar als auch Praxis Objekt existieren
                                if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Seminar != null && mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Practice != null)
                                {
                                    FillSeminarAndPractice(mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter], columnCounter, rowCounter);
                                }
                            }//END Eintrag vorhanden
                            else
                            {
                                mCalendar.CalendarList[rowCounter].CalendarEntry.Add(new MCalendarEntry());
                            }

                        }
                    }//END Durchlaufen aller Spalten
                    else
                    {
                        //Schreibt den Feiertag quer in alle Spalten und färbt sie grün
                        for (int columnCounter = 4; columnCounter <= columnCount - 1; columnCounter++)
                        {
                            dGV[columnCounter, rowCounter].Value = calendarDays[rowCounter].HolidayName.ToString();
                            dGV[columnCounter, rowCounter].ReadOnly = true;

                            dGV[columnCounter, rowCounter].Style.BackColor = colorHoliday;
                        } //END for

                    }
                }
                else
                {
                    //Einfärben des Wochenendes
                    dGV.Rows.Add();
                    for (int columnCounter = 0; columnCounter <= columnCount - 1; columnCounter++)
                    {
                        dGV[columnCounter, rowCounter].ReadOnly = true;

                        dGV[columnCounter, rowCounter].Style.BackColor = colorWeekend;
                    }//END Einfärben des Wochenendes
                }// END else
            }// END Schreiben der Calendar Entrys
        }// END-FillGrids


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Diese Methode ermittelt das Model zu dem das gewählte Element gehört
        /// </summary>
        /// <returns>calendarEntry</returns>
        public MCalendarEntry GetSelectedEntryModel()
        {
            MCalendarEntry calendarEntry = MCalendar.GetInstance().CalendarList[y_Coord].CalendarEntry[x_Coord];

            return calendarEntry;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Hier werden die Änderungen übergeben und wieder in das DataGrid geschrieben
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="applyIteration"></param>
        public void ApplyChangesToGrid(int applyIteration, MCalendarEntry entry)
        {

            if (dGV.Rows.Count >= y_Coord + applyIteration)
            {
                for (int i = 0; i < applyIteration; i++)
                {
                    if (MCalendar.GetInstance().CalendarList[y_Coord + i].HolidayName == null && CheckWeekend(MCalendar.GetInstance().CalendarList[y_Coord + i].CalendarDate))
                    {
                        MCalendar.GetInstance().CalendarList[y_Coord + i].CalendarEntry[x_Coord] = entry;
                        if (entry.School != null)
                        {
                            FillSchool(i);
                        }
                        if (entry.Practice != null)
                        {
                            FillPractice(i);
                        }

                        if (entry.Seminar != null)
                        {
                            FillSeminar(i, entry);

                            if (entry.Practice != null && entry.Seminar != null)
                            {
                                FillSeminarAndPractice(i, entry);
                            }
                        }
                    }
                    else
                    {
                        //Wenn ein Feiertag ist wird die applyIteration um 1 erhöht
                        applyIteration++;
                    }

                }
            }
            else
            {
                MessageBox.Show("Mehr Einträge als Tage im gewählten Zeitraum vorhanden!");
            }

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void FillSchool(int day)
        {
            dGV[4 + 6 * x_Coord, y_Coord + day].Value = "";
            dGV[4 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorSchool;

            dGV[5 + 6 * x_Coord, y_Coord + day].Value = "";
            dGV[5 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorSchool;

            dGV[6 + 6 * x_Coord, y_Coord + day].Value = "";
            dGV[6 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorSchool;

            dGV[7 + 6 * x_Coord, y_Coord + day].Value = "";
            dGV[7 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorSchool;

            dGV[8 + 6 * x_Coord, y_Coord + day].Value = "Berufschule";
            dGV[8 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorSchool;

            dGV[9 + 6 * x_Coord, y_Coord + day].Value = "Berufschule";
            dGV[9 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorSchool;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void FillPractice(int day)
        {
            dGV[4 + 6 * x_Coord, y_Coord + day].Value = "";
            dGV[4 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorPractice;

            dGV[5 + 6 * x_Coord, y_Coord + day].Value = "";
            dGV[5 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorPractice;

            dGV[6 + 6 * x_Coord, y_Coord + day].Value = "";
            dGV[6 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorPractice;

            dGV[7 + 6 * x_Coord, y_Coord + day].Value = "";
            dGV[7 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorPractice;

            dGV[8 + 6 * x_Coord, y_Coord + day].Value = "Praxis";
            dGV[8 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorPractice;

            dGV[9 + 6 * x_Coord, y_Coord + day].Value = "Praxis";
            dGV[9 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorPractice;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void FillSeminar(int day, MCalendarEntry entry)
        {
            if (entry.Place != null)
                dGV[4 + 6 * x_Coord, y_Coord + day].Value = entry.Place.Place;
            dGV[4 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorSeminar;
            if (entry.Room != null)
                dGV[5 + 6 * x_Coord, y_Coord + day].Value = entry.Room.Number;
            dGV[5 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorSeminar;
            if (entry.Trainer != null)
                dGV[6 + 6 * x_Coord, y_Coord + day].Value = entry.Trainer.Name + " " + entry.Trainer.Surname;
            dGV[6 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorSeminar;

            if (entry.Cotrainer != null)
                dGV[7 + 6 * x_Coord, y_Coord + day].Value = entry.Cotrainer.Name + " " + entry.Cotrainer.Surname;
            dGV[7 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorSeminar;

            if (entry.Seminar.Title != null)
            {
                dGV[8 + 6 * x_Coord, y_Coord + day].Value = entry.Seminar.Title;
                dGV[9 + 6 * x_Coord, y_Coord + day].Value = entry.Seminar.Title;
            }
            dGV[8 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorSeminar;
            dGV[9 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorSeminar;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void FillSeminarAndPractice(int day, MCalendarEntry entry)
        {
            dGV[8 + 6 * x_Coord, y_Coord + day].Value = entry.Seminar.Title;
            dGV[9 + 6 * x_Coord, y_Coord + day].Value = "Praxis";

            dGV[8 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorSeminar;
            dGV[9 + 6 * x_Coord, y_Coord + day].Style.BackColor = colorPractice;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void FillSchool(int columnCounter, int rowCounter)
        {
            dGV[columnCounter, rowCounter].Value = "";
            dGV[columnCounter, rowCounter].Style.BackColor = colorSchool;

            dGV[columnCounter, rowCounter].Value = "";
            dGV[columnCounter, rowCounter].Style.BackColor = colorSchool;

            dGV[columnCounter, rowCounter].Value = "";
            dGV[columnCounter, rowCounter].Style.BackColor = colorSchool;

            dGV[columnCounter, rowCounter].Value = "";
            dGV[columnCounter, rowCounter].Style.BackColor = colorSchool;

            dGV[columnCounter, rowCounter].Value = "Berufschule";
            dGV[columnCounter, rowCounter].Style.BackColor = colorSchool;

            dGV[columnCounter, rowCounter].Value = "Berufschule";
            dGV[columnCounter, rowCounter].Style.BackColor = colorSchool;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void FillPractice(int columnCounter, int rowCounter)
        {
            dGV[columnCounter, rowCounter].Value = "";
            dGV[columnCounter, rowCounter].Style.BackColor = colorPractice;

            dGV[columnCounter, rowCounter].Value = "";
            dGV[columnCounter, rowCounter].Style.BackColor = colorPractice;

            dGV[columnCounter, rowCounter].Value = "";
            dGV[columnCounter, rowCounter].Style.BackColor = colorPractice;

            dGV[columnCounter, rowCounter].Value = "";
            dGV[columnCounter, rowCounter].Style.BackColor = colorPractice;

            dGV[columnCounter, rowCounter].Value = "Praxis";
            dGV[columnCounter, rowCounter].Style.BackColor = colorPractice;

            dGV[columnCounter, rowCounter].Value = "Praxis";
            dGV[columnCounter, rowCounter].Style.BackColor = colorPractice;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void FillSeminar(MCalendarEntry entry, int columnCounter, int rowCounter)
        {
            if (entry.Place != null)
                dGV[columnCounter, rowCounter].Value = entry.Place.Place;
            dGV[columnCounter, rowCounter].Style.BackColor = colorSeminar;
            if (entry.Room != null)
                dGV[columnCounter, rowCounter].Value = entry.Room.Number;
            dGV[columnCounter, rowCounter].Style.BackColor = colorSeminar;
            if (entry.Trainer != null)
                dGV[columnCounter, rowCounter].Value = entry.Trainer.Name + " " + entry.Trainer.Surname;
            dGV[columnCounter, rowCounter].Style.BackColor = colorSeminar;

            if (entry.Cotrainer != null)
                dGV[columnCounter, rowCounter].Value = entry.Cotrainer.Name + " " + entry.Cotrainer.Surname;
            dGV[columnCounter, rowCounter].Style.BackColor = colorSeminar;

            if (entry.Seminar.Title != null)
            {
                dGV[columnCounter, rowCounter].Value = entry.Seminar.Title;
                dGV[columnCounter, rowCounter].Value = entry.Seminar.Title;
            }
            dGV[columnCounter, rowCounter].Style.BackColor = colorSeminar;
            dGV[columnCounter, rowCounter].Style.BackColor = colorSeminar;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void FillSeminarAndPractice(MCalendarEntry entry, int columnCounter, int rowCounter)
        {
            dGV[columnCounter, rowCounter].Value = entry.Seminar.Title;
            dGV[columnCounter, rowCounter].Value = "Praxis";

            dGV[columnCounter, rowCounter].Style.BackColor = colorSeminar;
            dGV[columnCounter, rowCounter].Style.BackColor = colorPractice;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Wird eine Zelle angeklickt werden die Koordinaten der Zelle ausgelesen, die auch den Indizies der Objekte 
        /// entsprechend und es werden Methoden zum Bearbeiten der Einträge aufgerufen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x_Cell = e.ColumnIndex;
            x_Coord = (Convert.ToInt32(Math.Floor(Convert.ToDouble(e.ColumnIndex) - 4d)) / 6);
            y_Coord = e.RowIndex;
            if (CheckCellValidation(x_Cell))
            {
                panel1.Show();
                if (CheckEntryExist())
                    tagplanChangePanelUserControl.ChangeCalendarEntry(GetSelectedEntryModel());
                else
                    tagplanChangePanelUserControl.ChangeCalendarEntry(new MCalendarEntry());
            }
            else
            {
                panel1.Hide();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Diese Methode prüft, ob eine Zelle valide ist. D.h ob dort hinter ein Calendar Entry liegen kann.
        /// Feiertage, Wochenende, Header und Footer(Index out of Range) werden als Invalide ausgewertet und ignoriert.
        /// Mit der x_Cell Variablen, fangen wir das Klicken auf einer der ersten vier Spalten ab, da dort keine Calendar Entrys liegen,
        /// sondern nur Informationen bzgl. des Datums und der Freien Tage.
        /// </summary>
        /// <param name="x_Cell"></param>
        /// <returns></returns>
        public bool CheckCellValidation(int x_Cell)
        {
            if (!((x_Coord == dGV.ColumnCount - 1 || y_Coord == dGV.RowCount - 1)
                    || x_Cell <= 3
                    || y_Coord < 0
                    || !(CheckWeekend(MCalendar.GetInstance().CalendarList[y_Coord].CalendarDate))
                    || MCalendar.GetInstance().CalendarList[y_Coord].HolidayName != null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Überprüft ob die aktuelle Zelle ein Wochenend Tag ist.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool CheckWeekend(DateTime date)
        {
            if (date.DayOfWeek.ToString() == "Saturday" || date.DayOfWeek.ToString() == "Sunday")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckEntryExist()
        {
            bool exist = false;
            if (MCalendar.GetInstance().CalendarList[y_Coord].CalendarEntry[x_Coord] != null)
            {
                exist = true;
            }
            return exist;

        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Diese Methode erstellt initial alle benötigten Columns mit Headerbeschriftung und korrekten Propertys
        /// </summary>
        /// <param name="countGrid"></param>
        public void ConfigureColumnHeader(int countGrid)
        {
            dGV.Columns[0].Name = "KW";
            dGV.Columns[1].Name = "Datum";
            dGV.Columns[2].Name = "Ferien";
            dGV.Columns[3].Name = "Feiertage";

            dGV.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dGV.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dGV.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dGV.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

            dGV.Columns[0].ReadOnly = true;
            dGV.Columns[1].ReadOnly = true;
            dGV.Columns[2].ReadOnly = true;
            dGV.Columns[3].ReadOnly = true;

            for (int columnCounter = 0; columnCounter <= countGrid - 1; columnCounter++)
            {

                dGV.Columns[4 + 6 * columnCounter].Name = mCalendar.Speciality[columnCounter].SpecialityName + " " + mCalendar.Speciality[columnCounter].Apprenticeship + " Ort " + mCalendar.Speciality[columnCounter].IdentifierOfYear;
                dGV.Columns[5 + 6 * columnCounter].Name = mCalendar.Speciality[columnCounter].SpecialityName + " " + mCalendar.Speciality[columnCounter].Apprenticeship + " Raum " + mCalendar.Speciality[columnCounter].IdentifierOfYear;
                dGV.Columns[6 + 6 * columnCounter].Name = mCalendar.Speciality[columnCounter].SpecialityName + " " + mCalendar.Speciality[columnCounter].Apprenticeship + " Trainer " + mCalendar.Speciality[columnCounter].IdentifierOfYear;
                dGV.Columns[7 + 6 * columnCounter].Name = mCalendar.Speciality[columnCounter].SpecialityName + " " + mCalendar.Speciality[columnCounter].Apprenticeship + " Co-Trainer " + mCalendar.Speciality[columnCounter].IdentifierOfYear;
                dGV.Columns[8 + 6 * columnCounter].Name = mCalendar.Speciality[columnCounter].SpecialityName + " " + mCalendar.Speciality[columnCounter].Apprenticeship + " Aktivität " + mCalendar.Speciality[columnCounter].IdentifierOfYear;
                dGV.Columns[9 + 6 * columnCounter].Name = mCalendar.Speciality[columnCounter].SpecialityName + " " + mCalendar.Speciality[columnCounter].Apprenticeship + " Aktivität " + mCalendar.Speciality[columnCounter].IdentifierOfYear;

                dGV.Columns[4 + 6 * columnCounter].SortMode = DataGridViewColumnSortMode.NotSortable;
                dGV.Columns[5 + 6 * columnCounter].SortMode = DataGridViewColumnSortMode.NotSortable;
                dGV.Columns[6 + 6 * columnCounter].SortMode = DataGridViewColumnSortMode.NotSortable;
                dGV.Columns[7 + 6 * columnCounter].SortMode = DataGridViewColumnSortMode.NotSortable;
                dGV.Columns[8 + 6 * columnCounter].SortMode = DataGridViewColumnSortMode.NotSortable;
                dGV.Columns[9 + 6 * columnCounter].SortMode = DataGridViewColumnSortMode.NotSortable;

                dGV.Columns[4 + 6 * columnCounter].ReadOnly = true;
                dGV.Columns[5 + 6 * columnCounter].ReadOnly = true;
                dGV.Columns[6 + 6 * columnCounter].ReadOnly = true;
                dGV.Columns[7 + 6 * columnCounter].ReadOnly = true;
                dGV.Columns[8 + 6 * columnCounter].ReadOnly = true;
                dGV.Columns[9 + 6 * columnCounter].ReadOnly = true;
            }// End-Header-Configuration
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}