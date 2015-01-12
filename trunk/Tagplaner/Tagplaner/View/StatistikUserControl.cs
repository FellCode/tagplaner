﻿using System;
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

        public void RefreshListView() {
            listView1.Items.Clear();

            listView1.Items.Add(GetAllDaysListViewItem());
            listView1.Items.Add(GetApprenticeshipDaysListViewItem());
            listView1.Items.Add(GetWeekendDaysListViewItem());
            listView1.Items.Add(GetHolidayDaysListViewItem());
        }

        private ListViewItem GetAllDaysListViewItem()
        {
            int allDays = CStatisticUtilitys.CountAllApprenticeshipDays();

            ListViewItem lvAllDays = new ListViewItem();
            lvAllDays.Text = "Tage gesamt";
            lvAllDays.SubItems.Add(Convert.ToString(allDays));

            return lvAllDays;
        }

        private ListViewItem GetApprenticeshipDaysListViewItem()
        {
            int allDays = CStatisticUtilitys.CountAllApprenticeshipDays();
            int weekendDays = CStatisticUtilitys.CountWeekendDays();
            int holidayDays = CStatisticUtilitys.CountHolidayDays();
            int apprenticeshipDays = allDays - weekendDays - holidayDays;

            ListViewItem lvApprenticeshipDays = new ListViewItem();
            lvApprenticeshipDays.Text = "Tage ohne Wochenenden und Feiertage";
            lvApprenticeshipDays.SubItems.Add(Convert.ToString(apprenticeshipDays));

            return lvApprenticeshipDays;
        }

        private ListViewItem GetWeekendDaysListViewItem()
        {
            int weekendDays = CStatisticUtilitys.CountWeekendDays();

            ListViewItem lvWeekendDays = new ListViewItem();
            lvWeekendDays.Text = "Samstage und Sonntage";
            lvWeekendDays.SubItems.Add(Convert.ToString(weekendDays));

            return lvWeekendDays;
        }

        private ListViewItem GetHolidayDaysListViewItem()
        {
            int holidayDays = CStatisticUtilitys.CountHolidayDays();

            ListViewItem lvHolidayDays = new ListViewItem();
            lvHolidayDays.Text = "Feiertage";
            lvHolidayDays.SubItems.Add(Convert.ToString(holidayDays));

            return lvHolidayDays;
        }
    }
}