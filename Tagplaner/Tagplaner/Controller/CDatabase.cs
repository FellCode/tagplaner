using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Tagplaner
{
    public class CDatabase : IDatabase
    {
        private string url="TagplanerDatabase.sqlite";
        private SQLiteConnection m_dbConnection;
        private SQLiteCommand m_dbCommand;

        public bool ConnectDatabase()
        {

            m_dbConnection = new SQLiteConnection("Data Source=" + url);
            try
            {
                m_dbConnection.Open();
                m_dbCommand = new SQLiteCommand("PRAGMA foreign_keys=on", m_dbConnection);
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

        public SQLiteDataReader ExecuteQuery(string query)
        {
            m_dbCommand.CommandText = query;
            SQLiteDataReader reader = m_dbCommand.ExecuteReader();
            return reader;
        }
        
        
  

        //Für die erst Installation der Datenbank
        public void CreateDB()
        {
            SQLiteConnection.CreateFile(url);

            SQLiteConnection connect = new SQLiteConnection("Data Source="+url);
            
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

            command.CommandText = "CREATE TABLE bundesland("
                                + "bundesland_id integer,"
                                + "name varchar(50),"
                                + "kuerzel varchar(10),"
                                + "primary key(bundesland_id))";
            command.ExecuteNonQuery();

            connect.Close();
        }
        //zum ersten befuellen der DB
        public void FillDB ()
        {
            FillSeminar();
            FillTrainer();
            FillBundesland();
        }

        private void FillBundesland()
        {
            SQLiteConnection connect;
            connect = new SQLiteConnection("Data Source=" + url);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand("PRAGMA foreign_keys=ON", connect);
            command.ExecuteNonQuery();

            command.CommandText = "insert into bundesland values(1,\"Nordrehin-Westfalen\",\"NRW\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into bundesland values(2,\"Hessen\",\"HE\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into bundesland values(3,\"Bayern\",\"BY\")";
            command.ExecuteNonQuery();

            connect.Close();
        }

        private void FillSeminar()
        {
            SQLiteConnection connect;
            connect = new SQLiteConnection("Data Source=" + url);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand("PRAGMA foreign_keys=ON", connect);
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(1,\"JavaScript\",\"Webseiten mit Programmfunktionalität steuern\",\"DAJSCRI\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(2,\"Objektorientierte Anforderungsanalyse\",\"Statiische und dynamische Klassenmodellierung\",\"DAOOANW\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(3,\"IT-Sicherheitskonzepte\",null,\"DASIKON\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(4,\"C# Grundlagen\",null,\"DACSHGL\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(5,\"Grundlagen der Informationstechnologie\",null,\"DAGLITE\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(6,\"Grundlagen Netze\",null,\"DAGLNET\",\"Laptop, Switch, Hub\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(7,\"Java Grundlagen\",\"Grundlagen\",\"DAJAVAG\",\"Laptop, Eclipse\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(8,\"Grundlagen der Programmierung\",null,\"DAGLPRO\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(9,\"C# Vertiefung\",null,\"DACSHVT\",\"Laptop, SharpDevelop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(10,\"Betriebssysteme (Linux und Windows)\",null,\"DABESYS\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(11,\"XML Einführung\",\"Die Auszeichnungssprache zur strukturellen Beschreibung von Web-Dokumenten\",\"DAXMLWD\",\"Laptop, XMLEditor\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(12,\"Der Business Analyst  in der Anforderungsanalyse\",null,\"BUSAO-R (3)\",\"Laptop, Visio\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(13,\"Netzwerkintegration\",null,\"DANWINT\",\"Laptop, Server, VMWare\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(14,\"Systemüberwachung\",null,\"DASYSUB\",\"Laptop, Server, VMWare\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(15,\"HTML Grundlagen\",null,\"DAIHTML\",\"Laptop, HTML-Kit\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(16,\"MySQL Grundlagen\",null,\"DAMYSQLGL-ALT\",\"Laptop, MySQL-Community Edition\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(17,\"Projektaufgabe Java Vertiefung\",\"Anhängervermietung\",\" PROJVT\",\"Laptop, Eclipse\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(18,\"Projektaufgabe C# Vertiefung\",null,\"DAPROCSV\",\"Laptop, SharpDevelop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar values(19,\"Vertiefung objektorientierte Anforderungsanalyse für Business Analysten\",null,\"DAVOOBA\",\"Laptop\")";
            command.ExecuteNonQuery();

            connect.Close();
        }

        private void FillTrainer()
        {
            SQLiteConnection connect;
            connect = new SQLiteConnection("Data Source=" + url);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand("PRAGMA foreign_keys=ON", connect);
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer values(1,\"Thomas\",\"Bender\",\"TBE\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer values(2,\"Nico\",\"Carlsen\",null)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer values(3,\"Bernd\",\"Reimann\",null)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer values(4,\"Carsten\",\"Lenz\",\"\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer values(5,\"Thomas\",\"Adameit\",\"\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer values(6,\"Katrin\",\"Klein\",null)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer values(7,\"Joerg\",\"Martin\",\"JMA\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer values(8,\"Rolf\",\"Schmidt\",\"RSC\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer values(9,\"Markus\",\"Ruecker\",\"MRK\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer values(10,\"Markus\",\"Ruecker\",\"MRK\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer values(11,\"Christina\",\"von Ziegsar\",\"\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer values(12,\"Bernie\",\"Cornwell\",\"\")";
            command.ExecuteNonQuery();

            connect.Close();
        }

        public void FillTrainerCombobox(ComboBox combobox)
        {
            Dictionary<string, string> trainer;

            trainer = new Dictionary<string, string>();

            ConnectDatabase();

            SQLiteDataReader reader = ExecuteQuery("select trainer_id, vorname, nachname from trainer");

            while(reader.Read())
            {
                trainer.Add(reader["trainer_id"].ToString(), reader["vorname"].ToString() + reader["nachname"].ToString());
            }

            BindingSource trainersource = new BindingSource();

            trainersource.DataSource = trainer;
            combobox.DataSource = trainersource;
            combobox.DisplayMember = "Value";
            combobox.ValueMember = "Key";

            CloseDatabase();
        }

        public void FillSeminarCombobox(ComboBox combobox)
        {
            Dictionary<string, string> seminar;

            seminar = new Dictionary<string, string>();

            ConnectDatabase();

            SQLiteDataReader reader = ExecuteQuery("select seminar_id, titel from seminar");

            while (reader.Read())
            {
                seminar.Add(reader["seminar_id"].ToString(), reader["titel"].ToString());
            }

            BindingSource seminarsource = new BindingSource();

            seminarsource.DataSource = seminar;
            combobox.DataSource = seminarsource;
            combobox.DisplayMember = "Value";
            combobox.ValueMember = "Key";

            CloseDatabase();
        }

        public void FillFederalStateCombobox(ComboBox combobox)
        {
            Dictionary<string, string> federalstate;

            federalstate = new Dictionary<string, string>();

            ConnectDatabase();

            SQLiteDataReader reader = ExecuteQuery("select bundesland_id, name from bundesland");

            while (reader.Read())
            {
                federalstate.Add(reader["bundesland_id"].ToString(), reader["name"].ToString());
            }

            BindingSource federalstatesource = new BindingSource();

            federalstatesource.DataSource = federalstate;
            combobox.DataSource = federalstatesource;
            combobox.DisplayMember = "Value";
            combobox.ValueMember = "Key";

            CloseDatabase();
        }

    }
}
