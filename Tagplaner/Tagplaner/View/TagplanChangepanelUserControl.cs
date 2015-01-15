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
    public partial class TagplanChangepanelUserControl : UserControl
    {
        CDatabase cdb = CDatabase.GetInstance();
        CChangepanel ccp = new CChangepanel();

        public TagplanChangepanelUserControl()
        {
            InitializeComponent();
        }

        private void TagplanChangepanelUserControl_Load(object sender, EventArgs e)
        {

        }

        private void Einfügen_Click(object sender, EventArgs e)
        {

        }

        private void Raum_SelectedIndexChanged(object sender, EventArgs e)
        {
            MRoom sraum = (MRoom)Raum.SelectedItem;

            cdb.FillRoomComboBox(Raum, sraum.Id);
        }

        private void Tagart_SelectedIndexChanged(object sender, EventArgs e)
        {

            ccp.ChangeVisibility(Tagart, Seminarpanel);

        }
    }
}
