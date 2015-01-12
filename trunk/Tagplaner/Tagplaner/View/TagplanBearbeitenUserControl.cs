using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tagplaner;


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

        }

        public ListView GetListView(){
            return this.listView1;
        }
    }
}