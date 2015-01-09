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
        private FormInit formInit;
        MCalendar calendarWithDays;
        TagplanBearbeitenUserControl tagplanBearbeitenUC;
        CSerialize serializer;

        public TagplanAnlegenUserControl(FormInit formInit, TagplanBearbeitenUserControl tagplanBearbeitenUC)
        {
            this.formInit = formInit;
            this.tagplanBearbeitenUC = tagplanBearbeitenUC;
            InitializeComponent();
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
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        //Speichern eines Tagplans
        private void button3_Click(object sender, EventArgs e)
        {
         
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            /*
            calendarWithDays = MCalendar.getInstance();
            serializer.SerializeObject(calendarWithDays);

             */
            System.IO.Directory.CreateDirectory(@"C:\Tagplan");

            saveFileDialog1.InitialDirectory = @"C:\Tagplan";
            saveFileDialog1.Filter = "Tagplan|*.tp";
            saveFileDialog1.Title = "Tagplan abspeichern";
            
            saveFileDialog1.ShowDialog();
         
            calendarWithDays = MCalendar.getInstance();
            serializer = new CSerialize();
            serializer.SerializeObject(calendarWithDays, saveFileDialog1.FileName);
        }

        //Öffnen eines Tagplans
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\Tagplan";
            openFileDialog1.Title = "Tagplan öffnen";
            openFileDialog1.Filter = "Tagplan|*.tp";
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
            string[] result = openFileDialog1.FileNames;

            StringBuilder stringBuilder = new StringBuilder();

            foreach (string y in result)
            {
                stringBuilder.Append(y);
            }
            MessageBox.Show(stringBuilder.ToString(), "Tagplan geöffnet");

            serializer = new CSerialize();
            calendarWithDays = serializer.DeserializeObject(openFileDialog1.FileName);

            fillListViewWithDays(calendarWithDays.CalendarList, tagplanBearbeitenUC.getListView());
            nextTabPage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nextTabPage();
            //Werte aus Datepicker werden an Kalenderobjekt übergeben
            getCalendarWithDates();
            //ListView wird mit Tagen gefüllt
            fillListViewWithDays(calendarWithDays.CalendarList, tagplanBearbeitenUC.getListView());
        }

        public void nextTabPage()
        {
            formInit.tabPageChange(1);
        }

        public void getCalendarWithDates()
        {
            MCalendar.getInstance().fillCalendarInitial(this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            calendarWithDays = MCalendar.getInstance();
        }

        public void fillListViewWithDays(List<MCalendarDay> calendarDayList, ListView listView)
        {
            foreach (MCalendarDay calendarDay in calendarDayList)
            {
                ListViewItem listViewItem = new ListViewItem();

                if (int.Parse(calendarDay.CalendarWeek) % 2 == 0)
                {
                    listViewItem.Text = calendarDay.CalendarWeek;
                    listViewItem.SubItems.Add(calendarDay.CalendarDate.ToString());

                    listViewItem.SubItems[0].BackColor = Color.LightSkyBlue;
                    listViewItem.SubItems[1].BackColor = Color.LightSkyBlue;
                    listViewItem.UseItemStyleForSubItems = false;
                }

                else
                {
                    listViewItem.Text = calendarDay.CalendarWeek;
                    listViewItem.SubItems.Add(calendarDay.CalendarDate.ToString());
                }

                if (calendarDay.VacationName != null)
                {
                    listViewItem.SubItems.Add(calendarDay.VacationName);

                    listViewItem.SubItems[2].BackColor = Color.LightGreen;
                    listViewItem.UseItemStyleForSubItems = false;
                }

                if (calendarDay.HolidayName != null && calendarDay.VacationName != null)
                {
                    listViewItem.SubItems.Add(calendarDay.HolidayName);

                    listViewItem.SubItems[3].BackColor = Color.LightGreen;
                    listViewItem.UseItemStyleForSubItems = false;
                }
                
                if (calendarDay.HolidayName != null)
                {
                    listViewItem.SubItems.Add("");
                    listViewItem.SubItems.Add(calendarDay.HolidayName);

                    listViewItem.SubItems[3].BackColor = Color.LightGreen;
                    listViewItem.UseItemStyleForSubItems = false;
                }

                listView.Items.Add(listViewItem);
            }
        }
    }
}