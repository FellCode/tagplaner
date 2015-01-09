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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            ListViewItem lv = new ListViewItem();
            lv.Text = "Tage gesamt";
            lv.SubItems.Add(Convert.ToString(MCalendar.getInstance().CalendarList.Count));

            listView1.Items.Add(lv);
        }
    }
}
