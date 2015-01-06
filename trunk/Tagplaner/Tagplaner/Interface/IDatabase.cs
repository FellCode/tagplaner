using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Tagplaner.Interface
{
    interface IDatabase
    {
        bool ConnectDatabase();
        bool CloseDatabase();

        //Könnte man in Select, Update und delete aufteilen
        SQLiteDataReader Executequery(string query);


    }
}
