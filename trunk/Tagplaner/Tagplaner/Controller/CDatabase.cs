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
    /// <summary>
    /// Klasse zum Verbinden mit der Datenbank und Ausführen der SQL-Abfragen,
    /// sowie erstellen der Objektlisten und Syncronisation dieser mit der Datenbank.
    /// </summary>
    public class CDatabase : IDatabase
    {
        private static CDatabase database;
        private string url = "TagplanerDatabase.sqlite";
        private string backup = "TagplanerDBBackup.sqlite";
        private SQLiteConnection m_dbConnection;
        private SQLiteCommand m_dbCommand;
        private Dictionary<int, MTrainer> AllTrainer = new Dictionary<int, MTrainer>();
        private Dictionary<int, MFederalState> AllFederalState = new Dictionary<int, MFederalState>();
        private Dictionary<int, MPlace> AllPlace = new Dictionary<int, MPlace>();
        private Dictionary<int, MRoom> AllRoom = new Dictionary<int, MRoom>();
        private Dictionary<int, MSeminar> AllSeminar = new Dictionary<int, MSeminar>();

        /// <summary>
        /// Verbindungsmethode zur Datenbank
        /// </summary>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Methode zum schließen der verbindung zu Datenbank
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Methode zum ausführen einer SQL Query mit ergebnis(select)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private SQLiteDataReader ExecuteQuery(string query)
        {
            m_dbCommand.CommandText = query;
            SQLiteDataReader reader = m_dbCommand.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// Methode zum Ausführen einer SQl Query ohne Ergebnis
        /// </summary>
        /// <param name="query"></param>
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
        /// <summary>
        /// Einfügen eines Seminars in die DB
        /// </summary>
        /// <param name="seminar"></param>
        /// <returns>bool</returns>
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
            catch (SQLiteException e)
            {
                DebugUserControl uc = DebugUserControl.GetInstance();
                uc.AddDebugMessage(e.ToString());
                CloseDatabase();
                return false;
            }
        }

        /// <summary>
        /// Einfügen eines Trainers in die DB
        /// </summary>
        /// <param name="trainer"></param>
        /// <returns>bool</returns>
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
            catch (SQLiteException e)
            {
                DebugUserControl uc = DebugUserControl.GetInstance();
                uc.AddDebugMessage(e.ToString());
                CloseDatabase();
                return false;
            }
        }

        /// <summary>
        /// Einfügen eines Raums in die DB
        /// </summary>
        /// <param name="room"></param>
        /// <returns>bool</returns>
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
            catch (SQLiteException e)
            {
                DebugUserControl uc = DebugUserControl.GetInstance();
                uc.AddDebugMessage(e.ToString());
                CloseDatabase();
                return false;
            }
        }


        /// <summary>
        /// Einfügen eines Seminarortes in die DB
        /// </summary>
        /// <param name="place"></param>
        /// <returns>bool</returns>
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
            catch (SQLiteException e)
            {
                DebugUserControl uc = DebugUserControl.GetInstance();
                uc.AddDebugMessage(e.ToString());
                CloseDatabase();
                return false;
            }
        }

        /// <summary>
        /// Einfügen eines Bundeslandes in die DB
        /// </summary>
        /// <param name="federalstate"></param>
        /// <returns>bool</returns>
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
            catch (SQLiteException e)
            {
                DebugUserControl uc = DebugUserControl.GetInstance();
                uc.AddDebugMessage(e.ToString());
                CloseDatabase();
                return false;
            }
        }
        #endregion

        #region update
        /// <summary>
        /// Ändern einse Seminars in Datenbank
        /// </summary>
        /// <param name="seminar"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Ändern eines Trainers in der Datenbank
        /// </summary>
        /// <param name="trainer"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Ändern eines Raums in der Datenbank
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Ändern eines Seminarortes in der Datenbank
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Ändern eines Bundeslandes in der Datenbank
        /// </summary>
        /// <param name="federalstate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Erzeugen von Testdaten in der Datenbank
        /// </summary>
        public void FillDB()
        {
            FillSeminar();
            FillTrainer();
            FillBundesland();
            FillSeminarort();
            FillRaum();
        }

        #region tabellen_fuellen
        /// <summary>
        /// Tabelle Seminar füllen
        /// </summary>
        private void FillSeminar()
        {
            ConnectDatabase();
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('DB2 Optimierung', NULL, 'D2OPT', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Erste Hilfe', NULL, 'DA1HI', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Betriebssysteme (Linux und Windows)', NULL, 'DABESYS', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Grundlagen Betriebswirtschaft', NULL, 'DABWLGL', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Cebit Präsentation', NULL, 'DACEBITP', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('C Grundlagen', NULL, 'DACGRLG', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('C# Grundlagen', NULL, 'DACSHGL', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('C# Vertiefung', NULL, 'DACSHVT', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('C# Werkstatt', NULL, 'DACWK', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Grundlagen der Informationstechnologie', NULL, 'DAGLITE', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Grundlagen Netze', NULL, 'DAGLNET', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Grundlagen der Programmierung', NULL, 'DAGLPRO', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('HTML Grundlagen', NULL, 'DAIHTML', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Java Grundlagen', NULL, 'DAJAVAG', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Java Grundlagen Werkstatt', NULL, 'DAJAVAGWS', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Java Vertiefung', NULL, 'DAJAVVT', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Jour Fixe Personalverantwortliche', NULL, 'DAJFPV', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('JavaScript', NULL, 'DAJSCRI', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Numerische Mathematik', NULL, 'DAMATHE', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Marktanalyse auf Fachmesse', NULL, 'DAMESS', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('MySQL Grundlagen', NULL, 'DAMYSQLGL', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('MySQL Grundlagen', NULL, 'DAMYSQLGL-ALT', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Netzwerkintegration', NULL, 'DANWINT', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('MS-Office Grundlagen', NULL, 'DAOFFGL', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Objektorientierte Anforderungsanalyse ', NULL, 'DAOOANW', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Objektorientiertes Design ', NULL, 'DAOODES', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('PHP Grundlagen', NULL, 'DAPHPGL', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Projektsimulation Betriebssysteme', NULL, 'DAPROBS', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Projektaufgabe C# Vertiefung', NULL, 'DAPROCSV', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('IT-Sicherheitskonzepte ', NULL, 'DASIKON', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Starttag Köln', NULL, 'DASTF-K', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('CSC-Starttag Wiesbaden', NULL, 'DASTF-WI-C', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('System- und Netzwerkadministration', NULL, 'DASYNWA', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Systemüberwachung', NULL, 'DASYSUB', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Grundlagen Software Qualität - Test', NULL, 'DATESTFI', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Infoveranstaltung Abschlussprüfung', NULL, 'DAVAP-INFO-1', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Infoveranstaltung Projektantrag und Doku', NULL, 'DAVAP-INFO-2', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Prüfungsvorbereitung - Schriftliche Abschlussprüfung', NULL, 'DAVAP-S', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Vertiefung objektorientierte Anforderungsanalyse für Business Analysten', NULL, 'DAVOOBA', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Vorbereitung auf die Zwischenprüfung', NULL, 'DAVZP-1', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Vorbereitung auf die Zwischenprüfung', NULL, 'DAVZP-2', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('Werkstatt Webanwendung', NULL, 'DAWERKWEB', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('DA Workshop betriebliche Projektarbeit', NULL, 'DAWSBP', NULL )");
            ExecuteNonQuery("INSERT INTO seminar (titel, untertitel, kuerzel, technik) VALUES ('XML Einführung', NULL, 'DAXMLWD', NULL )");
            CloseDatabase();
        }
        
        /// <summary>
        /// Tabelle Trainer füllen
        /// </summary>
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

        /// <summary>
        /// Tabelle Bundesland füllen
        /// </summary>
        private void FillBundesland()
        {
            SQLiteConnection connect;
            connect = new SQLiteConnection("Data Source=" + url);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand("PRAGMA foreign_keys=ON", connect);
            command.ExecuteNonQuery();

            command.CommandText = "insert into bundesland(name,kuerzel) values(\"Nordrhein-Westfalen\",\"NRW\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into bundesland(name,kuerzel) values(\"Hessen\",\"HE\")";
            command.ExecuteNonQuery();

            command.CommandText = "insert into bundesland(name,kuerzel) values(\"Bayern\",\"BY\")";
            command.ExecuteNonQuery();

            connect.Close();
        }

        /// <summary>
        /// Tabelle Seminarort füllen
        /// </summary>
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

        /// <summary>
        /// Tabelle Raum füllen
        /// </summary>
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

        /// <summary>
        /// Alle Dictionaries anhand der Datenbank füllen
        /// </summary>
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
        /// <summary>
        /// Trainerobjekte in Dictionary AllTrainer eintragen
        /// </summary>
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

        /// <summary>
        /// FederalStateobjekte in Dictionary AllFederalState eintragen
        /// </summary>
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

        /// <summary>
        /// Placeobjekte in Dictionary AllPlace eintragen
        /// </summary>
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

        /// <summary>
        /// Raumobjekte in Dictionary AllRoom eintragen
        /// </summary>
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

        /// <summary>
        /// Seminarobjekte in AllSeminar eintragen
        /// </summary>
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
        /// <summary>
        /// Ein Trainerobjekt AllTrainer hinzufügen
        /// </summary>
        /// <param name="trainer"></param>
        private void AddTrainer(MTrainer trainer)
        {
           if (AllTrainer.ContainsKey(trainer.Id) == false)
           {
               AllTrainer.Add(trainer.Id, trainer);
           }
           
        }
       /// <summary>
       /// Ein Federalstateobjekt AllFederalState hinzufügen
       /// </summary>
       /// <param name="federalstate"></param>
        private void AddFederalState(MFederalState federalstate)
        {
            if(AllFederalState.ContainsKey(federalstate.Id) == false)
            {
                AllFederalState.Add(federalstate.Id, federalstate);
            }
        }

        /// <summary>
        /// Ein Placeobjekt AllPlace hinzufügen
        /// </summary>
        /// <param name="place"></param>
        private void AddPlace(MPlace place)
        {
            if(AllPlace.ContainsKey(place.Id) == false)
            {
                AllPlace.Add(place.Id, place);
            }
        }

        /// <summary>
        /// Ein Roomobjekt AllRoom hinzufügen
        /// </summary>
        /// <param name="room"></param>
        private void AddRoom(MRoom room)
        {
            if (AllRoom.ContainsKey(room.Id) == false)
            {
                AllRoom.Add(room.Id, room);
            }
        }
        /// <summary>
        /// Ein Seminarobjekt AllSeminar hinzufügen
        /// </summary>
        /// <param name="seminar"></param>
        private void AddSeminar(MSeminar seminar)
        {
            if(AllSeminar.ContainsKey(seminar.Id) == false)
            {
                AllSeminar.Add(seminar.Id, seminar);
            }
        }
        #endregion

        #region Get
        /// <summary>
        /// Ein Trainerobjekt aus AllTrainer holen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MTrainer GetTrainer(int id)
        {
            if (AllTrainer.ContainsKey(id))
            {
                return AllTrainer[id];

            }
            return null;
        }

        /// <summary>
        /// Ein FederalStateobjekt aus AllFederalState holen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MFederalState GetFederalState(int id)
        {
            if(AllFederalState.ContainsKey(id))
            {
                return AllFederalState[id];
            }
            return null;
        }

        /// <summary>
        /// Ein Placeobjekt aus AllPlace holen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MPlace GetPlace(int id)
        {
            if (AllPlace.ContainsKey(id))
            {
                return AllPlace[id];
            }
            return null;
        }

        /// <summary>
        /// Ein Roomobjekt aus AllRoom holen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MRoom GetRoom(int id)
        {
            if (AllRoom.ContainsKey(id))
            {
                return AllRoom[id];
            }
            return null;
        }

        /// <summary>
        /// Ein Seminarobjekt aus AllSeminar holen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        private CDatabase()
        {

        }

        public static CDatabase GetInstance()
        {
            if (database == null)
            { 
                database = new CDatabase();
            }
            return database;
        }
    
        /// <summary>
        /// Alle Listen als Thread füllen
        /// </summary>
        public void ThreadFillAll()
        {
            Thread t_FillAll = new Thread(FillAllList);
            t_FillAll.Start();
        }

        /// <summary>
        /// Datenbank kopieren
        /// </summary>
        private void SaveDB()
        {
            System.IO.File.Copy(url, backup, true);
        }

        /// <summary>
        /// datenbank aus kopie wiederherstellen
        /// </summary>
        private void RestoreDB()
        {
            System.IO.File.Copy(backup, url, true);
        }

        /// <summary>
        /// Überprüfen ob Datenbank beschädigt ist
        /// </summary>
        /// <returns></returns>
        private bool CheckDB()
        {
            ConnectDatabase();
            m_dbCommand.CommandText = "PRAGMA integrity_check";
            int i = m_dbCommand.ExecuteNonQuery();
            CloseDatabase();
            if (i == -1)
            {
                return false;
            }
            return true;

        }

        /// <summary>
        /// Datenbank überprüfen und ggf wiederherstellen oder sichern.
        /// </summary>
        public void CheckDBForBug()
        {
            if (CheckDB())
                SaveDB();
            else
                RestoreDB();
        }
    }
}
