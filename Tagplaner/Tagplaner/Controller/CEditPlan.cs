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
        private List<DataGridView> dGVList = new List<DataGridView>();
        private DataGridView dGV = new DataGridView();

        public List<DataGridView> createDataGridViews(int countGrid){
            TagplanBearbeitenUserControl userControl = new TagplanBearbeitenUserControl();
            ListView listView = userControl.getListView();

            int columnCount = 6;
            int space = 10;
            int drawingSizeX = 250;
            int drawingSizeY = 250;
            int drawingPointY =10;
            int drawingPointX = listView.Size.Width + space;

            for (int i = 1; i <= countGrid; i++)
            {      
                this.dGV = new System.Windows.Forms.DataGridView();
                ((System.ComponentModel.ISupportInitialize)(this.dGV)).BeginInit();
                this.SuspendLayout();

                this.dGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                this.dGV.Location = new System.Drawing.Point(drawingPointX, drawingPointY);
                this.dGV.Size = new System.Drawing.Size(drawingSizeX, drawingSizeY);
                this.dGV.TabIndex = 0;
                this.dGV.ColumnCount = columnCount;
                dGVList.Add(dGV);
                drawingPointX = drawingPointX + drawingSizeX + space;
            }
            fillGrids(dGVList, columnCount);
            return dGVList;
        }
        private void fillGrids(List<DataGridView> dGVList, int columnCount)
        {
            MCalendar mCalendar = MCalendar.getInstance();
           
            //TEST

            MTrainer trainer = new MTrainer("Arnold", "Bechtold", "AB", false, false);
            MTrainer trainer_co = new MTrainer("", "", "", false, true);
            MSpeciality mspec = new MSpeciality("AE", "2104", "Koeln");
            MSeminar seminar = new MSeminar("Titel", "Subtitel", "SAP", "false", "commment");
            List<MRoom> room = new List<MRoom>(109);
            MPlace ort = new MPlace("Koeln", "Arnold", room);

            // calendar.CalendarList[0].CalendarEntry = new List<MCalendarEntry>;

            
       
            ///Test
            Console.WriteLine("1");
            for (int y = 0; y < 300 ; y++){
                mCalendar.CalendarList[y].CalendarEntry.Add(new MCalendarEntry(trainer, trainer_co, mspec, seminar, ort, room[0]));
            }
            ListView listView = getListView();
            for (int i = 0; i < listView.Items.Count - 1; i++)
            {
                Console.WriteLine("2");
                if (mCalendar.CalendarList[i].CalendarDate == Convert.ToDateTime(listView.Items[i].SubItems["Datum"/*"columnHeader2"*/]))
                {
                    Console.WriteLine("3");
                    if (mCalendar.CalendarList[i].HolidayName != "")
                    {
                        for (int j = 0; j <= columnCount; i++)
                        {
                            Console.WriteLine("4");
                            this.dGV = dGVList[j];

                            this.dGV[0, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Room;
                            this.dGV[1, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Trainer;
                            this.dGV[2, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Place;
                            this.dGV[3, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Practice;

                            if (mCalendar.CalendarList[i].CalendarEntry[j].School.ToString() != "")
                            {
                                this.dGV[4, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].School;
                                this.dGV[5, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].School;
                            }
                            if (mCalendar.CalendarList[i].CalendarEntry[j].Seminar.ToString() != "")
                            {
                                this.dGV[4, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Seminar;
                                this.dGV[5, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Seminar;
                            }
                            if (mCalendar.CalendarList[i].CalendarEntry[j].Practice.ToString() != "")
                            {
                                this.dGV[4, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Practice;
                                this.dGV[5, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Practice;
                            }
                            if (mCalendar.CalendarList[i].CalendarEntry[j].Seminar.ToString() != "" && mCalendar.CalendarList[i].CalendarEntry[j].Practice.ToString() != "")
                            {
                                this.dGV[4, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Seminar;
                                this.dGV[5, i].Value = mCalendar.CalendarList[i].CalendarEntry[j].Practice;
                            }
                        }
                    }
                    else
                    {
                        for (int k = 0 ; k <= columnCount; k++)
                        {
                            this.dGV[k, i].Value = mCalendar.CalendarList[i].HolidayName;
                        }
                        
                    }
                }
            }
        }
    }
}
