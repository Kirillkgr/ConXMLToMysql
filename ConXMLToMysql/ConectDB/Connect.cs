using MySql.Data.MySqlClient;

namespace ConXMLToMysql.ConectDB
{
    public class Connect
    {
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public MySqlConnection Connection { get; private set; }

        private static Connect _instance;

        public static Connect Instance()
        {
            return _instance ?? (_instance = new Connect());
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (string.IsNullOrEmpty(DatabaseName))
                    return false;
                var construing = $"Server={Server}; database={DatabaseName}; uid={UserName}; pwd={Password}";
                Connection = new MySqlConnection(construing);
                Connection.Open();
            }

            return true;
        }

        public void Close()
        {
            if (!Connect.Instance().IsConnect()) return;
            Connection.Close();
            Connection = null;
        }
    }
}