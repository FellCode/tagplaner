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
    public partial class DebugUserControl : UserControl
    {
        public DebugUserControl()
        {
            InitializeComponent();
            addDebugMessage("Hallo Deine Mutter");
        }

        public void addDebugMessage(string debugMessage)
        {
            DateTime datetime = DateTime.Now;
            this.listBox1.Items.Add("[" + datetime.ToString() + "] - " + debugMessage);
        }
    }
}
