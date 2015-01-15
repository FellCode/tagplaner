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
            dGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGV.Location = new System.Drawing.Point(10, 10);
            dGV.TabIndex = 0;
            dGV.ColumnCount = 4;
            dGV.Dock = System.Windows.Forms.DockStyle.Fill;

            dGV.Columns[0].Name = "KW";
            dGV.Columns[1].Name = "Datum";
            dGV.Columns[2].Name = "Ferien";
            dGV.Columns[3].Name = "Feiertage";

            dGV.Columns[0].ReadOnly = true;
            dGV.Columns[1].ReadOnly = true;
            dGV.Columns[2].ReadOnly = true;
            dGV.Columns[3].ReadOnly = true;

            dGV.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dGV.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dGV.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dGV.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        public void AddDGV(DataGridView dGV)
        {
            this.Controls.Clear();
            this.Controls.Add(dGV);
        }
        public DataGridView returnDGV()
        {
            return dGV;
        }

        
    }
}