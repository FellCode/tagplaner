﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tagplaner
{
    public partial class ExportPdfForm : Form
    {
        public ExportPdfForm()
        {
            InitializeComponent();
        }

        private void ExportPdfForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            exportPdf();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exportPdf()
        {
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                List<MTrainer> trainerList = new List<MTrainer>();
                trainerList.Add(new MTrainer("Alexander", "Theis", "AT", true, true));

                CPdfExporter pdfExporter = new CPdfExporter(MCalendar.getInstance(), trainerList);
                pdfExporter.ExportPdf(saveFileDialog1.FileName);
            }

            if (checkBox1.Checked && File.Exists(saveFileDialog1.FileName))
            {
                this.Close();
                System.Diagnostics.Process.Start(saveFileDialog1.FileName);
            }

            this.Close();
        }
    }
}