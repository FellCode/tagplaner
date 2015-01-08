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
    public partial class TagplanAnlegenUserControl : UserControl
    {
        private FormInit formInit;

        public TagplanAnlegenUserControl(FormInit formInit)
        {
            this.formInit = formInit;    
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TagplanAnlegenUserControl_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            System.IO.Directory.CreateDirectory(@"C:\Tagplan");

            saveFileDialog1.InitialDirectory = @"C:\Tagplan";
            saveFileDialog1.Filter = "Tagplan|*.tp";
            saveFileDialog1.Title = "Tagplan abspeichern";
            saveFileDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\Tagplan";
            openFileDialog1.Title = "Tagplan öffnen";
            openFileDialog1.Filter = "Tagplan|*.tp";
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
            string[] result = openFileDialog1.FileNames;

            foreach (string y in result)
            {
                System.IO.StreamReader sr = new
               System.IO.StreamReader(y);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nextTabPage();   
        }

        public void nextTabPage()
        {
            formInit.tabPageChange(1);
        }
    }
}
