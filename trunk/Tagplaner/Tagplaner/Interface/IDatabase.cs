using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Tagplaner
{
    public interface IDatabase
    {
        bool InsertSeminar(MSeminar seminar);
        bool InsertTrainer(MTrainer trainer);
        bool InsertRoom(MRoom room);
        bool InsertPlace(MPlace place);
        bool InsertFederalState(MFederalState federalstate);


        void FillTrainerComboBox(ComboBox combobox);
        void FillSeminarComboBox(ComboBox combobox);
        void FillFederalStateComboBox(ComboBox combobox);
        void FillPlaceComboBox(ComboBox combobox);
        //id des Seminarorts mitgeben
        void FillRoomComboBox(ComboBox combobox, int location);

        void ThreadFillAll();

    }
}
