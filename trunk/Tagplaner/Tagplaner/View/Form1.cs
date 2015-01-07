using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tagplaner
{
    public partial class SaveLoad : Form
    {
        public SaveLoad()
        {
            InitializeComponent();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            System.IO.Directory.CreateDirectory(@"C:\Tagplan");

            saveFileDialog1.InitialDirectory = @"C:\Tagplan";
            saveFileDialog1.Filter = "Tagplan|*.tp";
            saveFileDialog1.Title = "Tagplan abspeichern";
            saveFileDialog1.ShowDialog();
        }

        private void button_open_Click(object sender, EventArgs e)
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
    }
}
