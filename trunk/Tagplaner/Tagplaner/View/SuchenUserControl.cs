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
    public partial class SuchenUserControl : UserControl
    {
        private int choosenRow;

        MCalendar calendar;
        List<MCalendarDay> list;
        DataGridView dGVFromBearbeiten;

        TagplanBearbeitenUserControl tagplanBearbeitenUC;
        FormInit formInit;
        SearchResultForm searchResultForm;

        List<String> resultList;
        List<int> columnList;
        List<int> rowList;

        /// <summary>
        /// Erzeugt ein Objekt vom Typ SuchenUserControl
        /// </summary>
        /// <param name="tagplanBearbeitenUC">Objekt vom Typ TagplanBearbeitenUserControl</param>
        /// <param name="formInit">Ein Objekt vom Typ FormInit</param>
        public SuchenUserControl(TagplanBearbeitenUserControl tagplanBearbeitenUC, FormInit formInit)
        {
            InitializeComponent();

            this.tagplanBearbeitenUC = tagplanBearbeitenUC;
            this.formInit = formInit;
            searchResultForm = new SearchResultForm();

            dGVFromBearbeiten = tagplanBearbeitenUC.GetDGV();
            resultList = new List<string>();
            columnList = new List<int>();
            rowList = new List<int>();

            dataGridView_Suchergebnisse.ColumnCount = 3;
            dataGridView_Suchergebnisse.Columns[0].Name = "Ergebnis";
            dataGridView_Suchergebnisse.Columns[1].Name = "Zeile";
            dataGridView_Suchergebnisse.Columns[2].Name = "Spalte";

            dataGridView_Suchergebnisse.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_Suchergebnisse.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_Suchergebnisse.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /// <summary>
        /// Überprüft ob der eingegebene Suchbegriff im Tagplan enthalten ist
        /// </summary>
        /// <returns></returns>
        public bool FindInput()
        {
            foreach (DataGridViewRow row in dGVFromBearbeiten.Rows)
            {
                for (int counter = 0; counter < dGVFromBearbeiten.ColumnCount; counter++)
                {
                    if (row.Cells[counter].Value != null)
                    {
                        if (CompareStrings(textBox_Suchen.Text, row.Cells[counter].Value.ToString()))
                        {
                            resultList.Add(row.Cells[counter].Value.ToString());
                            columnList.Add(counter);
                            rowList.Add(row.Index);

                            dGVFromBearbeiten.CurrentCell = dGVFromBearbeiten[counter, row.Index];
                            dGVFromBearbeiten.BeginEdit(true);
                            formInit.TabPageChange(1);
                            DialogResult dr = searchResultForm.ShowDialog();
                            if (dr == DialogResult.Cancel)
                            {
                                FillResultGrid();
                                return false;
                            }
                        }
                    }
                }
            }
            formInit.ShowMessageInStatusbar(MMessage.INFO_SEARCH_COMPLETE);
            //Das ResultGrid wird mit den Suchergebnissen und deren Position gefüllt
            FillResultGrid();
            return true;
        }

        /// <summary>
        /// Füllt die GridView mit Suchergebnissen
        /// </summary>
        public void FillResultGrid()
        {
            //DataGridView muss vor dem erneuten Befüllen geleert werden
            dataGridView_Suchergebnisse.DataSource = null;
            dataGridView_Suchergebnisse.Rows.Clear();

            if (resultList.Count != 0)
            {
                int resultCounter = 0;

                foreach (String result in resultList)
                {
                    dataGridView_Suchergebnisse.Rows.Add();
                    dataGridView_Suchergebnisse[0, resultCounter].Value = resultList[resultCounter];
                    //+1 da die Werte mit denen aus dem Aufruf der Indizies der GridView übereinstimmen müssen
                    dataGridView_Suchergebnisse[1, resultCounter].Value = rowList[resultCounter] + 1;
                    dataGridView_Suchergebnisse[2, resultCounter].Value = columnList[resultCounter] + 1;

                    resultCounter++;
                }
                //Ergebnislisten werden für die nächste Suche geleert
                resultList.Clear();
                columnList.Clear();
                rowList.Clear();

                //DataGridView wird angezeigt wenn Ergebnisse vorhanden sind
                groupBox_Suchergebnisse.Text = "Suchergebnisse für " + textBox_Suchen.Text;
                groupBox_Suchergebnisse.Visible = true;
            }
            else
            {
                //DataGridView wird ausgeblendet wenn keine Ergebnisse vorhanden sind
                formInit.ShowMessageInStatusbar(MMessage.INFO_NO_RESULT_FOUND);
                groupBox_Suchergebnisse.Visible = false;
            }
        }

        /// <summary>
        /// Vergleicht zwei Zeichenketten miteinander
        /// </summary>
        /// <param name="searchString">Erste Zeichenkette</param>
        /// <param name="compareString">Zweite Zeichenkette</param>
        /// <returns>Wahrheitswert ob die beiden Zeichenketten übereinstimmen</returns>
        public bool CompareStrings(String searchString, String compareString)
        {
            if (!String.IsNullOrEmpty(searchString.Trim()) && (searchString.ToUpper().Trim().Equals(compareString.ToUpper().Trim()) || String.Compare(searchString.ToUpper().Trim(), 0, compareString.ToUpper().Trim(), 0, textBox_Suchen.Text.Length) == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Registriert Mausklick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dataGridView_Suchergebnisse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            choosenRow = e.RowIndex;

            if (dataGridView_Suchergebnisse[2, choosenRow].Value != null && dataGridView_Suchergebnisse[1, choosenRow].Value != null)
            {
                dGVFromBearbeiten.CurrentCell = dGVFromBearbeiten[Convert.ToInt32(dataGridView_Suchergebnisse[2, choosenRow].Value) - 1, Convert.ToInt32(dataGridView_Suchergebnisse[1, choosenRow].Value) - 1];
                dGVFromBearbeiten.BeginEdit(true);
                formInit.TabPageChange(1);
            }
        }

        /// <summary>
        /// Registriert Tastendruck
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Suchbegriff_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                FindInput();
            }
        }

        /// <summary>
        /// Registriert Mausklick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Suchen_Click(object sender, EventArgs e)
        {
            FindInput();
        }
    }
}
