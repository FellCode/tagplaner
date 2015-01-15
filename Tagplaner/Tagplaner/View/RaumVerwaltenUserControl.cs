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
        CDatabase db;

        public RaumVerwaltenUserControl()
        {
            db = CDatabase.GetInstance();
            InitializeComponent();
            db.FillPlaceComboBox(comboBox2);


        }

        private void RaumVerwaltenUserControl_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            textBox1.Clear();
            comboBox1.Text = "";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MRoom room = (MRoom)comboBox1.SelectedItem;

            button1.Enabled = false;
            textBox1.Text = room.Number;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MPlace place = (MPlace)comboBox2.SelectedItem;
            MRoom room = new MRoom(textBox1.Text, place.Id);


            bool erg = db.InsertRoom(room);

            if (erg == true)
            {
                comboBox1.Text = "";
                comboBox1.Items.Clear();
                db.FillRoomComboBox(comboBox1, place.Id);
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox1.Items.Clear();


            MPlace place = (MPlace)comboBox2.SelectedItem;
            db.FillRoomComboBox(comboBox1, place.Id);

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            MRoom room = (MRoom)comboBox1.SelectedItem;
            bool erg = db.DeleteRoom(room);
            if (erg == true)
            {
                button1.Enabled = true;
                textBox1.Clear();
                comboBox1.Text = "";

                comboBox1.Items.Clear();
                comboBox1.Text = "";
                db.FillTrainerComboBox(comboBox1);
            }
        }
    }
}
