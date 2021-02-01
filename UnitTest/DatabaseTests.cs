using Blurr.SQL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;

namespace UnitTest
{
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
        ///  Test selecting all rows using DBConnect with 'SELECT * FROM' command
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


