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
    /// <summary>
    /// UserControl das berechnete Statistiken in einem ListView anzeigt
    /// </summary>
    public partial class StatistikUserControl : UserControl
    {
        /// <summary>
        /// Erzeugt eine neue Instanz von StatistikUserControl
        /// </summary>
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

        /// <summary>
        /// Aktualisiert die Statistikanzeige in der ListView
        /// </summary>
        public void RefreshListView() {
            listView1.Items.Clear();

            listView1.Items.Add(GetAllDaysListViewItem());
            listView1.Items.Add(GetApprenticeshipDaysListViewItem());
            listView1.Items.Add(GetWeekendDaysListViewItem());
            listView1.Items.Add(GetHolidayDaysListViewItem());
            listView1.Items.Add(GetSeminarDaysListViewItem());
            listView1.Items.Add(GetSchoolDaysListViewItem());
            listView1.Items.Add(GetPraticeDaysListViewItem());
        }

        /// <summary>
        /// Erstellt ein ListViewItem mit der Statistik-Informationen "Tage gesamt"
        /// </summary>
        /// <returns>ListViewItem für Tage gesamt</returns>
        private ListViewItem GetAllDaysListViewItem()
        {
            int allDays = CStatisticUtilitys.CountAllApprenticeshipDays();

            ListViewItem lvAllDays = new ListViewItem();
            lvAllDays.Text = "Tage gesamt";
            lvAllDays.SubItems.Add(Convert.ToString(allDays));

            return lvAllDays;
        }

        /// <summary>
        /// Erstellt ein ListViewItem mit der Statistik-Informationen "Tage gesamt ohne Wochenenden und Feiertage"
        /// </summary>
        /// <returns>ListViewItem für Tage gesamt ohne Wochenenden und Feiertage</returns>
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

        /// <summary>
        /// Erstellt ein ListViewItem mit der Statistik-Informationen "Anzahl Samstage und Sonntage"
        /// </summary>
        /// <returns>ListViewItem für die Anzahl von Samstagen und Sonntagen</returns>
        private ListViewItem GetWeekendDaysListViewItem()
        {
            int weekendDays = CStatisticUtilitys.CountWeekendDays();

            ListViewItem lvWeekendDays = new ListViewItem();
            lvWeekendDays.Text = "Anzahl Samstage und Sonntage";
            lvWeekendDays.SubItems.Add(Convert.ToString(weekendDays));

            return lvWeekendDays;
        }

        /// <summary>
        /// Erstellt ein ListViewItem mit der Statistik-Informationen "Anzahl Feiertage"
        /// </summary>
        /// <returns>ListViewItem für die Anzahl der Feiertage</returns>
        private ListViewItem GetHolidayDaysListViewItem()
        {
            int holidayDays = CStatisticUtilitys.CountHolidayDays();

            ListViewItem lvHolidayDays = new ListViewItem();
            lvHolidayDays.Text = "Anzahl Feiertage";
            lvHolidayDays.SubItems.Add(Convert.ToString(holidayDays));

            return lvHolidayDays;
        }

        /// <summary>
        /// Erstellt ein ListViewItem mit der Statistik-Informationen "Seminartage"
        /// </summary>
        /// <returns>ListViewItem für die Anzahl der Seminartage</returns>
        private ListViewItem GetSeminarDaysListViewItem()
        {
            int seminarDays = CStatisticUtilitys.CountSeminarDays();

            ListViewItem lvSeminarDays = new ListViewItem();
            lvSeminarDays.Text = "Seminartage";
            lvSeminarDays.SubItems.Add(Convert.ToString(seminarDays));

            return lvSeminarDays;
        }

        /// <summary>
        /// Erstellt ein ListViewItem mit der Statistik-Informationen "Schultage"
        /// </summary>
        /// <returns>ListViewItem für die Anzahl der Schultage</returns>
        private ListViewItem GetSchoolDaysListViewItem()
        {
            int schoolDays = CStatisticUtilitys.CountSchoolDays();

            ListViewItem lvSchoolDays = new ListViewItem();
            lvSchoolDays.Text = "Schultage";
            lvSchoolDays.SubItems.Add(Convert.ToString(schoolDays));

            return lvSchoolDays;
        }

        /// <summary>
        /// Erstellt ein ListViewItem mit der Statistik-Informationen "Praxistage"
        /// </summary>
        /// <returns>ListViewItem für die Anzahl der Praxistage</returns>
        private ListViewItem GetPraticeDaysListViewItem()
        {
            int praticeDays = CStatisticUtilitys.CountSchoolDays();

            ListViewItem lvPraticeDays = new ListViewItem();
            lvPraticeDays.Text = "Praxistage";
            lvPraticeDays.SubItems.Add(Convert.ToString(praticeDays));

            return lvPraticeDays;
        }
    }
}
