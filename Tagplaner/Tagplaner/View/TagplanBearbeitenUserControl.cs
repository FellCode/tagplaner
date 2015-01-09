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
                //ListViewItem listViewItem = new ListViewItem();
                //listViewItem.Text = "Noch kein Tagplan vorhanden";
                //listView1.Items.Add(listViewItem);
        }

        public ListView getListView(){
            return this.listView1;
        }
    }
}