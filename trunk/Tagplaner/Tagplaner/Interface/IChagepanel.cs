using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tagplaner
{
    public interface IChangepanel
    {


        void ChangeCalendarEntry(MCalendarEntry calendarentry);

        void PasteEntry();

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
