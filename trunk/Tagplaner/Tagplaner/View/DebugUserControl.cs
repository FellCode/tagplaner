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
    /// <summary>
    /// UserControl für die Anzeige von Debug Informationen, diese Klasse ist als Singleton implementiert
    /// um Lognachrichten aus jeder anderen Klasse einfach schreiben zu können
    /// </summary>
    public partial class DebugUserControl : UserControl
    {
        private static DebugUserControl instance;

        private DebugUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Liefert eine Instanz von DebugUserControl zurück
        /// </summary>
        /// <returns>Instanz von DebugUserControl</returns>
        public static DebugUserControl GetInstance()
        {
            if (instance == null)
            {
                instance = new DebugUserControl();
            }

            return instance;
        }

        /// <summary>
        /// Erzeugt einen Logeintrag mit Datum und Uhrzeit in der ListBox
        /// </summary>
        /// <param name="debugMessage"></param>
        public void AddDebugMessage(string debugMessage)
        {
            DateTime datetime = DateTime.Now;
            this.listBox1.Items.Add("[" + datetime.ToString() + "] - " + debugMessage);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
