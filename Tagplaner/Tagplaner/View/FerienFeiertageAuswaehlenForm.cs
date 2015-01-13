using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tagplaner.View
{
    public partial class FerienFeiertageAuswaehlenForm : Form
    {
        Boolean file1Choosen;/* = false;*/
        Boolean file2Choosen;/* = false;*/
        Boolean file3Choosen;/* = false;*/
        Boolean file4Choosen;/* = false;*/

        String textFromTextBox1;
        String textFromTextBox2;
        String textFromTextBox3;
        String textFromTextBox4;

        public String TextForBox1
        {
            get { return textFromTextBox1; }
        }
        public String TextForBox2
        {
            get { return textFromTextBox2; }
        }
        public String TextForBox3
        {
            get { return textFromTextBox3; }
        }
        public String TextForBox4
        {
            get { return textFromTextBox4; }
        }
        public FerienFeiertageAuswaehlenForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //openHolidayFile(textBox1, file1Choosen, textFromTextBox1, "Feiertagdatei aktuelles Jahr öffnen");
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Feiertage");

            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Feiertage";
            openFileDialog1.Title = "Feiertagedatei aktuelles Jahr öffnen";
            openFileDialog1.Filter = "Feiertage|*.csv";
            openFileDialog1.Multiselect = true;

            DialogResult fileChoiceResult = openFileDialog1.ShowDialog();

            if (fileChoiceResult == DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
                this.textFromTextBox1 = openFileDialog1.FileName;
                file1Choosen = true;
                CheckAllFilesChoosen();
            }
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //openHolidayFile(textBox2, file2Choosen, textFromTextBox2, "Feiertagdatei nächstes Jahr öffnen");

            OpenFileDialog openFileDialog2 = new OpenFileDialog();

            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Feiertage");

            openFileDialog2.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Feiertage";
            openFileDialog2.Title = "Feiertagdatei nächstes Jahr öffnen";
            openFileDialog2.Filter = "Feiertage|*.csv";
            openFileDialog2.Multiselect = true;

            DialogResult fileChoiceResult = openFileDialog2.ShowDialog();

            if (fileChoiceResult == DialogResult.OK)
            {
                this.textBox2.Text = openFileDialog2.FileName;
                this.textFromTextBox2 = openFileDialog2.FileName;
                file2Choosen = true;
                CheckAllFilesChoosen();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //openVacationFile(textBox3, this.file3Choosen, textFromTextBox3, "Feriendatei aktuelles Jahr öffnen");
            
            OpenFileDialog openFileDialog3 = new OpenFileDialog();

            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Ferien");

            openFileDialog3.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Ferien";
            openFileDialog3.Title = "Feriendatei aktuelles Jahr öffnen";
            openFileDialog3.Filter = "Ferien|*.ics";
            openFileDialog3.Multiselect = true;

            DialogResult fileChoiceResult = openFileDialog3.ShowDialog();

            if (fileChoiceResult == DialogResult.OK)
            {
                this.textBox3.Text = openFileDialog3.FileName;
                this.textFromTextBox3 = openFileDialog3.FileName;
                file3Choosen = true;
                CheckAllFilesChoosen();
            }
             
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //openVacationFile(textBox4, this.file4Choosen, textFromTextBox4, "Feriendatei nächstes Jahr öffnen");
            
            OpenFileDialog openFileDialog4 = new OpenFileDialog();

            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Ferien");

            openFileDialog4.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Ferien";
            openFileDialog4.Title = "Feriendatei nächstes Jahr öffnen";
            openFileDialog4.Filter = "Ferien|*.ics";
            openFileDialog4.Multiselect = true;

            DialogResult fileChoiceResult = openFileDialog4.ShowDialog();

            if (fileChoiceResult == DialogResult.OK)
            {
                this.textBox4.Text = openFileDialog4.FileName;
                this.textFromTextBox4 = openFileDialog4.FileName;
                file4Choosen = true;
                CheckAllFilesChoosen();
            }
             
        }

        //OK-Button enablen wenn alles nötigen Dateien ausgewählt wurden
        public void CheckAllFilesChoosen()
        {
            if (file1Choosen == true && file2Choosen == true && file3Choosen == true && file4Choosen == true)
            {
                this.button5.Enabled = true;
            }
        }

        //OK-Result bei Button-Click setzen
        private void button5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        //Fenster schließen bei Button-Click
        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void openVacationFile(TextBox textBox, bool fileChoosen, String textFromTextBox, String fileDialogTitle)
        {
            OpenFileDialog openFileDialog3 = new OpenFileDialog();

            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Ferien");

            openFileDialog3.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Ferien";
            openFileDialog3.Title = fileDialogTitle;
            openFileDialog3.Filter = "Ferien|*.ics";
            openFileDialog3.Multiselect = false;

            DialogResult fileChoiceResult = openFileDialog3.ShowDialog();

            if (fileChoiceResult == DialogResult.OK)
            {
                textBox.Text = openFileDialog3.FileName;
                textFromTextBox = openFileDialog3.FileName;
                fileChoosen = true;
                CheckAllFilesChoosen();
            }
        }

        private void openHolidayFile(TextBox textBox, Boolean fileChoosen, String textFromTextBox, String fileDialogTitle)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();

            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Feiertage");

            openFileDialog2.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Feiertage";
            openFileDialog2.Title = fileDialogTitle;
            openFileDialog2.Filter = "Feiertage|*.csv";
            openFileDialog2.Multiselect = false;

            DialogResult fileChoiceResult = openFileDialog2.ShowDialog();

            if (fileChoiceResult == DialogResult.OK)
            {
                textBox.Text = openFileDialog2.FileName;
                textFromTextBox = openFileDialog2.FileName;
                fileChoosen = true;
                CheckAllFilesChoosen();
            }
        }
    }
}
