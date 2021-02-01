using Blurr.SQL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;

namespace UnitTest
{
    /// <summary>
    /// UnitTests for database methods
    /// </summary>
    [TestClass]
    public class DatabaseTests
    {
        /// <summary>
        ///  Test connecting to database using DBConnection
        /// </summary>
        [TestMethod]
        public void DatabaseConnectionTest()
        {
            DBConnection dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "blurr";
            Assert.IsTrue(dbCon.Connect());
        }

        /// <summary>
        ///  Test that the database connection works and we are able to select from a table
        /// </summary>
        [TestMethod]
        public void SelectTest()
        {
            DBConnection dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "blurr";
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
    }
}


