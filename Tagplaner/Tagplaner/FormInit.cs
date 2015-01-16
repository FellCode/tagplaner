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
using Tagplaner.View;

namespace Tagplaner
{
    public partial class FormInit : Form
    {
        Boolean tabsAlreadyAdded = false;

        TagplanAnlegenUserControl tagplanAnlegenUC;
        TagplanBearbeitenUserControl tagplanBearbeitenUC;
        SeminarVerwaltenUserControl seminarVerwaltenUC;
        StatistikUserControl statistikUC;
        RaumVerwaltenUserControl raumVerwaltenUC;
        TrainerVerwaltenUserControl trainerVerwaltenUC;

        public FormInit()
        {
            InitializeComponent();
            tagplanBearbeitenUC = new TagplanBearbeitenUserControl();
            tagplanAnlegenUC = new TagplanAnlegenUserControl(this, tagplanBearbeitenUC);
            seminarVerwaltenUC = new SeminarVerwaltenUserControl();
            statistikUC = new StatistikUserControl();
            raumVerwaltenUC = new RaumVerwaltenUserControl();
            trainerVerwaltenUC = new TrainerVerwaltenUserControl();
        }

        private void Init_Load(object sender, EventArgs e)
        {
            // Show date and time
            ShowDateTimeAsTitle();

            // Init tabpages
            AddTabPage(tagplanAnlegenUC, "Tagplan anlegen", 0);
            AddTabPage(seminarVerwaltenUC, "Seminar verwalten", 1);
            AddTabPage(raumVerwaltenUC, "Räume verwalten", 2);
            AddTabPage(trainerVerwaltenUC, "Trainer verwalten", 3);
            AddTabPage(DebugUserControl.GetInstance(), "Debug", 4);
        }

        /// <summary>
        /// Fügt ein UserControl mit einer neuen Tabpage zu tabControl1 zu
        /// </summary>
        /// <param name="userControl">Instanz des UserControls das hinzugefügt werden soll</param>
        /// <param name="pageName">Anzeigetext für die tabPage</param>
        /// <param name="tabPosition">Position im tabControl</param>
        private void AddTabPage(UserControl userControl, string pageName, int tabPosition)
        {
            userControl.Dock = DockStyle.Fill;
            userControl.BackColor = Color.White;

            // Init tabPage
            TabPage tabPage = new TabPage();
            tabPage.Text = pageName;
            tabPage.Controls.Add(userControl);

            // Add tabpage to tabControl1
            tabControl1.TabPages.Insert(tabPosition, tabPage);

        }

        /// <summary>
        /// Wechselt zur angegebenen Seite im tabControl1
        /// </summary>
        /// <param name="pageIndex">Index von der Seite zu der gewechselt werden soll</param>
        public void TabPageChange(int pageIndex)
        {
            tabControl1.SelectedIndex = pageIndex;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowDateTimeAsTitle();
        }

        /// <summary>
        /// Zeigt Uhrzeit und Datum, sowie die Information über die letzte Speicherzeit des Tagplans
        /// </summary>
        private void ShowDateTimeAsTitle()
        {
            string time = DateTime.Now.ToShortTimeString();
            string date = DateTime.Now.ToShortDateString();


            if (MCalendar.getInstance().LastModified.Year.ToString().Equals("1"))
            {
                this.Text = "Tagplaner | " + date + " - " + time;
            }
            else
            {
                this.Text = "Tagplaner | " + date + " - " + time + " | stand vom: " + MCalendar.getInstance().LastModified.ToShortDateString() + " " + MCalendar.getInstance().LastModified.ToShortTimeString();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pageIndex = tabControl1.SelectedIndex;

            // Statistik ListView aktualisieren wenn die entsprechende TabPage ausgewählt wird
            if (tabControl1.TabPages[pageIndex].Text.Equals("Statistiken"))
            {
                statistikUC.RefreshListView();
            }
        }

        private void FormInit_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Prüfen, ob der Tagplan bereits gespeichert wurde
            if (!MCalendar.getInstance().Saved)
            {
                e.Cancel = true;

                // Messagebox mit Speicher-Hinweis anzeigen
                DialogResult dialogSaveResult = MessageBox.Show(
                MMessage.REQUEST_FILE_SAVE, "Tagplaner", MessageBoxButtons.YesNoCancel);

                // Wenn Ja gedrückt wurde SaveDialog anzeigen
                if (dialogSaveResult == DialogResult.Yes)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                    System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Tagplan");

                    saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Tagplan";
                    saveFileDialog1.Filter = "Tagplan|*.tp";
                    saveFileDialog1.Title = "Tagplan abspeichern";

                    DialogResult fileSaveResult = saveFileDialog1.ShowDialog();
                    if (fileSaveResult == DialogResult.OK && saveFileDialog1.FileName != null)
                    {
                        CSerialize serialize = new CSerialize();
                        MCalendar calendarWithDays = MCalendar.getInstance();
                        serialize.SerializeObject(calendarWithDays, saveFileDialog1.FileName);
                        e.Cancel = false;
                    }
                }
                else if (dialogSaveResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }

        private void tagplanÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Tagplan");
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Tagplan";

            DialogResult fileChoiceResult = openFileDialog1.ShowDialog();

            if (fileChoiceResult == DialogResult.OK)
            {
                OpenTagplan(openFileDialog1.FileName);
                ShowDateTimeAsTitle();
                TabPageChange(1);
            }
        }

        private void tagplanSpeichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Tagplan");
            saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Tagplan";
            DialogResult fileSaveResult = saveFileDialog1.ShowDialog();

            if (fileSaveResult == DialogResult.OK && saveFileDialog1.FileName != null)
            {
                SaveTagplan(saveFileDialog1.FileName);
            }
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //exportExcel();
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenExportPdfWindow();
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        /// <summary>
        /// Öffnet eine Tagplan Datei
        /// </summary>
        /// <param name="filename">Dateipfad und Name zur Datei die geöffnet werden soll</param>
        private void OpenTagplan(string filename)
        {
            CSerialize serializer = new CSerialize();
            MCalendar calendarWithDays = (MCalendar)serializer.DeserializeObject(filename);
            MCalendar.SetInstance(calendarWithDays);

            //Füllt die GridView mit Daten
            tagplanBearbeitenUC.CreateDataGridViews(MCalendar.getInstance().Speciality.Count());
            tagplanBearbeitenUC.FillGrids(MCalendar.getInstance().CalendarList);
            EnableBearbeitenStatistikTabPage();
            tagplanAnlegenUC.nextTabPage();
        }

        /// <summary>
        /// Speichert die aktuelle Tagplan Datei
        /// </summary>
        /// <param name="filename">Speicherort und Dateiname für die zu Speichernden Datei</param>
        private void SaveTagplan(string filename)
        {
            CSerialize serializer = new CSerialize();
            MCalendar.getInstance().LastModified = DateTime.Now;
            serializer.SerializeObject(MCalendar.getInstance(), filename);
            MCalendar.getInstance().Saved = true;
        }

        /// <summary>
        /// Zeigt das Fenster zum Exportieren von PDF-Dokumenten an
        /// </summary>
        private void OpenExportPdfWindow()
        {
            ExportPdfForm exportPdfForm = new ExportPdfForm();
            exportPdfForm.ShowDialog();
        }

        private void FormInit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Fügt die UnserControls TagplanBearbeiten und Statistiken zum tabControl1 hinzu
        /// </summary>
        public void EnableBearbeitenStatistikTabPage()
        {
            if (!tabsAlreadyAdded)
            {
                AddTabPage(tagplanBearbeitenUC, "Tagplan bearbeiten", 1);
                AddTabPage(statistikUC, "Statistik", 5);
            }
            tabsAlreadyAdded = true;
        }
    }
}
