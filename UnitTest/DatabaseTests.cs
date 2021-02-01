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

        /// <summary>
        /// Test INSERT INTO helper method
        /// </summary>
        [TestMethod]
        public void InsertIntoTest()
        {
            string[] columns = { "id", "name" };
            object[] values = { 0, "Steve" };

            DBConnection dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "blurr";
            try
            {
                Assert.IsTrue(Helper.InsertIntoTable(dbCon, "insert_test", columns, values));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            string[] columns = { "first_name", "last_name" };
            object[] values = { "Bob", "Bryant" };
            string where = "id = '2'";

            DBConnection dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "blurr";
            try
            {
                Assert.IsTrue(Helper.UpdateTable(dbCon, "update_test", columns, values, where));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}


