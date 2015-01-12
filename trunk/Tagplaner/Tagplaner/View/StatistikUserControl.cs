using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tagplaner
{
    public partial class StatistikUserControl : UserControl
    {
        public StatistikUserControl()
        {
            InitializeComponent();
        }

        private void StatistikUserControl_Load(object sender, EventArgs e)
        {
            RefreshListView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshListView();

        }

        private void RefreshListView()
        {
            int allDays = CStatisticUtilitys.CountAllApprenticeshipDays();
            int weekendDays = CStatisticUtilitys.CountWeekendDays();
            int holidayDays = CStatisticUtilitys.CountHolidayDays();
            int apprenticeshipDays = allDays - weekendDays - holidayDays;

            listView1.Items.Clear();

            ListViewItem lvAllDays = new ListViewItem();
            lvAllDays.Text = "Tage gesamt";
            lvAllDays.SubItems.Add(Convert.ToString(allDays));

            ListViewItem lvApprenticeshipDays = new ListViewItem();
            lvApprenticeshipDays.Text = "Tage ohne Wochenenden und Feiertage";
            lvApprenticeshipDays.SubItems.Add(Convert.ToString(apprenticeshipDays));

            ListViewItem lvWeekendDays = new ListViewItem();
            lvWeekendDays.Text = "Samstage und Sonntage";
            lvWeekendDays.SubItems.Add(Convert.ToString(weekendDays));

            ListViewItem lvHolidayDays = new ListViewItem();
            lvHolidayDays.Text = "Feiertage";
            lvHolidayDays.SubItems.Add(Convert.ToString(holidayDays));

            listView1.Items.Add(lvAllDays);
            listView1.Items.Add(lvApprenticeshipDays);
            listView1.Items.Add(lvWeekendDays);
            listView1.Items.Add(lvHolidayDays);
        }
    }
}
