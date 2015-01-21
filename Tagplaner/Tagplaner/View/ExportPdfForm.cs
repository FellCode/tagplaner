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

        private void ExportPdfForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

            addSpecialitiesToCheckedListBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == Convert.ToInt32(comboBox1.Text)) {
                exportPdf();
            }
            else if (checkedListBox1.CheckedItems.Count < Convert.ToInt32(comboBox1.Text))
            {
                MessageBox.Show(MMessage.WARNING_SELECTED_TO_FEW_ENTRIES,
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(MMessage.WARNING_SELECTED_TO_MANY_ENTRIES,
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
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

                CPdfExporter pdfExporter = new CPdfExporter(
                    MCalendar.GetInstance(),
                    Convert.ToInt32(comboBox1.Text),
                    checkBox2.Checked,
                    trainerList);
                pdfExporter.ExportPdf(saveFileDialog1.FileName);
            }

            if (checkBox1.Checked && File.Exists(saveFileDialog1.FileName))
            {
                System.Diagnostics.Process.Start(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// Liest die SpecialityListe der MCalendar Instanz aus und fügt einen Eintrag pro
        /// Speciality in die checkedListbox ein (gleiche IdentifierOfYear werden gruppiert)
        /// </summary>
        private void addSpecialitiesToCheckedListBox()
        {
            checkedListBox1.Items.Clear();

            MCalendar calendar = MCalendar.GetInstance();
            for (int i = 0; i < MCalendar.GetInstance().Speciality.Count; i++)
            {
                MSpeciality currentSpecialityItem = calendar.Speciality[i];
                MSpeciality nextSpecialityItem = null;

                if (calendar.Speciality.Count - 1 > i)
                {
                    nextSpecialityItem = calendar.Speciality[i + 1];

                    if (!currentSpecialityItem.IdentifierOfYear.Equals(nextSpecialityItem.IdentifierOfYear))
                    {
                        checkedListBox1.Items.Add(currentSpecialityItem.IdentifierOfYear);
                    }
                }
                else
                {
                    checkedListBox1.Items.Add(currentSpecialityItem.IdentifierOfYear);
                }
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            this.Close();
        }

    }
}
