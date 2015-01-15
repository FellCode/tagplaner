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
    public partial class TrainerVerwaltenUserControl : UserControl
    {
        CDatabase db;
      
        
        public TrainerVerwaltenUserControl()
        {
            db = CDatabase.GetInstance();

            InitializeComponent();
            db.FillTrainerComboBox(comboBox1);
        }

        private void TrainerVerwaltenUserControl_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MTrainer trainer = (MTrainer) comboBox1.SelectedItem;
            textBox1.Text = trainer.Name;
            textBox2.Text = trainer.Surname;
            textBox3.Text = trainer.Abbreviation;

            if (trainer.IsInternal == true)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else if (trainer.IsInternal == false)
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {

                MTrainer trainer = new MTrainer(textBox1.Text,textBox2.Text,textBox3.Text,true);
                db.InsertTrainer(trainer);
            }
            else if (radioButton2.Checked == true)
            {
                MTrainer trainer = new MTrainer(textBox1.Text, textBox2.Text, textBox3.Text, false);
                db.InsertTrainer(trainer);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MTrainer trainer = (MTrainer)comboBox1.SelectedItem;
            db.DeleteTrainer(trainer);
        }
    }
}
