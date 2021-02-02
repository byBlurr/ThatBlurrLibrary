using MySql.Data.MySqlClient;
using System;

namespace Blurr.Sql
{
    public class SqlConnection
    {
        /// <summary> We don't want people to make instances of this class... </summary>
        private SqlConnection(){}

        /// Database information for connection string
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

        /// Our MySql connection
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        /// Get an instance of DBConnection
        private static SqlConnection _instance = null;
        public static SqlConnection Instance()
        {
            _instance = new SqlConnection();
            return _instance;
        }

        /// <summary>
        /// Connect to the database
        /// </summary>
        /// <returns>Returns true if successful</returns>
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

        /// <summary>
        /// Close the database connection
        /// </summary>
        public void Close()
        {
            if (connection != null)
            {
                connection.Close();
                connection = null;
            }
        }
    }
}
