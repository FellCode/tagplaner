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
    public partial class RaumVerwaltenUserControl : UserControl
    {
        CDatabase db = new CDatabase();

        public RaumVerwaltenUserControl()
        {
            InitializeComponent();
            db.FillLocationCombobox(comboBox2);
        }

        private void RaumVerwaltenUserControl_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
             MPlace place = (MPlace) comboBox2.SelectedItem;
            db.InsertRoom(textBox1.Text, place.Id.ToString()); 
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox1.Items.Clear();
           MPlace place = (MPlace) comboBox2.SelectedItem;
             db.FillRoomCombobox(comboBox1, place.Id);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
           
        }
    }
}
