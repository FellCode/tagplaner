using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Tagplaner
{
    public class CDatabase : IDatabase
    {
        private SQLiteDataReader result;
        private string url="TagplanerDatabase.sqlite";
        private SQLiteConnection m_dbConnection;
        private SQLiteCommand command;

        public bool ConnectDatabase()
        {

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + url);
            try
            {
                m_dbConnection.Open();
                command = new SQLiteCommand("PRAGMA foreign_keys=on", m_dbConnection);
                return true;
            }
            catch(SQLiteException)
            {
                return false;
            }
            
        }

        public bool CloseDatabase()
        {

            try
            {
                m_dbConnection.Close();
                return true;
            }
            catch(SQLiteException)
            {
                return false;
            }
            
        }

        public SQLiteDataReader Executequery(string query)
        {
            command.CommandText = query;
            SQLiteDataReader reader = command.ExecuteReader();
            return reader;
        }
        
        
        
        
        


        //Für die erst Instalation der Datenbank
        public void createdb()
        {
            SQLiteConnection.CreateFile(url);

            SQLiteConnection connect;
            connect = new SQLiteConnection("Data Source="+url+"Version=3;");
            connect.Open();

            string sql = "CREATE TABLE trainer("
                        +"trainer_id integer,"
                        +"vorname varchar(100),"
                        +"nachname varchar(100),"
                        +"kuerzel varchar(5),"
                        +"primary key(trainer_id))";

            SQLiteCommand command = new SQLiteCommand(sql, connect);
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE seminarort("
                                    + "seminarort_id integer,"
                                    + "ort varchar(100),"
                                    + "ansprechpartner varchar(100),"
                                    + "primary key(seminarort_id))";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE raum("
                                +"raum_id integer,"
                                +"raumnummer varchar(50),"
                                +"fk_seminarort_id integer,"
                                +"primary key(raum_id),"
                                +"foreign key(fk_seminarort_id) references seminarort(seminarort_id))";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE Kalendertag ("
                                +"kalendertag_id integer,"
                                +"datum date,"
                                +"ferien_name varchar(100),"
                                +"kalenderwoche integer,"
                                +"primary key (kalendertag_id))";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE fachrichtung("
                                +"fachrichtung_id integer,"
                                +"bezeichnung varchar(100),"
                                +"ausbildungsjahr varchar(15),"
                                +"bundesland varchar(50),"
                                +"primary key(fachrichtung_id))";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE seminar("
                                +"seminar_id integer,"
                                +"titel varchar(50),"
                                +"untertitel varchar(100),"
                                +"kuerzel varchar(20),"
                                +"technik varchar(200),"
                                +"primary key(seminar_id))";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE seminartag("
                                +"seminartag_id integer,"
                                +"fk_trainer_id integer null,"
                                +"fk_cotrainer_id integer null,"
                                +"fk_seminar_id integer null,"
                                +"primary key(seminartag_id),"
                                +"foreign key(fk_trainer_id) references trainer(trainer_id),"
                                +"foreign key(fk_cotrainer_id) references trainer(trainer_id))";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE se_2_ra("
                                +"fk_seminar_id integer,"
                                +"fk_raum_id integer,"
                                +"gruppenraum integer,"
                                +"primary key(fk_seminar_id, fk_raum_id),"
                                +"foreign key(fk_seminar_id) references seminar(seminar_id),"
                                +"foreign key(fk_raum_id) references raum(raum_id))";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE ka_2_se_2_fa("
                                +"fk_kalendertag_id integer,"
                                +"fk_fachrichtung_id integer,"
                                +"fk_seminartag_id integer null,"
                                +"typ varchar(50),"
                                +"bemerkung varchar(200),"
                                +"primary key(fk_kalendertag_id,fk_fachrichtung_id),"
                                +"foreign key(fk_kalendertag_id) references kalendertag(kalendertag_id),"
                                +"foreign key(fk_fachrichtung_id) references fachrichtung(fachrichtung_id),"
                                +"foreign key(fk_seminartag_id) references seminartag(seminartag_id))";
            command.ExecuteNonQuery();
            connect.Close();
     
            



        }

    }
}
