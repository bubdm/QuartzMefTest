using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Toolbox.Persistance.Svc.DTO;

namespace Toolbox.Persistance.Svc
{
    public class PersistanceService : IPersistanceService
    {
        private SQLiteConnection m_dbConnection;

        public PersistanceService()
        {
            // check for DB existance, if not, creation
            if (!File.Exists("Toolbox.Data.Svc.sqlite"))
            {
                SQLiteConnection.CreateFile("Toolbox.Data.Svc.sqlite");
            }

            m_dbConnection = new SQLiteConnection("Data Source=Toolbox.Data.Svc.sqlite;Version=3;");
            m_dbConnection.Open();

            // Load all database module definitions with mef and initialize it
        }

        public RequestResult ExecuteRequest(string moduleName, string request)
        {
            return null;
        }
    }
}
