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
    public partial class SearchResultForm : Form
    {
        /// <summary>
        /// Erzeugt ein Objekt vom Typ SearchResultForm
        /// </summary>
        public SearchResultForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Registriert Mausklick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Weitersuchen_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Registriert Mausklick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_NichtWeitersuchen_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
