using System;
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
    /// <summary>
    /// Form zum konfigurieren und exportieren des PDF-Dokuments
    /// </summary>
    public partial class ExportPdfForm : Form
    {
        /// <summary>
        /// Erstellt eine neue Instanz von ExportPdfForm
        /// </summary>
        public ExportPdfForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            exportPdf();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Öffnet den SpeicherDialog um das PDF-Dokument zu erzeugen
        /// </summary>
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
