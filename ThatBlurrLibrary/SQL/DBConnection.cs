using MySql.Data.MySqlClient;
using System;

namespace Discord.Net.Bot.Database.Sql
{
    public class DBConnection
    {
        private DBConnection()
        {

        }

        private string databaseIp = string.Empty;
        private string databaseName = string.Empty;
        private string databasePassword = string.Empty;
        public string DatabaseIp
        {
            get { return databaseIp; }
            set { databaseIp = value; }
        }
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }
        public string DatabasePassword
        {
            get { return databasePassword; }
            set { databasePassword = value; }
        }

        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            _instance = new DBConnection();
            return _instance;
        }

        public bool Connect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(databaseIp)) databaseIp = "127.0.0.1";
                if (String.IsNullOrEmpty(databaseName)) return false;
                if (String.IsNullOrEmpty(databasePassword)) return false;

                string connstring = $"Server={databaseIp}; database={databaseName}; UID=root; password={databasePassword}";
                try
                {
                    connection = new MySqlConnection(connstring);
                    connection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Database error: Already connected.");
                return false;
            }

        }

        public void Close()
        {
            if (Connection != null) connection.Close();
        }
    }
}