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

        bool InsertSeminar(string titel, string untertitel, string kuerzel, string technick);
        bool InsertTrainer(string vorname, string nachname, string kuerzel, string intern);
        bool InsertRoom(string raumnummer, string fk_seminarort_id);
        bool InsertLocation(string ort, string ansprechpartner, string fk_bundesland_id);
        bool InsertFederelState(string name, string kuerzel);


        void FillTrainerCombobox(ComboBox combobox);
        void FillSeminarCombobox(ComboBox combobox);
        void FillFederalStateCombobox(ComboBox combobox);
        void FillLocationCombobox(ComboBox combobox);
        //id des Seminarorts mitgeben
        void FillRoomCombobox(ComboBox combobox, int location);

        //Könnte man in Select, Update und delete aufteilen
        SQLiteDataReader ExecuteQuery(string query);

    }
}
