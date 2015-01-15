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
    public partial class SeminarVerwaltenUserControl : UserControl
    {
        CDatabase db;
        
        public SeminarVerwaltenUserControl()
        {  InitializeComponent();
            db = CDatabase.GetInstance();
            db.FillSeminarComboBox(seminarBox);
            
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void seminarBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSeminar seminar = (MSeminar)seminarBox.SelectedItem;
            button1.Enabled = false;


            textBox1.Text = seminar.Title;
            textBox2.Text = seminar.Subtitle;
            textBox3.Text = seminar.Abbreviation;
            textBox4.Text = seminar.HasTechnology;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            MSeminar seminar = new MSeminar(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);

            if (db.ContainsSeminar(seminar) == false)
            {

                bool erg = db.InsertSeminar(seminar);
                if (erg == true)
                {
                    seminarBox.Items.Clear();
                    seminarBox.Text = "";
                    db.FillSeminarComboBox(seminarBox);
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            seminarBox.Text = "";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
       
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MSeminar seminar = (MSeminar)seminarBox.SelectedItem;
            bool erg = db.DeleteSeminar(seminar);
            if (erg == true)
            {
                seminarBox.Text = "";
                seminarBox.Items.Clear();
                db.FillSeminarComboBox(seminarBox);
            }
        }
      
    }
}
