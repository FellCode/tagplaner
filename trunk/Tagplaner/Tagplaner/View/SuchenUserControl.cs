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

            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Ergebnis";
            dataGridView1.Columns[1].Name = "Zeile";
            dataGridView1.Columns[2].Name = "Spalte";

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button_Suchen_Click(object sender, EventArgs e)
        {
            FindInput();
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
                        if (CompareStrings(row.Cells[counter].Value.ToString(), textBox_Suchen.Text))
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
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            if (resultList.Count != 0)
            {
                int resultCounter = 0;

                foreach (String result in resultList)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, resultCounter].Value = resultList[resultCounter];
                    //+1 da die Werte mit denen aus dem Aufruf der Indizies der GridView übereinstimmen müssen
                    dataGridView1[1, resultCounter].Value = rowList[resultCounter] + 1;
                    dataGridView1[2, resultCounter].Value = columnList[resultCounter] + 1;

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
        /// <returns></returns>
        public bool CompareStrings(String searchString, String compareString)
        {
            if (searchString.ToUpper().Trim().Equals(compareString.ToUpper().Trim()) || String.Compare(searchString.ToUpper().Trim(), 0, compareString.ToUpper().Trim(), 0, textBox_Suchen.Text.Length) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            choosenRow = e.RowIndex;

            if (dataGridView1[2, choosenRow].Value != null && dataGridView1[1, choosenRow].Value != null)
            {
                dGVFromBearbeiten.CurrentCell = dGVFromBearbeiten[Convert.ToInt32(dataGridView1[2, choosenRow].Value) - 1, Convert.ToInt32(dataGridView1[1, choosenRow].Value) - 1];
                dGVFromBearbeiten.BeginEdit(true);
                formInit.TabPageChange(1);
            }
        }
    }
}
