﻿using System;
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
    /// Autor: Niklas Wazal, Felix Smuda
    /// Datum: 13.01.15
    /// Diese Klasse enthält alle nötigen Methoden zum Erstellen der DatenTabelle und zum füllen, lesen und ändern von Einträgen
    /// innerhalb der Tabelle
    /// ------TODO------
    /// -Schleife im Apply Testen
    /// - Spaltenvariable einfügen
    /// - Feiertage (evtl Ferien) Grün färben 
    /// </summary>
    class CEditPlan
    {

        int x_Coord = 0;
        int y_Coord = 0;

        private DataGridView dGV = new DataGridView();

        /// <summary>
        /// Diese Methode erstellt die Tabelle entsprechend der Menge an Blöcken die benötigt werden.
        /// Dazu erwartet sie einen Int-Wert als Parameter.
        /// </summary>
        /// <param name="countGrid"></param>
        /// <returns></returns>
        public DataGridView createDataGridViews(int countGrid)
        {
            TagplanBearbeitenUserControl userControl = new TagplanBearbeitenUserControl();
            ListView listView = userControl.GetListView();

            int columnCount = 6* countGrid + 4;
            int space = 10;
            int drawingSizeX = 700;
            int drawingSizeY = 400;
            int drawingPointY = 0;
            int drawingPointX = listView.Size.Width + space;


            this.dGV = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).BeginInit();
            

            this.dGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV.Location = new System.Drawing.Point(drawingPointX, drawingPointY);
            this.dGV.Size = new System.Drawing.Size(drawingSizeX, drawingSizeY);
            this.dGV.TabIndex = 0;
            this.dGV.ColumnCount = columnCount;

            this.dGV.Columns[0].Name = "KW";
            this.dGV.Columns[1].Name = "Datum";
            this.dGV.Columns[2].Name = "Ferien";
            this.dGV.Columns[3].Name = "Feiertage";

            this.dGV.Columns[0].ReadOnly = true;
            this.dGV.Columns[1].ReadOnly = true;
            this.dGV.Columns[2].ReadOnly = true;
            this.dGV.Columns[3].ReadOnly = true;

            this.dGV.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dGV.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dGV.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dGV.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;


            for(int columnCounter=0; columnCounter <= countGrid - 1 ;columnCounter++)
            {
                this.dGV.Columns[4 + 6 * columnCounter].Name = "Ort";
                this.dGV.Columns[5 + 6 * columnCounter].Name = "Raum";
                this.dGV.Columns[6 + 6 * columnCounter].Name = "Trainer";
                this.dGV.Columns[7 + 6 * columnCounter].Name = "Co-Trainer";
                this.dGV.Columns[8 + 6 * columnCounter].Name = "Aktivität";
                this.dGV.Columns[9 + 6 * columnCounter].Name = "Aktivität";

                this.dGV.Columns[4 + 6 * columnCounter].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dGV.Columns[5 + 6 * columnCounter].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dGV.Columns[6 + 6 * columnCounter].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dGV.Columns[7 + 6 * columnCounter].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dGV.Columns[8 + 6 * columnCounter].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dGV.Columns[9 + 6 * columnCounter].SortMode = DataGridViewColumnSortMode.NotSortable;

            }

            this.dGV.CellClick += new DataGridViewCellEventHandler(TagplanBearbeitenUserControl_CellClick);

            return dGV;
        }

        /// <summary>
        /// Wird in eine Zelle der Tabelle geklickt, started es die Methodedie ermittelt welches Model zu dem angeklickten Element gehört
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagplanBearbeitenUserControl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x_Cell = e.ColumnIndex;
            x_Coord = Convert.ToInt32(Math.Floor(Convert.ToDouble(e.ColumnIndex) / 6));
            y_Coord = e.RowIndex;

            if (x_Cell <= 3 || y_Coord < 0 || x_Coord == dGV.ColumnCount - 1 || y_Coord == dGV.RowCount - 1 || dGV[x_Cell, y_Coord].ReadOnly == true)
            {
                MessageBox.Show("Keine gueltige Zelle");
            }
            else
            {
                getSelectedEntryModel();
            }
        }

        /// <summary>
        /// Diese Methode befüllt die Tabelle Initial mit allen Werten die im MCalendar Objekt stehen
        /// </summary>
        /// <param name="dGV"></param>
        /// <param name="columnCount"></param>
        /// <param name="listView"></param>
        public void fillGrids(DataGridView dGV, List<MCalendarDay> calendarDays)
        {
            MCalendar mCalendar = MCalendar.getInstance();

            //TEST
            int columnCount = dGV.ColumnCount;
            MTrainer trainer = new MTrainer("Arnold", "Bechtold", "AB", false, false);
            MTrainer trainer_co = new MTrainer("Arnold", "Bechtold", "AB", false, true);
            MSeminar seminar = new MSeminar("SEMINARRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR", "Subtitel", "SAP", "false", "commment");
            List<MRoom> room = new List<MRoom>();
            room.Add(new MRoom("209"));
            MPlace ort = new MPlace("Koeln", "Arnold", room);

            // Durchläuft jeden Kalendertag
            for (int rowCounter = 0; rowCounter < calendarDays.Count ; rowCounter++)
            {
                dGV.Rows.Add();
                
                dGV[0,rowCounter].Value = calendarDays[rowCounter].CalendarWeek.ToString();
                dGV[1 ,rowCounter].Value = calendarDays[rowCounter].CalendarDate.ToString();
                if (calendarDays[rowCounter].HolidayName != null)
                    dGV[2 , rowCounter].Value = calendarDays[rowCounter].HolidayName.ToString();
                if (calendarDays[rowCounter].VacationName != null) 
                    dGV[3 , rowCounter].Value = calendarDays[rowCounter].VacationName.ToString();

                if (calendarDays[rowCounter].HolidayName == null)
                {

                    //Durchläuft jede Spalte der Tabelle
                    for (int columnCounter = 0; columnCounter < columnCount / 6; columnCounter++)
                    {
                        mCalendar.CalendarList[rowCounter].CalendarEntry.Add(new MCalendarEntry(trainer, trainer_co, seminar, ort, room[0]));
                        dGV[4 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Place.Place.ToString();
                        dGV[5 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Room.Number.ToString();
                        dGV[6 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Trainer.Name.ToString();
                        dGV[7 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Cotrainer.Name.ToString();

                        //Ab hier wird Unterschieden ob der CalendarEntry ein SchulObjekt, SeminarObjekt oder ein PraxisObjekt enthält
                        if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].School != null)
                        {
                            dGV[8 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].School.Comment.ToString();
                            dGV[9 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].School.Comment.ToString();
                        }
                        if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Seminar != null)
                        {
                            dGV[8 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Seminar.Title.ToString();
                            dGV[9 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Seminar.Title.ToString();
                        }
                        if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Practice != null)
                        {
                            dGV[8 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Practice.Comment.ToString();
                            dGV[9 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Practice.Comment.ToString();
                        }
                        //Case: Sowohl Seminar als auch Praxis Objekt existieren
                        if (mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Seminar != null && mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Practice != null)
                        {
                            dGV[8 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Seminar.Title.ToString();
                            dGV[9 + 6 * columnCounter, rowCounter].Value = mCalendar.CalendarList[rowCounter].CalendarEntry[columnCounter].Practice.Comment.ToString();

                        }
                    }
                }
                else
                {
                    for (int columnCounter = 4; columnCounter <= columnCount - 1; columnCounter++)
                    {
                        dGV[columnCounter, rowCounter].Value = calendarDays[rowCounter].HolidayName.ToString();
                        dGV[columnCounter, rowCounter].ReadOnly = true;
                    }
                }
            }
        }
            
        /// <summary>
        /// Diese Methode ermittelt das Model zu dem das gewählte Element gehört
        /// </summary>
        /// <returns></returns>
        public MCalendarEntry getSelectedEntryModel()
        {
            MCalendarEntry calendarEntry = MCalendar.getInstance().CalendarList[y_Coord].CalendarEntry[x_Coord];

            return calendarEntry;
        }

        /// <summary>
        /// Hier werden die Änderungen übergeben und wieder in das DataGrid geschrieben
        /// </summary>
        /// <param name="entry"></param>
        public void ApplyChangesToGrid(int applyIteration, MCalendarEntry entry)
        {
            MCalendar.getInstance().CalendarList[y_Coord].CalendarEntry[x_Coord] = entry;
            double bereich = 0;
            bereich = (Math.Floor(Convert.ToDouble(x_Coord) / 6));

            for (int i = 0; i < applyIteration; i++)
            {
                //Die Rechnung ermittelt für jede Fachausrichtung alle 6 Spalten, um diese einzeln ansteuern zu können
                dGV[4 + 6 * Convert.ToInt32(bereich), y_Coord+i].Value = entry.Place.Place.ToString();
                dGV[5 + 6 * Convert.ToInt32(bereich), y_Coord+i].Value = entry.Room.Number.ToString();
                dGV[6 + 6 * Convert.ToInt32(bereich), y_Coord+i].Value = entry.Trainer.Name.ToString();
                dGV[7 + 6 * Convert.ToInt32(bereich), y_Coord+i].Value = entry.Cotrainer.Name.ToString();

                if (entry.School != null)
                {
                    dGV[8 + 6 * Convert.ToInt32(bereich), y_Coord+i].Value = entry.School.Comment.ToString();
                    dGV[9 + 6 * Convert.ToInt32(bereich), y_Coord+i].Value = entry.School.Comment.ToString();
                }
                if (entry.Practice != null)
                {
                    dGV[8 + 6 * Convert.ToInt32(bereich), y_Coord+i].Value = entry.Practice.Comment.ToString();
                    dGV[9 + 6 * Convert.ToInt32(bereich), y_Coord+i].Value = entry.Practice.Comment.ToString();
                }
                if (entry.Seminar != null)
                {
                    dGV[8 + 6 * Convert.ToInt32(bereich), y_Coord+i].Value = entry.Seminar.Comment.ToString();
                    dGV[9 + 6 * Convert.ToInt32(bereich), y_Coord+i].Value = entry.Seminar.Comment.ToString();
                }

                if (entry.Practice.Id != 0 && entry.Seminar != null)
                {
                    dGV[8 + 6 * Convert.ToInt32(bereich), y_Coord+i].Value = entry.Practice.Comment.ToString();
                    dGV[9 + 6 * Convert.ToInt32(bereich), y_Coord+i].Value = entry.Seminar.Comment.ToString();
                }
            }
        }
    }    
}