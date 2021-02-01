using MySql.Data.MySqlClient;
using System;

namespace Blurr.SQL
{
    public class DBConnection
    {
        private DBConnection()
        {

        }

        private string databaseUser = string.Empty;
        private string databaseIp = string.Empty;
        private string databaseName = string.Empty;
        private string databasePassword = string.Empty;
        public string DatabaseUser
        {
            get { return databaseUser; }
            set { databaseUser = value; }
        }
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
                if (String.IsNullOrEmpty(databaseName)) throw new Exception("Database error: No database name provided. instance.DatabaseName() to set the database name.");

                if (String.IsNullOrEmpty(databaseUser)) databaseUser = "root";
                if (String.IsNullOrEmpty(databaseIp)) databaseIp = "127.0.0.1";
                if (String.IsNullOrEmpty(databasePassword)) databasePassword = "";

                string connstring = $"Server={databaseIp}; database={databaseName}; UID={databaseUser}; password={databasePassword}";
                try
                {
                    connection = new MySqlConnection(connstring);
                    connection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("Database error: Already connected.");
            }

        }

        public void Close()
        {
            if (Connection != null) connection.Close();
        }
    }
}