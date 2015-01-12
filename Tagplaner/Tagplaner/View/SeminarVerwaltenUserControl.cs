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
    public partial class SeminarVerwaltenUserControl : UserControl
    {
        CDatabase db = new CDatabase();
        
        public SeminarVerwaltenUserControl()
        {
            db.CreateDB();
            db.FillDB();
            InitializeComponent();
            
         db.FillSeminarCombobox(seminarBox);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void seminarBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

      
    }
}
