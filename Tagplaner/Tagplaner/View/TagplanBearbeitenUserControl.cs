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
    public partial class TagplanBearbeitenUserControl : UserControl
    {
        public TagplanBearbeitenUserControl()
        {
            InitializeComponent();
        }

        private void TagplanBearbeitenUserControl_Load(object sender, EventArgs e)
        {
            

            for (int i = 1; i < 10; i++)
            {
                ListViewItem lv = new ListViewItem();
                lv.Text = "0" + i + ".04.2015";
                lv.SubItems.Add("");
                lv.SubItems.Add("Osterferien");

                listView1.Items.Add(lv);                 
            }
        }
    }
}