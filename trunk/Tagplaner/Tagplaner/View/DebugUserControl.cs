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
        }

        public void addDebugMessage(string debugMessage)
        {
            this.listBox1.Items.Add(debugMessage);
        }
    }
}
