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
    class CEditPlan : TagplanBearbeitenUserControl
    {

        int x_Coord = 0;
        int y_Coord = 0;

        private DataGridView dGV = new DataGridView();

        public DataGridView createDataGridViews(int countGrid)
        {
            TagplanBearbeitenUserControl userControl = new TagplanBearbeitenUserControl();
            ListView listView = userControl.getListView();

            int columnCount = 6 * countGrid;
            int space = 10;
            int drawingSizeX = 700;
            int drawingSizeY = 700;
            int drawingPointY = 0;
            int drawingPointX = listView.Size.Width + space;


            this.dGV = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).BeginInit();
            this.SuspendLayout();

            this.dGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV.Location = new System.Drawing.Point(drawingPointX, drawingPointY);
            this.dGV.Size = new System.Drawing.Size(drawingSizeX, drawingSizeY);
            this.dGV.TabIndex = 0;
            this.dGV.ColumnCount = columnCount;
            this.dGV.CellClick += new DataGridViewCellEventHandler(TagplanBearbeitenUserControl_CellClick);


            return dGV;
        }


        private void TagplanBearbeitenUserControl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            x_Coord = e.ColumnIndex;
            y_Coord = e.RowIndex;
            getSelectedEntryModel();
        }
        public void fillGrids(DataGridView dGV, int columnCount, ListView listView)
        {
            MCalendar mCalendar = MCalendar.getInstance();

            //TEST

            MTrainer trainer = new MTrainer("Arnold", "Bechtold", "AB", false, false);
            MTrainer trainer_co = new MTrainer("Arnold", "Bechtold", "AB", false, true);
            MSpeciality mspec = new MSpeciality("AE", "2104", "Koeln");
            MSeminar seminar = new MSeminar("", "Subtitel", "SAP", "false", "commment");
            List<MRoom> room = new List<MRoom>();
            room.Add(new MRoom(209));
            MPlace ort = new MPlace("Koeln", "Arnold", room);




            for (int i = 0; i < listView.Items.Count - 1; i++)
            {
                if (!(mCalendar.CalendarList[i].CalendarDate == Convert.ToDateTime(listView.Items[i].SubItems["Datum"])))
                {
                    if (mCalendar.CalendarList[i].HolidayName != "")
                    {
                        dGV.Rows.Add();
                        for (int j = 0; j < columnCount / 6; j++)
                        {
                            mCalendar.CalendarList[i].CalendarEntry.Add(new MCalendarEntry(trainer, trainer_co, mspec, seminar, ort, room[0], new MSchool("Schule"), new MPractice("")));

                            Console.WriteLine("Bla");

                            dGV[0 + 6 * j, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Room.Number.ToString();
                            dGV[1 + 6 * j, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Trainer.Name.ToString();
                            dGV[2 + 6 * j, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Place.Place.ToString();
                            dGV[3 + 6 * j, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Practice.Comment.ToString();

                            if (mCalendar.CalendarList[i].CalendarEntry[j].School.Id != 0)
                            {
                                dGV[4 + 6 * j, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].School.Comment.ToString();
                                dGV[5 + 6 * j, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].School.Comment.ToString();
                                Console.WriteLine("IF-School");
                            }
                            if (mCalendar.CalendarList[i].CalendarEntry[j].Seminar.Title.ToString() != "")
                            {
                                dGV[4 + 6 * j, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Seminar;
                                dGV[5 + 6 * j, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Seminar;
                                Console.WriteLine("IF-Seminar");
                            }
                            if (mCalendar.CalendarList[i].CalendarEntry[j].Practice.Id != 0)
                            {
                                dGV[4 + 6 * j, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Practice.Comment.ToString();
                                dGV[5 + 6 * j, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Practice.Comment.ToString();
                                Console.WriteLine("IF-Practice: " + mCalendar.CalendarList[i].CalendarEntry[j].Practice.Id.ToString());
                            }
                            if (mCalendar.CalendarList[i].CalendarEntry[j].Seminar.Title.ToString() != "" && mCalendar.CalendarList[i].CalendarEntry[j].Practice.Id != 0)
                            {
                                //    dGV[4 + 6 * j, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Seminar;
                                //   dGV[5 + 6 * j, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Practice.;
                                Console.WriteLine(mCalendar.CalendarList[i].CalendarEntry[j].Seminar.Title.ToString());
                                Console.WriteLine(mCalendar.CalendarList[i].CalendarEntry[j].Practice.Comment.ToString());
                            }
                        }

                    }
                    else
                    {
                        for (int k = 0; k <= columnCount; k++)
                        {
                            dGV[k, i].Value = mCalendar.CalendarList[i].HolidayName;
                        }

                    }
                }
            }
        }

        public MCalendarEntry getSelectedEntryModel()
        {


            MCalendarEntry calendarEntry = MCalendar.getInstance().CalendarList[y_Coord].CalendarEntry[x_Coord];

            return calendarEntry;
        }
        public void ApplyChangesToGrid(MCalendarEntry entry)
        {

            MCalendar.getInstance().CalendarList[y_Coord].CalendarEntry[x_Coord] = entry;
            double bereich = 0;
            bereich = (Math.Floor(Convert.ToDouble(x_Coord) / 6));



            dGV[6 * Convert.ToInt32(bereich), y_Coord].Value = entry.Trainer.Name.ToString();
            dGV[6 * Convert.ToInt32(bereich) + 1, y_Coord].Value = entry.Cotrainer.Name.ToString();
            dGV[6 * Convert.ToInt32(bereich) + 2, y_Coord].Value = entry.Place.Place.ToString();
            dGV[6 * Convert.ToInt32(bereich) + 3, y_Coord].Value = entry.Room.Number.ToString();
            if (entry.School.Id != 0)
            {
                dGV[6 * Convert.ToInt32(bereich) + 4, y_Coord].Value = entry.School.Comment.ToString();
                dGV[6 * Convert.ToInt32(bereich) + 5, y_Coord].Value = entry.School.Comment.ToString();
            }
            if(entry.Practice.Id !=0)
            {
                dGV[6 * Convert.ToInt32(bereich) + 4, y_Coord].Value = entry.Practice.Comment.ToString();
                dGV[6 * Convert.ToInt32(bereich) + 5, y_Coord].Value = entry.Practice.Comment.ToString();
            }
            if(entry.Seminar.Title.ToString() != "")
            {
                dGV[6 * Convert.ToInt32(bereich) + 4, y_Coord].Value = entry.Seminar.Comment.ToString();
                dGV[6 * Convert.ToInt32(bereich) + 5, y_Coord].Value = entry.Seminar.Comment.ToString();
            }
            
            if(entry.Practice.Id !=0 && entry.Seminar.Title.ToString() != "")
            {
                dGV[6 * Convert.ToInt32(bereich) + 4, y_Coord].Value = entry.Practice.Comment.ToString();
                dGV[6 * Convert.ToInt32(bereich) + 5, y_Coord].Value = entry.Seminar.Comment.ToString();

            }

        }
    }
}
