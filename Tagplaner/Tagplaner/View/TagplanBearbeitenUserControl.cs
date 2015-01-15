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

        private void dGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CEditPlan cEditPlan = new CEditPlan();
            int x_Coord = 0;
            int y_Coord = 0;
            int x_Cell = e.ColumnIndex;
            x_Coord = Convert.ToInt32(Math.Floor((Convert.ToDouble(e.ColumnIndex) - 4d) / 6));
            y_Coord = e.RowIndex;

            if (!((x_Cell <= 3 || y_Coord < 0 || x_Coord == dGV.ColumnCount - 1 || y_Coord == dGV.RowCount - 1) && dGV[x_Cell, y_Coord].ReadOnly == true))
            {
                cEditPlan.getSelectedEntryModel();
            }
        }

        
    }
}