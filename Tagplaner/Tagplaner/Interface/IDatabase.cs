using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Tagplaner
{
    interface IDatabase
    {
        bool ConnectDatabase();
        bool CloseDatabase();

        void FillTrainerCombobox(ComboBox combobox);
        void FillSeminarCombobox(ComboBox combobox);
        void FillFederalStateCombobox(ComboBox combobox);
        void FillLocationCombobox(ComboBox combobox);
        void FillRoomCombobox(ComboBox combobox);

        //Könnte man in Select, Update und delete aufteilen
        SQLiteDataReader ExecuteQuery(string query);

    }
}
