using Blurr.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace UnitTest
{
    /// <summary>
    /// UnitTests for database methods
    /// </summary>
    [TestClass]
    public class DatabaseTests
    {
        /// Need to setup a database for testing...
        private const string DATABASE_IP = "";
        private const string DATABASE_NAME = "";
        private const string DATABASE_USER = "";
        private const string DATABASE_PASSWORD = "";

        /// <summary>
        ///  Test connecting to database using DBConnection
        /// </summary>
        [TestMethod]
        public void DatabaseConnectionTest()
        {
            SqlConnection dbCon = SqlConnection.Instance();
            dbCon.DatabaseIp = DATABASE_IP;
            dbCon.DatabaseName = DATABASE_NAME;
            dbCon.DatabaseUser = DATABASE_USER;
            dbCon.DatabasePassword = DATABASE_PASSWORD;
            Assert.IsTrue(dbCon.Connect());
        }

        /// <summary>
        ///  Test that the database connection works and we are able to select from a table
        /// </summary>
        [TestMethod]
        public void SelectTest()
        {
            SqlConnection dbCon = SqlConnection.Instance();
            dbCon.DatabaseIp = DATABASE_IP;
            dbCon.DatabaseName = DATABASE_NAME;
            dbCon.DatabaseUser = DATABASE_USER;
            dbCon.DatabasePassword = DATABASE_PASSWORD;
            Assert.IsTrue(dbCon.Connect());
            var cmd = new MySqlCommand($"SELECT * FROM test", dbCon.Connection);

            var reader = cmd.ExecuteReader();
            int rows = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    rows++;
                }
            }

            reader.Close();
            cmd.Dispose();
            dbCon.Close();

            Assert.AreEqual(3, rows);
        }

        /// <summary>
        /// Test INSERT INTO helper method
        /// </summary>
        [TestMethod]
        public void InsertIntoTest()
        {
            string[] columns = { "id", "name" };
            object[] values = { 0, "Steve" };

            SqlConnection dbCon = SqlConnection.Instance();
            dbCon.DatabaseIp = DATABASE_IP;
            dbCon.DatabaseName = DATABASE_NAME;
            dbCon.DatabaseUser = DATABASE_USER;
            dbCon.DatabasePassword = DATABASE_PASSWORD;
            try
            {
                Assert.IsTrue(SqlHelper.InsertIntoTable(dbCon, "insert_test", columns, values));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Test UPDATE helper method
        /// </summary>
        [TestMethod]
        public void UpdateTest()
        {
            string[] columns = { "first_name", "last_name" };
            object[] values = { "Bob", "Bryant" };
            string where = "id = '2'";

            SqlConnection dbCon = SqlConnection.Instance();
            dbCon.DatabaseIp = DATABASE_IP;
            dbCon.DatabaseName = DATABASE_NAME;
            dbCon.DatabaseUser = DATABASE_USER;
            dbCon.DatabasePassword = DATABASE_PASSWORD;
            try
            {
                Assert.IsTrue(SqlHelper.UpdateTable(dbCon, "update_test", columns, values, where));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Test SELECT helper method
        /// </summary>
        [TestMethod]
        public void SelectDataTest()
        {
            string[] columns = { "first_name", "last_name" };

            SqlConnection dbCon = SqlConnection.Instance();
            dbCon.DatabaseIp = DATABASE_IP;
            dbCon.DatabaseName = DATABASE_NAME;
            dbCon.DatabaseUser = DATABASE_USER;
            dbCon.DatabasePassword = DATABASE_PASSWORD;
            try
            {
                List<Person> data = SqlHelper.SelectDataFromTable<Person>(dbCon, "update_test", columns);
                Assert.IsNotNull(data[0]);
                Assert.IsNotNull(data[0].first_name);
                Console.WriteLine(data[0].first_name);
                Assert.AreEqual(2, data.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }

    public class Person
    {
        public string first_name { get; private set; }
        public string last_name { get; private set; }

        public override string ToString()
        {
            return $"Name: {first_name} {last_name}";
        }
    }
}


