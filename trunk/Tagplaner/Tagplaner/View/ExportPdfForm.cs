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
            addSpecialitiesToCheckedListBox();
            addItemsToCombobox();

            if (checkedListBox1.Items.Count > 0)
            {
                button1.Enabled = true;
                comboBox1.Enabled = true;
                checkedListBox1.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                comboBox1.Enabled = false;
                checkedListBox1.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == Convert.ToInt32(comboBox1.Text)) {
                try
                {
                    exportPdf();
                }
                catch (Exception exp)
                {
                    FormInit.GetInstance().ShowMessageInStatusbar(exp.Message);
                }
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

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
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
                CPdfExporter pdfExporter = new CPdfExporter(
                    CreateIdentifierOfYearList(),
                    checkBox2.Checked,
                    CDatabase.GetInstance().GetAllTrainer());
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

            if (checkedListBox1.Items.Count == 1)
            {
                checkedListBox1.SetItemChecked(0, true);
            }
        }

        /// <summary>
        /// Fügt anhand der Anzahl der Jahrgänge die Items zur ComboBox "Anzahl der Jahrgänge" hinzu
        /// </summary>
        private void addItemsToCombobox()
        {
            if (checkedListBox1.Items.Count == 1)
            {
                comboBox1.Items.Add("1");
                comboBox1.SelectedIndex = 0;
            }
            else if (checkedListBox1.Items.Count > 1)
            {
                comboBox1.Items.Add("1");
                comboBox1.Items.Add("2");
                comboBox1.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Gibt eine Liste zurück mit den IdentifierOfYear der MSpeciality Instanzen die
        /// in der checkedListbox ausgewählt wurden
        /// </summary>
        /// <returns>Liste mit ausgewählten "IdentifierOfYear"</returns>
        private List<string> CreateIdentifierOfYearList()
        {
            List<string> list = new List<string>();

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        list.Add(checkedListBox1.Items[i].ToString());
                    }
                }

            return list;
        }
    }
}
