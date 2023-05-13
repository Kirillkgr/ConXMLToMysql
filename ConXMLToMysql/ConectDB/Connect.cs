using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace ConXMLToMysql.ConectDB
{
    public class Connect
    {
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public MySqlConnection Connection { get; set; }

        private static Connect _instance = null;
        public static Connect Instance()
        {
            if (_instance == null)
                _instance = new Connect();
            return _instance;
        }
        
        public bool IsConnect()
        {
            
         
            if (Connection == null)
            {
                if (string.IsNullOrEmpty(DatabaseName.ToString()))
                    return false;
                string construing = string.Format("Server={0}; database={1}; uid={2}; pwd={3}", Server,
                    DatabaseName, UserName, Password);
                Connection = new MySqlConnection(construing);
                Connection.Open();
            }

            return true;
        }

        public void Close()
        {
            Connection.Close();
            Connection = null;
        }
    }
}