using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Threading;

namespace Tagplaner
{
    public class CDatabase : IDatabase
    {
        private static CDatabase database;
        private string url = "TagplanerDatabase.sqlite";
        private SQLiteConnection m_dbConnection;
        private SQLiteCommand m_dbCommand;
        private Dictionary<int, MTrainer> AllTrainer = new Dictionary<int, MTrainer>();
        private Dictionary<int, MFederalState> AllFederalState = new Dictionary<int, MFederalState>();
        private Dictionary<int, MPlace> AllPlace = new Dictionary<int, MPlace>();
        private Dictionary<int, MRoom> AllRoom = new Dictionary<int, MRoom>();
        private Dictionary<int, MSeminar> AllSeminar = new Dictionary<int, MSeminar>();

        // Die Verbindungsmethode zur Datenbank

        private bool ConnectDatabase()
        {

            m_dbConnection = new SQLiteConnection("Data Source=" + url);
            try
            {
                m_dbConnection.Open();
                m_dbCommand = new SQLiteCommand("PRAGMA foreign_keys=on", m_dbConnection);
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }

        }

        //Die Methode zum schließen der verbindung zu Datenbank

        private bool CloseDatabase()
        {

            try
            {
                m_dbConnection.Close();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }

        }

        //Die Methode zum ausführen einer SQL Query

        private SQLiteDataReader ExecuteQuery(string query)
        {
            m_dbCommand.CommandText = query;
            SQLiteDataReader reader = m_dbCommand.ExecuteReader();
            return reader;
        }

        private void ExecuteNonQuery(string query)
        {
            m_dbCommand.CommandText = query;
            m_dbCommand.ExecuteNonQuery();
        }

        // Methode zum ermitteln der nächsten ID einer Tabelle
        private int nextId(string tabelle)
        {
            int i = 0;
            m_dbCommand.CommandText = "select max(" + tabelle + "_id) as id from " + tabelle;
            SQLiteDataReader reader = m_dbCommand.ExecuteReader();

            while (reader.Read())
            {
                i = Convert.ToInt32(reader["id"].ToString());
            }
            reader.Close();
            return i + 1;

        }


        #region insert
        [Obsolete("Bitte nutz InsertSeminar(MSeminar)")]
        public bool InsertSeminar(string titel, string untertitel, string kuerzel, string technik)
        {
            ConnectDatabase();
            int seminarId = nextId("seminar");
            try
            {
                ExecuteNonQuery("insert into seminar(titel,untertitel,kuerzel,technik) values("
                    + seminarId
                    + ",\"" + titel + "\""
                    + ",\"" + untertitel + "\""
                    + ",\"" + kuerzel + "\""
                    + ",\"" + technik + "\""
                    + ")");
                CloseDatabase();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }
        [Obsolete("Bitte nutz InsertRoom(MRoom)")]
        public bool InsertRoom(string raumnummer, string fk_seminarort_id)
        {
            ConnectDatabase();
            int raum_id = nextId("raum");
            try
            {
                m_dbCommand.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(" + raum_id
                                        + ",\"" + raumnummer + "\""
                                        + "," + fk_seminarort_id
                                        + ")";
                m_dbCommand.ExecuteNonQuery();
                CloseDatabase();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }
        [Obsolete("Bitte nutz InsertPlace(MPlace)")]
        public bool InsertLocation(string ort, string ansprechpartner, string fk_bundesland_id)
        {
            ConnectDatabase();
            int seminarort_id = nextId("seminarort");
            try
            {
                m_dbCommand.CommandText = "insert into seminarort(ort,ansprechpartner,fk_bundesland_id) values("
                                            + seminarort_id
                                            + ",\"" + ort + "\""
                                            + ",\"" + ansprechpartner + "\""
                                            + "," + fk_bundesland_id + ")";
                CloseDatabase();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }
        [Obsolete("Bitte nutz InsertFederalState(MFederalState)")]
        public bool InsertFederelState(string name, string kuerzel)
        {
            ConnectDatabase();
            int bundesland_id = nextId("bundesland");
            try
            {
                m_dbCommand.CommandText = "insert into bundesland values("
                                        + bundesland_id + ",\""
                                        + name + "\",\""
                                        + kuerzel + "\")";
                CloseDatabase();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }
        [Obsolete("Bitte nutz InsertTrainer(MTrainer)")]
        public bool InsertTrainer(string vorname, string nachname, string kuerzel, string intern)
        {
            return false;
        }

        public bool InsertSeminar(MSeminar seminar)
        {
            ConnectDatabase();
            try
            {
                ExecuteNonQuery("insert into seminar(titel,untertitel,kuerzel,technik) values("
                    + "\"" + seminar.Title + "\""
                    + ",\"" + seminar.Subtitle + "\""
                    + ",\"" + seminar.Abbreviation + "\""
                    + ",\"" + seminar.HasTechnology + "\""
                    + ")");
                CloseDatabase();
                FillAllSeminar();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }

        public bool InsertTrainer(MTrainer trainer)
        {
            ConnectDatabase();
            int isinternal;
            try
            {
                if (trainer.IsInternal)
                {
                    isinternal = 1;
                }
                else
                {
                    isinternal = 0;
                }
                ExecuteNonQuery("insert into trainer(vorname,nachname,kuerzel,intern) values("
                                        + "\"" + trainer.Name + "\""
                                        + ",\"" + trainer.Surname + "\""
                                        + ",\"" + trainer.Abbreviation + "\""
                                        + "," + isinternal
                                        + ")");
                CloseDatabase();
                FillAllTrainer();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }

        public bool InsertRoom(MRoom room)
        {
            ConnectDatabase();
            try
            {
               ExecuteNonQuery("insert into raum(raumnummer,fk_seminarort_id) values("
                                        + "\"" + room.Number + "\""
                                        + "," + room.Place_id
                                        + ")");
                CloseDatabase();
                FillAllRoom();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }

        public bool InsertPlace(MPlace place)
        {
            ConnectDatabase();
            try
            {
               ExecuteNonQuery("insert into seminarort(ort,ansprechpartner,fk_bundesland_id) values("
                                            + "\"" + place.Place + "\""
                                            + ",\"" + place.Contact + "\""
                                            + "," + place.Federalstate_id + ")");
                CloseDatabase();
                FillAllPlace();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }

        public bool InsertFederalState(MFederalState federalstate)
        {
            ConnectDatabase();
            try
            {
               ExecuteNonQuery( "insert into bundesland(name,kuerzel) values("
                                        + "\"" + federalstate.Name + "\""
                                        + ",\"" + federalstate.Abbreviation + "\""
                                        + ")");
               ExecuteNonQuery("commit;");
                CloseDatabase();
                FillAllFederalState();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }
        #endregion

        #region update
        public bool UpdateSeminar(MSeminar seminar)
        {
            ConnectDatabase();
            try
            {
                ExecuteNonQuery("update seminar set "
                                   + "titel =  \"" + seminar.Title + "\""
                                   + ",untertitel = \"" + seminar.Subtitle + "\""
                                   + ",kuerzel = \"" + seminar.Abbreviation + "\""
                                   + ",technik = \"" + seminar.HasTechnology + "\""
                                   + "where seminar_id = " + seminar.Id);
                CloseDatabase();
                AllSeminar.Clear();
                FillAllSeminar();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public bool UpdateTrainer(MTrainer trainer)
        {
            ConnectDatabase();
            int isinternal;
            try
            {
                if (trainer.IsInternal)
                {
                    isinternal = 1;
                }
                else
                {
                    isinternal = 0;
                }
                ExecuteNonQuery("update trainer set "
                              + "kuerzel = \""        + trainer.Abbreviation  + "\""
                              + ",intern = "          + isinternal
                              + ",vorname = \""       + trainer.Name          + "\""
                              + ",nachname = \""      + trainer.Surname       + "\""
                              + "where trainer_id = " + trainer.Id
                              );
                CloseDatabase();
                AllTrainer.Clear();
                FillAllTrainer();
                return true;
            }
            catch(SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }

        public bool UpdateRoom(MRoom room)
        {
            ConnectDatabase();
            try
            {
                
                ExecuteNonQuery("update raum set "
                              + "raumnummer = \"" + room.Number + "\""
                              + ",fk_seminarort_id = " + room.Place_id
                              + " where raum_id = " + room.Id
                              );
                CloseDatabase();
                AllRoom.Clear();
                FillAllRoom();
                return true;
            }catch(SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }

        public bool UpdatePlace(MPlace place)
        {
            ConnectDatabase();
            try
            {
                ExecuteNonQuery("update seminarort set "
                              + " ansprechpartner = \"" + place.Contact + "\""
                              + ",fk_bundesland_id = " + place.Federalstate_id
                              + ",ort = \"" + place.Place + "\""
                              + " where seminarort_id = " + place.Id
                              );
                CloseDatabase();
                AllPlace.Clear();
                FillAllPlace();
                return true;
            }
            catch(SQLiteException)
            {
                CloseDatabase();
                return false;
            }
                
        }

        public bool UpdateFederalState(MFederalState federalstate)
        {
            ConnectDatabase();
            try
            {
                ExecuteNonQuery("update bundesland set "
                              + " kuerzel = \"" + federalstate.Abbreviation + "\""
                              + ",name = \"" + federalstate.Name + "\""
                              + " where bundesland_id = " + federalstate.Id
                              );
                CloseDatabase();
                AllFederalState.Clear();
                FillAllFederalState();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }
        #endregion

        #region delete
        public bool DeleteSeminar(MSeminar seminar)
        {
            ConnectDatabase();
            try
            {
                ExecuteNonQuery("delete from seminar where seminar_id = " + seminar.Id);
                CloseDatabase();
                AllSeminar.Clear();
                FillAllSeminar();
                return true;
            }
            catch(SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }

        public bool DeleteTrainer(MTrainer trainer)
        {
            ConnectDatabase();
            try
            {
                ExecuteNonQuery("delete from trainer where trainer_id = " + trainer.Id);
                CloseDatabase();
                AllTrainer.Clear();
                FillAllTrainer();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }

        public bool DeleteRoom(MRoom room)
        {
            ConnectDatabase();
            try
            {
                ExecuteNonQuery("delete from raum where raum_id = " + room.Id);
                CloseDatabase();
                AllRoom.Clear();
                FillAllRoom();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }

        public bool DeletePlace(MPlace place)
        {
            ConnectDatabase();
            try
            {
                ExecuteNonQuery("delete from seminarort where seminarort_id = " + place.Id);
                CloseDatabase();
                AllPlace.Clear();
                FillAllPlace();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }

        public bool DeleteFederalState(MFederalState federalstate)
        {
            ConnectDatabase();
            try
            {
                ExecuteNonQuery("delete from bundesland where bundesland_id = " + federalstate.Id);
                CloseDatabase();
                AllFederalState.Clear();
                FillAllFederalState();
                return true;
            }
            catch (SQLiteException)
            {
                CloseDatabase();
                return false;
            }
        }
        #endregion
        //Für die erst Installation der Datenbank
        public void CreateDB()
        {
            SQLiteConnection.CreateFile(url);

            SQLiteConnection connect = new SQLiteConnection("Data Source=" + url);

            connect.Open();

            string sql = "CREATE TABLE trainer("
                        + "trainer_id integer primary key autoincrement,"
                        + "vorname varchar(100),"
                        + "nachname varchar(100),"
                        + "kuerzel varchar(5),"
                        + "intern integer"
                        + ")";

            SQLiteCommand command = new SQLiteCommand(sql, connect);
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE bundesland("
                                + "bundesland_id integer primary key autoincrement,"
                                + "name varchar(50),"
                                + "kuerzel varchar(10)"
                                + ")";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE seminarort("
                                    + "seminarort_id integer primary key autoincrement,"
                                    + "ort varchar(100),"
                                    + "ansprechpartner varchar(100),"
                                    + "fk_bundesland_id integer,"
                                    + "foreign key(fk_bundesland_id) references bundesland(bundesland_id)"
                                    + ")";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE raum("
                                + "raum_id integer primary key autoincrement,"
                                + "raumnummer varchar(50),"
                                + "fk_seminarort_id integer,"
                                + "foreign key(fk_seminarort_id) references seminarort(seminarort_id)"
                                + ")";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE Kalendertag ("
                                + "kalendertag_id integer primary key autoincrement,"
                                + "datum date,"
                                + "ferien_name varchar(100),"
                                + "kalenderwoche integer"
                                + ")";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE fachrichtung("
                                + "fachrichtung_id integer primary key autoincrement,"
                                + "bezeichnung varchar(100),"
                                + "ausbildungsjahr varchar(15),"
                                + "bundesland varchar(50)"
                                + ")";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE seminar("
                                + "seminar_id integer primary key autoincrement,"
                                + "titel varchar(50),"
                                + "untertitel varchar(100),"
                                + "kuerzel varchar(20),"
                                + "technik varchar(200)"
                                + ")";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE seminartag("
                                + "seminartag_id integer primary key autoincrement,"
                                + "fk_trainer_id integer null,"
                                + "fk_cotrainer_id integer null,"
                                + "fk_seminar_id integer null,"
                                + "foreign key(fk_trainer_id) references trainer(trainer_id),"
                                + "foreign key(fk_cotrainer_id) references trainer(trainer_id)"
                                + ")";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE se_2_ra("
                                + "fk_seminar_id integer primary key autoincrement,"
                                + "fk_raum_id integer,"
                                + "gruppenraum integer,"
                                + "foreign key(fk_seminar_id) references seminar(seminar_id),"
                                + "foreign key(fk_raum_id) references raum(raum_id)"
                                + ")";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE ka_2_se_2_fa("
                                + "fk_kalendertag_id integer primary key autoincrement,"
                                + "fk_fachrichtung_id integer,"
                                + "fk_seminartag_id integer null,"
                                + "typ varchar(50),"
                                + "bemerkung varchar(200),"
                                + "foreign key(fk_kalendertag_id) references kalendertag(kalendertag_id),"
                                + "foreign key(fk_fachrichtung_id) references fachrichtung(fachrichtung_id),"
                                + "foreign key(fk_seminartag_id) references seminartag(seminartag_id)"
                                + ")";
            command.ExecuteNonQuery();



            connect.Close();
        }

        //zum ersten befuellen der DB
        public void FillDB()
        {
            FillSeminar();
            FillTrainer();
            FillBundesland();
            FillSeminarort();
            FillRaum();
        }

        #region tabellen_fuellen
        private void FillSeminar()
        {
            SQLiteConnection connect;
            connect = new SQLiteConnection("Data Source=" + url);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand("PRAGMA foreign_keys=ON", connect);
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"JavaScript\",\"Webseiten mit Programmfunktionalität steuern\",\"DAJSCRI\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"Objektorientierte Anforderungsanalyse\",\"Statiische und dynamische Klassenmodellierung\",\"DAOOANW\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"IT-Sicherheitskonzepte\",null,\"DASIKON\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"C# Grundlagen\",null,\"DACSHGL\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"Grundlagen der Informationstechnologie\",null,\"DAGLITE\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"Grundlagen Netze\",null,\"DAGLNET\",\"Laptop, Switch, Hub\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"Java Grundlagen\",\"Grundlagen\",\"DAJAVAG\",\"Laptop, Eclipse\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"Grundlagen der Programmierung\",null,\"DAGLPRO\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"C# Vertiefung\",null,\"DACSHVT\",\"Laptop, SharpDevelop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"Betriebssysteme (Linux und Windows)\",null,\"DABESYS\",\"Laptop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"XML Einführung\",\"Die Auszeichnungssprache zur strukturellen Beschreibung von Web-Dokumenten\",\"DAXMLWD\",\"Laptop, XMLEditor\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"Der Business Analyst  in der Anforderungsanalyse\",null,\"BUSAO-R (3)\",\"Laptop, Visio\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"Netzwerkintegration\",null,\"DANWINT\",\"Laptop, Server, VMWare\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"Systemüberwachung\",null,\"DASYSUB\",\"Laptop, Server, VMWare\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"HTML Grundlagen\",null,\"DAIHTML\",\"Laptop, HTML-Kit\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"MySQL Grundlagen\",null,\"DAMYSQLGL-ALT\",\"Laptop, MySQL-Community Edition\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"Projektaufgabe Java Vertiefung\",\"Anhängervermietung\",\" PROJVT\",\"Laptop, Eclipse\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"Projektaufgabe C# Vertiefung\",null,\"DAPROCSV\",\"Laptop, SharpDevelop\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminar(titel,untertitel,kuerzel,technik) values(\"Vertiefung objektorientierte Anforderungsanalyse für Business Analysten\",null,\"DAVOOBA\",\"Laptop\")";
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

            command.CommandText = "insert into trainer(vorname,nachname,kuerzel,intern) values(\"Thomas\",\"Bender\",\"TBE\",1)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer(vorname,nachname,kuerzel,intern) values(\"Nico\",\"Carlsen\",\"\",1)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer(vorname,nachname,kuerzel,intern) values(\"Bernd\",\"Reimann\",\"\",0)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer(vorname,nachname,kuerzel,intern) values(\"Carsten\",\"Lenz\",\"\",1)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer(vorname,nachname,kuerzel,intern) values(\"Thomas\",\"Adameit\",\"\",1)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer(vorname,nachname,kuerzel,intern) values(\"Katrin\",\"Klein\",\"\",0)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer(vorname,nachname,kuerzel,intern) values(\"Joerg\",\"Martin\",\"JMA\",1)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer(vorname,nachname,kuerzel,intern) values(\"Rolf\",\"Schmidt\",\"RSC\",1)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer(vorname,nachname,kuerzel,intern) values(\"Markus\",\"Ruecker\",\"MRK\",1)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer(vorname,nachname,kuerzel,intern) values(\"Christina\",\"von Ziegsar\",\"\",1)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into trainer(vorname,nachname,kuerzel,intern) values(\"Bernie\",\"Cornwell\",\"\",1)";
            command.ExecuteNonQuery();

            connect.Close();
        }

        private void FillBundesland()
        {
            SQLiteConnection connect;
            connect = new SQLiteConnection("Data Source=" + url);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand("PRAGMA foreign_keys=ON", connect);
            command.ExecuteNonQuery();

            command.CommandText = "insert into bundesland(name,kuerzel) values(\"Nordrehin-Westfalen\",\"NRW\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into bundesland(name,kuerzel) values(\"Hessen\",\"HE\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into bundesland(name,kuerzel) values(\"Bayern\",\"BY\")";
            command.ExecuteNonQuery();

            connect.Close();
        }

        private void FillSeminarort()
        {

            SQLiteConnection connect;
            connect = new SQLiteConnection("Data Source=" + url);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand("PRAGMA foreign_keys=ON", connect);
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminarort(ort,ansprechpartner,fk_bundesland_id) values(\"Wiesbaden Berufsschule\",\"Gerhand Ganz, Abdreas Kirschner\",2)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminarort(ort,ansprechpartner,fk_bundesland_id) values(\"Wiesbaden BCRM\",\"Markus Rossberg\",2)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminarort(ort,ansprechpartner,fk_bundesland_id) values(\"Köln Geschäftsstelle\",\"Susanne Wegener\",1)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminarort(ort,ansprechpartner,fk_bundesland_id) values(\"Köln Berufsschule\",\"GSO Herr Faller\",1)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into seminarort(ort,ansprechpartner,fk_bundesland_id) values(\"München Geschäftsstelle\",\"Christina\",3)";
            command.ExecuteNonQuery();

            connect.Close();
        }

        private void FillRaum()
        {

            SQLiteConnection connect;
            connect = new SQLiteConnection("Data Source=" + url);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand("PRAGMA foreign_keys=ON", connect);
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"207\",3)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"208\",3)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"209\",3)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"210\",3)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"211\",3)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"213\",3)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"306\",3)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"26\",1)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"28\",1)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"C01.06\",2)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"AKON.1\",2)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"AKON.2\",2)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"AKON.3\",2)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"C021\",4)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"C001\",4)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"C005\",4)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"C017\",4)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"C002\",4)";
            command.ExecuteNonQuery();

            command.CommandText = "insert into raum(raumnummer,fk_seminarort_id) values(\"in München\",5)";
            command.ExecuteNonQuery();

            connect.Close();

        }

        #endregion

        #region combobox
        [Obsolete("Bitte nutz FillTrainerComboBox(ComboBox)")]
        public void FillTrainerCombobox(ComboBox combobox)
        {
            combobox.Items.Clear();
            ConnectDatabase();
            bool intern;
            SQLiteDataReader reader = ExecuteQuery("select * from trainer");

            while (reader.Read())
            {
                if (Convert.ToInt32(reader["intern"].ToString()) == 1)
                {
                    intern = true;
                }
                else
                {
                    intern = false;
                }
                combobox.Items.Add(new MTrainer(Convert.ToInt32(reader["trainer_id"].ToString()),
                                                reader["vorname"].ToString(),
                                                reader["nachname"].ToString(),
                                                reader["kuerzel"].ToString(),
                                                intern));
            }

            reader.Close();

            CloseDatabase();
        }
        [Obsolete("Bitte nutz FillSeminarComboBox(ComboBox)")]
        public void FillSeminarCombobox(ComboBox combobox)
        {

            ConnectDatabase();

            SQLiteDataReader reader = ExecuteQuery("select * from seminar");

            while (reader.Read())
            {
                combobox.Items.Add(new MSeminar(Convert.ToInt32(reader["seminar_id"].ToString()),
                                            reader["titel"].ToString(),
                                            reader["untertitel"].ToString(),
                                            reader["kuerzel"].ToString(),
                                            reader["technik"].ToString(),
                                            "")); //comment, wird nicht von der DB befuellt
            }

            reader.Close();
            CloseDatabase();
        }
        [Obsolete("Bitte nutz FillFederalStateComboBox(ComboBox)")]
        public void FillFederalStateCombobox(ComboBox combobox)
        {

            ConnectDatabase();

            SQLiteDataReader reader = ExecuteQuery("select bundesland_id, name, kuerzel from bundesland");

            while (reader.Read())
            {
                combobox.Items.Add(new MFederalState(Convert.ToInt32(reader["bundesland_id"].ToString()), reader["name"].ToString(), reader["kuerzel"].ToString()));
            }
            reader.Close();
            CloseDatabase();
        }
        [Obsolete("Bitte nutz FillTPlaceComboBox(ComboBox)")]
        public void FillLocationCombobox(ComboBox combobox)
        {
            ConnectDatabase();

            SQLiteDataReader reader = ExecuteQuery("select seminarort_id, ort,ansprechpartner  from seminarort");

            while (reader.Read())
            {
                combobox.Items.Add(new MPlace(Convert.ToInt32(reader["seminarort_id"].ToString()),
                                                reader["ort"].ToString(),
                                                reader["ansprechpartner"].ToString()));
            }

            reader.Close();
            CloseDatabase();
        }
        [Obsolete("Bitte nutz FillRoomComboBox(ComboBox,int)")]
        public void FillRoomCombobox(ComboBox combobox, int location)
        {
            ConnectDatabase();

            SQLiteDataReader reader = ExecuteQuery("select raum_id, raumnummer from raum where fk_seminarort_id =" + location);

            while (reader.Read())
            {
                combobox.Items.Add(new MPlace(Convert.ToInt32(reader["seminarort_id"].ToString()),
                                                   reader["ort"].ToString(),
                                                   reader["ansprechpartner"].ToString()));
            }

            reader.Close();
            CloseDatabase();
        }
        #endregion

        public void FillAllList()
        {
            lock (this)
            {
                FillAllFederalState();
                FillAllPlace();
                FillAllRoom();
                FillAllSeminar();
                FillAllTrainer();
            }
            
        }

        #region FillAll
        private void FillAllTrainer()
        {
            ConnectDatabase();
            SQLiteDataReader reader = ExecuteQuery("select * from trainer");
            bool intern;
            while (reader.Read())
            {
                if (Convert.ToInt32(reader["intern"].ToString()) == 1)
                {
                    intern = true;
                }
                else
                {
                    intern = false;
                }

                AddTrainer(new MTrainer(Convert.ToInt32(reader["trainer_id"].ToString()),
                                                reader["vorname"].ToString(),
                                                reader["nachname"].ToString(),
                                                reader["kuerzel"].ToString(),
                                                intern));
            }
            reader.Close();
            CloseDatabase();
        }

        private void FillAllFederalState()
        {
            ConnectDatabase();
            SQLiteDataReader reader = ExecuteQuery("select * from bundesland");
            while (reader.Read())
            {
                AddFederalState(new MFederalState(Convert.ToInt32(reader["bundesland_id"].ToString()),
                                                                  reader["name"].ToString(),
                                                                  reader["kuerzel"].ToString()));
            }
            reader.Close();
            CloseDatabase();
        }

        private void FillAllPlace()
        {
            ConnectDatabase();
            SQLiteDataReader reader = ExecuteQuery("select * from seminarort");
            while (reader.Read())
            {
                AddPlace(new MPlace(Convert.ToInt32(reader["seminarort_id"].ToString()),
                                                    reader["ort"].ToString(),
                                                    reader["ansprechpartner"].ToString(),
                                    Convert.ToInt32(reader["fk_bundesland_id"].ToString())));
            }
            reader.Close();
            CloseDatabase();
        }

        private void FillAllRoom()
        {
            ConnectDatabase();
            SQLiteDataReader reader = ExecuteQuery("select * from raum");
            while (reader.Read())
            {
                AddRoom(new MRoom(Convert.ToInt32(reader["raum_id"].ToString()),
                                                  reader["raumnummer"].ToString(),
                                  Convert.ToInt32(reader["fk_seminarort_id"].ToString())));
            }
            reader.Close();
            CloseDatabase();
        }

        private void FillAllSeminar()
        {
            ConnectDatabase();
            SQLiteDataReader reader = ExecuteQuery("select * from seminar");
            while (reader.Read())
            {
                AddSeminar(new MSeminar(Convert.ToInt32(reader["seminar_id"].ToString()),
                                            reader["titel"].ToString(),
                                            reader["untertitel"].ToString(),
                                            reader["kuerzel"].ToString(),
                                            reader["technik"].ToString(),
                                            ""));
            }
            reader.Close();
            CloseDatabase();
        }
        #endregion

        #region Add
        private void AddTrainer(MTrainer trainer)
        {
           if (AllTrainer.ContainsKey(trainer.Id) == false)
           {
               AllTrainer.Add(trainer.Id, trainer);
           }
           
        }
       
        private void AddFederalState(MFederalState federalstate)
        {
            if(AllFederalState.ContainsKey(federalstate.Id) == false)
            {
                AllFederalState.Add(federalstate.Id, federalstate);
            }
        }

        private void AddPlace(MPlace place)
        {
            if(AllPlace.ContainsKey(place.Id) == false)
            {
                AllPlace.Add(place.Id, place);
            }
        }

        private void AddRoom(MRoom room)
        {
            if (AllRoom.ContainsKey(room.Id) == false)
            {
                AllRoom.Add(room.Id, room);
            }
        }

        private void AddSeminar(MSeminar seminar)
        {
            if(AllSeminar.ContainsKey(seminar.Id) == false)
            {
                AllSeminar.Add(seminar.Id, seminar);
            }
        }
        #endregion

        #region Get
        public MTrainer GetTrainer(int id)
        {
            if (AllTrainer.ContainsKey(id))
            {
                return AllTrainer[id];

            }
            return null;
        }

        public MFederalState GetFederalState(int id)
        {
            if(AllFederalState.ContainsKey(id))
            {
                return AllFederalState[id];
            }
            return null;
        }

        public MPlace GetPlace(int id)
        {
            if (AllPlace.ContainsKey(id))
            {
                return AllPlace[id];
            }
            return null;
        }

        public MRoom GetRoom(int id)
        {
            if (AllRoom.ContainsKey(id))
            {
                return AllRoom[id];
            }
            return null;
        }

        public MSeminar GetSeminar(int id)
        {
            if (AllSeminar.ContainsKey(id))
            {
                return AllSeminar[id];
            }
            return null;
        }
        #endregion

        #region new_combobox
        public void FillTrainerComboBox(ComboBox combobox)
        {
            combobox.Items.Clear();
            foreach (int key in AllTrainer.Keys)
            {
                combobox.Items.Add(AllTrainer[key]);
            }
        }

        public void FillFederalStateComboBox(ComboBox combobox)
        {
            combobox.Items.Clear();
            foreach(int key in AllFederalState.Keys)
            {
                combobox.Items.Add(AllFederalState[key]);
            }
        }

        public void FillPlaceComboBox(ComboBox combobox)
        {
            combobox.Items.Clear();
            foreach(int key in AllPlace.Keys)
            {
                combobox.Items.Add(AllPlace[key]);
            }
        }

        public void FillRoomComboBox(ComboBox combobox, int location)
        {
            combobox.Items.Clear();
            foreach(int key in AllRoom.Keys)
            {
                if (AllRoom[key].Place_id == location)
                {
                    combobox.Items.Add(AllRoom[key]);
                }
            }
        }

        public void FillSeminarComboBox(ComboBox combobox)
        {
            combobox.Items.Clear();
            foreach(int key in AllSeminar.Keys)
            {
                combobox.Items.Add(AllSeminar[key]);
            }
        }
        #endregion

        [Obsolete("Bitte nutz GetInstanz")]
        public CDatabase()
        {

        }

        private CDatabase(int i)
        {

        }

        public static CDatabase GetInstance()
        {
            if (database == null)
            { 
                database = new CDatabase(1);
            }
            return database;
        }
    
        public void ThreadFillAll()
        {
            Thread t_FillAll = new Thread(FillAllList);
            t_FillAll.Start();
        }
    }
}
