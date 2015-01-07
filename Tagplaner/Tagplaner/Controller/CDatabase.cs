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
        //noch in arbeit
        public void createdb()
        {
            SQLiteConnection.CreateFile("TagplanerDatabase.sqlite");

            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=TagplanerDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            string sql = "create table";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            m_dbConnection.Close();

        }

    }
}
