using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tagplaner
{
    interface IChangepanel
    {


        void ChangeCalendarEntry(MCalendarEntry calendarentry, ComboBox tagartb, ComboBox seminarb,
            ComboBox trainerb, ComboBox cotrainerb, ComboBox ortb, ComboBox raumb, TextBox kommentarb);

        void PasteEntry(ComboBox tagartb, ComboBox seminarb,
            ComboBox trainerb, ComboBox cotrainerb, ComboBox ortb, ComboBox raumb, TextBox kommentarb);

        bool FillDayType(ComboBox Tagart);

        bool FillSeminar(ComboBox Seminar);
        
        bool FillTrainer(ComboBox Trainer);
        
        bool FillCoTrainer(ComboBox CoTrainer);

        bool FillLocation(ComboBox Ort);

        bool FillRoom(ComboBox Ort, ComboBox Raum);
        void UpdateCalenderEntry(ComboBox Tagart, TextBox Kommentar);
        void ChangeVisibility(ComboBox Tagart, Panel Seminarpanel);

    }
}
