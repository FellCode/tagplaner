using System;
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
    /// <summary>
    /// Autor: Niklas Wazal, Felix Smuda
    /// Datum: 13.01.15
    /// Diese Klasse enthält alle nötigen Methoden zum Erstellen der DatenTabelle und zum füllen, lesen und ändern von Einträgen
    /// innerhalb der Tabelle
    /// </summary>
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


        /// <summary>
        /// Erstellt eine Instanz der Klasse
        /// </summary>
        /// <returns></returns>
        public static TagplanBearbeitenUserControl getInstance()
        {
            if (instance == null)
            {
                instance = new TagplanBearbeitenUserControl();
            } return instance;
        }
        /// <summary>
        /// Malt alle Elemente auf das UserControl
        /// </summary>
        /// 
        private TagplanBearbeitenUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fügt dem Panel das TagplanChangepanelUserControl hinzu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TagplanBearbeitenUserControl_Load(object sender, EventArgs e)
        {
            panel1.Controls.Add(tagplanChangePanelUserControl);
        }


        /// <summary>
        /// Diese Methode fügt dem DataGridView alle benötigten Columns hinzu entsprechend der gewählten
        /// Optionen im TagplanAnlegen-Fenster.
        /// Dazu erwartet sie einen Int-Wert als Parameter.
        /// </summary>
        /// <param name="countGrid"></param>
        public void CreateDataGridViews(int countGrid)
        {
            mCalendar = MCalendar.getInstance();

            dGV.DataSource = null;
            dGV.Rows.Clear();
            int columnCount = 6 * countGrid + 4;


            dGV.ColumnCount = columnCount;
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
                //Die Rechnung ermittelt für jede Fachausrichtung alle 6 Spalten, um diese einzeln ansteuern zu können
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
            }
        }

        /// <summary>
        /// Diese Methode befüllt die Tabelle Initial mit allen Werten die im MCalendar Objekt stehen
        /// </summary>
        /// <param name="calendarDays"></param>
        public void FillGrids(List<MCalendarDay> calendarDays)
        {


            //dGV.DataSource = null;
            //dGV.Rows.Clear();

            //TEST
            int columnCount = dGV.ColumnCount;
            MTrainer trainer = new MTrainer("Arnold", "Bechtold", "AB", false, false);
            MTrainer trainer_co = new MTrainer("Arnold", "Bechtold", "AB", false, true);
            MSeminar seminar = new MSeminar("SEMINARRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR", "Subtitel", "SAP", "false", "commment");
            MPractice practice = new MPractice("Praxis");
            List<MRoom> room = new List<MRoom>();
            room.Add(new MRoom("209"));
            MPlace ort = new MPlace("Koeln", "Arnold", room);



            // Durchläuft jeden Kalendertag
            for (int rowCounter = 0; rowCounter < calendarDays.Count; rowCounter++)
            {
                if (calendarDays[rowCounter].CalendarDate.DayOfWeek.ToString() != "Saturday" && calendarDays[rowCounter].CalendarDate.DayOfWeek.ToString() != "Sunday")
                {
                    trainer = new MTrainer("Arnold - " + rowCounter.ToString(), "Bechtold", "AB", false, false);
                    trainer_co = new MTrainer("CO-Arnold - " + rowCounter.ToString(), "Bechtold", "AB", false, true);
                    seminar = new MSeminar("SEMINARR - " + rowCounter.ToString(), "Subtitel", "SAP", "false", "commment");
                    practice = new MPractice("Praxis - " + rowCounter.ToString());
                    room.Add(new MRoom("209 - " + rowCounter.ToString()));
                    ort = new MPlace("Koeln - " + rowCounter.ToString(), "Arnold", room);

                    dGV.Rows.Add();

                    dGV[0, rowCounter].Value = calendarDays[rowCounter].CalendarWeek.ToString();
                    dGV[1, rowCounter].Value = calendarDays[rowCounter].GetCalendarDatePrintDate();
                    if (calendarDays[rowCounter].HolidayName != null)
                        dGV[2, rowCounter].Value = calendarDays[rowCounter].HolidayName.ToString();
                    if (calendarDays[rowCounter].VacationName != null)
                        dGV[3, rowCounter].Value = calendarDays[rowCounter].VacationName.ToString();

                    dGV.Columns[0].ReadOnly = true;
                    dGV.Columns[1].ReadOnly = true;
                    dGV.Columns[2].ReadOnly = true;
                    dGV.Columns[3].ReadOnly = true;

                    if (calendarDays[rowCounter].HolidayName == null)
                    {

                        //Durchläuft jede Spalte der Tabelle
                        for (int columnCounter = 0; columnCounter < columnCount / 6; columnCounter++)
                        {
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
                            }


                            //Ab hier wird Unterschieden ob der CalendarEntry ein SchulObjekt, SeminarObjekt oder ein PraxisObjekt enthält
                            if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].School != null)
                            {
                                dGV[8 + 6 * columnCounter, rowCounter].Value = "Schule";
                                dGV[9 + 6 * columnCounter, rowCounter].Value = "Schule";

                                dGV[8 + 6 * columnCounter, rowCounter].Style.BackColor = colorSchool;
                                dGV[9 + 6 * columnCounter, rowCounter].Style.BackColor = colorSchool;

                            }
                            if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Seminar != null)
                            {
                                if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Place != null)
                                    dGV[4 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Place.Place.ToString();
                                if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Room.Number != null)
                                    dGV[5 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Room.Number.ToString();
                                if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Trainer.Name != null)
                                    dGV[6 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Trainer.Name.ToString();
                                if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Cotrainer.Name != null)
                                    dGV[7 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Cotrainer.Name.ToString();
                                if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Seminar.Title != null)
                                {
                                    dGV[8 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Seminar.Title.ToString();
                                    dGV[9 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Seminar.Title.ToString();
                                }

                                dGV[4 + 6 * columnCounter, rowCounter].Style.BackColor = colorSeminar;
                                dGV[5 + 6 * columnCounter, rowCounter].Style.BackColor = colorSeminar;
                                dGV[6 + 6 * columnCounter, rowCounter].Style.BackColor = colorSeminar;
                                dGV[7 + 6 * columnCounter, rowCounter].Style.BackColor = colorSeminar;
                                dGV[8 + 6 * columnCounter, rowCounter].Style.BackColor = colorSeminar;
                                dGV[9 + 6 * columnCounter, rowCounter].Style.BackColor = colorSeminar;

                            }
                            if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Practice != null)
                            {
                                dGV[8 + 6 * columnCounter, rowCounter].Value = "Praxis";
                                dGV[9 + 6 * columnCounter, rowCounter].Value = "Praxis";

                                dGV[8 + 6 * columnCounter, rowCounter].Style.BackColor = colorPractice;
                                dGV[9 + 6 * columnCounter, rowCounter].Style.BackColor = colorPractice;

                            }
                            //Case: Sowohl Seminar als auch Praxis Objekt existieren
                            if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Seminar != null && mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Practice != null)
                            {
                                dGV[8 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Seminar.Title.ToString();
                                dGV[9 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Practice.Comment.ToString();
                                dGV[8 + 6 * columnCounter, rowCounter].Style.BackColor = colorSeminar;
                                dGV[9 + 6 * columnCounter, rowCounter].Style.BackColor = colorPractice;
                            }
                        }
                    }
                    else
                    {
                        for (int columnCounter = 4; columnCounter <= columnCount - 1; columnCounter++)
                        {
                            dGV[columnCounter, rowCounter].Value = calendarDays[rowCounter].HolidayName.ToString();
                            dGV[columnCounter, rowCounter].ReadOnly = true;

                            dGV[columnCounter, rowCounter].Style.BackColor = colorHoliday;
                        }

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
                    }
                }
            }
        }

        /// <summary>
        /// Diese Methode ermittelt das Model zu dem das gewählte Element gehört
        /// </summary>
        /// <returns>calendarEntry</returns>
        public MCalendarEntry GetSelectedEntryModel()
        {
            MCalendarEntry calendarEntry = MCalendar.getInstance().CalendarList[y_Coord].CalendarEntry[x_Coord];

            return calendarEntry;
        }

        /// <summary>
        /// Hier werden die Änderungen übergeben und wieder in das DataGrid geschrieben
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="applyIteration"></param>
        public void ApplyChangesToGrid(int applyIteration, MCalendarEntry entry)
        {
            double bereich = 0;
            bereich = (Math.Floor(Convert.ToDouble(x_Coord) / 6));
            if (dGV.Rows.Count >= y_Coord + applyIteration)
            {
                for (int i = 0; i < applyIteration; i++)
                {
                    if (MCalendar.getInstance().CalendarList[y_Coord + i].HolidayName == null && CheckWeekend(MCalendar.getInstance().CalendarList[y_Coord + i].CalendarDate))
                    {
                        MCalendar.getInstance().CalendarList[y_Coord + i].CalendarEntry[x_Coord] = entry;
                        if (entry.School != null)
                        {
                            dGV[8 + 6 * Convert.ToInt32(bereich), y_Coord + i].Value = "Berufschule";
                            dGV[9 + 6 * Convert.ToInt32(bereich), y_Coord + i].Value = "Berufschule";
                            dGV[8 + 6 * Convert.ToInt32(bereich), y_Coord + i].Style.BackColor = colorSchool;
                            dGV[9 + 6 * Convert.ToInt32(bereich), y_Coord + i].Style.BackColor = colorSchool;
                        }
                        if (entry.Practice != null)
                        {
                            dGV[8 + 6 * Convert.ToInt32(bereich), y_Coord + i].Value = "Praxis";
                            dGV[9 + 6 * Convert.ToInt32(bereich), y_Coord + i].Value = "Praxis";
                            dGV[8 + 6 * Convert.ToInt32(bereich), y_Coord + i].Style.BackColor = colorPractice;
                            dGV[9 + 6 * Convert.ToInt32(bereich), y_Coord + i].Style.BackColor = colorPractice;
                        }
                        if (entry.Seminar != null)
                        {
                            if (entry.Place.Place != null)
                                dGV[4 + 6 * Convert.ToInt32(bereich), y_Coord + i].Value = entry.Place.Place.ToString();
                            if (entry.Room.Number != null)
                                dGV[5 + 6 * Convert.ToInt32(bereich), y_Coord + i].Value = entry.Room.Number.ToString();
                            if (entry.Trainer.Name != null)
                                dGV[6 + 6 * Convert.ToInt32(bereich), y_Coord + i].Value = entry.Trainer.Name.ToString();
                            if (entry.Cotrainer.Name != null)
                                dGV[7 + 6 * Convert.ToInt32(bereich), y_Coord + i].Value = entry.Cotrainer.Name.ToString();
                            if (entry.Seminar.Title != null)
                            {
                                dGV[8 + 6 * Convert.ToInt32(bereich), y_Coord + i].Value = entry.Seminar.Title.ToString();
                                dGV[9 + 6 * Convert.ToInt32(bereich), y_Coord + i].Value = entry.Seminar.Title.ToString();

                                dGV[8 + 6 * Convert.ToInt32(bereich), y_Coord + i].Style.BackColor = colorSeminar;
                                dGV[9 + 6 * Convert.ToInt32(bereich), y_Coord + i].Style.BackColor = colorSeminar;
                            }

                            if (entry.Practice != null && entry.Seminar != null)
                            {
                                dGV[8 + 6 * Convert.ToInt32(bereich), y_Coord + i].Value = entry.Seminar.Title.ToString();
                                dGV[9 + 6 * Convert.ToInt32(bereich), y_Coord + i].Value = "Praxis";

                                dGV[8 + 6 * Convert.ToInt32(bereich), y_Coord + i].Style.BackColor = colorSeminar;
                                dGV[9 + 6 * Convert.ToInt32(bereich), y_Coord + i].Style.BackColor = colorPractice;
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


        /// <summary>
        /// Wird eine Zelle angeklickt werden die Koordinaten der Zelle ausgelesen, die auch den Indizies der Objekte 
        /// entsprechend und es werden Methoden zum Bearbeiten der Einträge aufgerufen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x_Cell = e.ColumnIndex;
            x_Coord = Convert.ToInt32(Math.Floor((Convert.ToDouble(e.ColumnIndex) - 4d) / 6));
            y_Coord = e.RowIndex;
            if (CheckCellValidation(x_Cell))
            {
                tagplanChangePanelUserControl.ChangeCalendarEntry(GetSelectedEntryModel());
            }
        }

        public bool CheckCellValidation(int x_Cell)
        {
            if (!(((x_Coord == dGV.ColumnCount - 1
                    || y_Coord == dGV.RowCount - 1)
                    && dGV[x_Cell, y_Coord].ReadOnly == true)
                    || x_Cell <= 3
                    || y_Coord < 0
                    || (!CheckWeekend(MCalendar.getInstance().CalendarList[y_Coord].CalendarDate))
                    || MCalendar.getInstance().CalendarList[y_Coord].HolidayName != null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckWeekend(DateTime date)
        {
            if (!(date.DayOfWeek.ToString() == "Saturday" || date.DayOfWeek.ToString() == "Sunday"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}