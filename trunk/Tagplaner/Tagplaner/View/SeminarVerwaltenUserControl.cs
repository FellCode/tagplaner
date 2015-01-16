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
        {
            InitializeComponent();
            db = CDatabase.GetInstance();
            db.FillSeminarComboBox(seminarComboBox);


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void seminarBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSeminar seminar = (MSeminar)seminarComboBox.SelectedItem;
            speichernButton.Enabled = false;


            titelTextBox.Text = seminar.Title;
            untertitelTextBox.Text = seminar.Subtitle;
            kuerzelTextBox.Text = seminar.Abbreviation;
            technikTextBox.Text = seminar.HasTechnology;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            MSeminar seminar = new MSeminar(titelTextBox.Text, untertitelTextBox.Text, kuerzelTextBox.Text, technikTextBox.Text);


            bool erg = db.InsertSeminar(seminar);
            if (erg == true)
            {
                seminarComboBox.Items.Clear();
                seminarComboBox.Text = "";
                db.FillSeminarComboBox(seminarComboBox);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            speichernButton.Enabled = true;
            seminarComboBox.Text = "";
            titelTextBox.Clear();
            untertitelTextBox.Clear();
            kuerzelTextBox.Clear();
            technikTextBox.Clear();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MSeminar seminar = (MSeminar)seminarComboBox.SelectedItem;
            bool erg = db.DeleteSeminar(seminar);
            if (erg == true)
            {

                speichernButton.Enabled = true;
                seminarComboBox.Text = "";
                titelTextBox.Clear();
                untertitelTextBox.Clear();
                kuerzelTextBox.Clear();
                technikTextBox.Clear();

                seminarComboBox.Text = "";
                seminarComboBox.Items.Clear();
                db.FillSeminarComboBox(seminarComboBox);
            }
        }

    }
}
