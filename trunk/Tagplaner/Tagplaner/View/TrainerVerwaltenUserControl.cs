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
            db.FillTrainerComboBox(trainerComboBox);


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
            speichernButton.Enabled = true;
            trainerComboBox.Text = "";
            nachnameTextBox.Clear();
            vornameTextBox.Clear();
            kuerzelTextBox.Clear();
            internRadioButton.Checked = false;
            externRadioButton.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            speichernButton.Enabled = false;

            MTrainer trainer = (MTrainer)trainerComboBox.SelectedItem;
            nachnameTextBox.Text = trainer.Name;
            vornameTextBox.Text = trainer.Surname;
            kuerzelTextBox.Text = trainer.Abbreviation;

            speichernButton.Enabled = false;
            if (trainer.IsInternal == true)
            {
                internRadioButton.Checked = true;
                externRadioButton.Checked = false;
            }
            else if (trainer.IsInternal == false)
            {
                externRadioButton.Checked = true;
                internRadioButton.Checked = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool erg = false;
            MTrainer trainer = null;
            if (internRadioButton.Checked == true)
            {
                trainer = new MTrainer(nachnameTextBox.Text, vornameTextBox.Text, kuerzelTextBox.Text, true);

            }
            else if (externRadioButton.Checked == true)
            {
                trainer = new MTrainer(nachnameTextBox.Text, vornameTextBox.Text, kuerzelTextBox.Text, false);

            }

            erg = db.InsertTrainer(trainer);
            if (erg == true)
            {
                trainerComboBox.Text = "";
                trainerComboBox.Items.Clear();
                db.FillTrainerComboBox(trainerComboBox);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MTrainer trainer = (MTrainer)trainerComboBox.SelectedItem;
            bool erg = db.DeleteTrainer(trainer);

            if (erg == true)
            {

                speichernButton.Enabled = true;
                trainerComboBox.Text = "";
                nachnameTextBox.Clear();
                vornameTextBox.Clear();
                kuerzelTextBox.Clear();
                internRadioButton.Checked = false;
                externRadioButton.Checked = false;

                trainerComboBox.Items.Clear();
                trainerComboBox.Text = "";
                db.FillTrainerComboBox(trainerComboBox);
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
