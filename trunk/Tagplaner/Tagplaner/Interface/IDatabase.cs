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
        /// <summary>
        /// Einfügen eines Seminars in die DB
        /// </summary>
        /// <param name="seminar"></param>
        /// <returns>bool</returns>
        bool InsertSeminar(MSeminar seminar);

        /// <summary>
        /// Einfügen eines Trainers in die DB
        /// </summary>
        /// <param name="trainer"></param>
        /// <returns>bool</returns>
        bool InsertTrainer(MTrainer trainer);

        /// <summary>
        /// Einfügen eines Raums in die DB
        /// </summary>
        /// <param name="room"></param>
        /// <returns>bool</returns>
        bool InsertRoom(MRoom room);

        /// <summary>
        /// Einfügen eines Seminarortes in die DB
        /// </summary>
        /// <param name="place"></param>
        /// <returns>bool</returns>
        bool InsertPlace(MPlace place);

        /// <summary>
        /// Einfügen eines Bundeslandes in die DB
        /// </summary>
        /// <param name="federalstate"></param>
        /// <returns>bool</returns>
        bool InsertFederalState(MFederalState federalstate);

        /// <summary>
        /// Füllt Combobox mit allen Trainern
        /// </summary>
        /// <param name="combobox"></param>
        void FillTrainerComboBox(ComboBox combobox);

        /// <summary>
        /// Füllt Combobox mit allen Seminaren
        /// </summary>
        /// <param name="combobox"></param>
        void FillSeminarComboBox(ComboBox combobox);

        /// <summary>
        /// Füllt Combobox mit allen Bundesländern
        /// </summary>
        /// <param name="combobox"></param>
        void FillFederalStateComboBox(ComboBox combobox);

        /// <summary>
        /// Füllt Combobox mit allen Seminarorten
        /// </summary>
        /// <param name="combobox"></param>
        void FillPlaceComboBox(ComboBox combobox);
        
        /// <summary>
        /// Füllt Combobox mit Räumen zu einem Seminarort id des Seminarorts mitgeben
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="location"></param>
        void FillRoomComboBox(ComboBox combobox, int location);

        void ThreadFillAll();

    }
}
