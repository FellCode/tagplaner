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
    public partial class FormInit : Form
    {
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
            showDateTimeAsTitle();

            // Init tabpages
            addTabPage(tagplanAnlegenUC, "Tagplan anlegen");
            addTabPage(tagplanBearbeitenUC, "Tagplan bearbeiten");
            addTabPage(seminarVerwaltenUC, "Seminar verwalten");
            addTabPage(raumVerwaltenUC, "Räume verwalten");
            addTabPage(trainerVerwaltenUC, "Trainer verwalten");
            addTabPage(statistikUC, "Statistiken");

            openTagplan(@"C:\Users\Alexander\Desktop\2015_2016.tp");
            tabControl1.SelectedIndex = 1;

        }

        /// <summary>
        /// Fügt ein UserControl mit einer neuen Tabpage zu tabControl1 zu
        /// </summary>
        /// <param name="userControl"></param>
        /// <param name="pageName"></param>
        private void addTabPage(UserControl userControl, string pageName) {
            userControl.Dock = DockStyle.Fill;
            userControl.BackColor = Color.White;

            // Init tabPage
            TabPage tabPage = new TabPage();
            tabPage.Text = pageName;
            tabPage.Controls.Add(userControl);

            // Add tabpage to tabControl1
            tabControl1.TabPages.Add(tabPage);
        }

        public void tabPageChange(int pageIndex)
        {
            tabControl1.SelectedIndex = pageIndex;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            showDateTimeAsTitle();
        }

        private void showDateTimeAsTitle() {
            string time = DateTime.Now.ToShortTimeString();
            string date = DateTime.Now.ToShortDateString();

            this.Text = "Tagplaner | " + date + " - " + time;
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
            if (!MCalendar.getInstance().Saved) {
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
                else if (dialogSaveResult == DialogResult.Cancel) {
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
                openTagplan(openFileDialog1.FileName);
                tabControl1.SelectedIndex = 1;

                tagplanBearbeitenUC.GetListView().Items.Clear();
                tagplanAnlegenUC.fillListViewWithDays(
                    MCalendar.getInstance().CalendarList, tagplanBearbeitenUC.GetListView());  
            }
        }

        private void tagplanSpeichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Tagplan");
            saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Tagplan";
            DialogResult fileSaveResult = saveFileDialog1.ShowDialog();

            if (fileSaveResult == DialogResult.OK && saveFileDialog1.FileName != null)
            {
                saveTagplan(saveFileDialog1.FileName);
            }
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //exportExcel();
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openExportPdfWindow();
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void openTagplan(string filename)
        {
            CSerialize serializer = new CSerialize();
            MCalendar calendarWithDays = (MCalendar)serializer.DeserializeObject(filename);
            MCalendar.SetInstance(calendarWithDays);
        }

        private void saveTagplan(string filename)
        {
            CSerialize serializer = new CSerialize();
            MCalendar calendarWithDays = MCalendar.getInstance();
            serializer.SerializeObject(calendarWithDays, filename);
            calendarWithDays.Saved = true;
        }

        private void openExportPdfWindow()
        {
            ExportPdfForm exportPdfForm = new ExportPdfForm();
            exportPdfForm.ShowDialog();
        }
    }
}
