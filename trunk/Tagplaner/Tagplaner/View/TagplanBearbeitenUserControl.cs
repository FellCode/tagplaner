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
        private DataGridView dGV;

        public TagplanBearbeitenUserControl()
        {
            InitializeComponent();
            dGV = new DataGridView();
        }

        private void TagplanBearbeitenUserControl_Load(object sender, EventArgs e)
        {
            CEditPlan cEditPlan = new CEditPlan();
            dGV = cEditPlan.createDataGridViews(3);
            this.Controls.Add(dGV);
        }

        public ListView GetListView()
        {
            return this.listView1;
        }
        public DataGridView dGVReturn()
        {
            return dGV;
        }
    }
}