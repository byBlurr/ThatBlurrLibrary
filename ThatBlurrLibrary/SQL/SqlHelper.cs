using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Blurr.Sql
{
    /// <summary>
    /// Helper methods for commands that are often used, more complex commands will still have to be done manually
    /// </summary>
    public class SqlHelper
    {
        private SqlHelper() {}

        /// <summary>
        /// Insert data into a specified table.
        /// </summary>
        /// <param name="dbInstance">An instance of DBConnection used to connect to the database.</param>
        /// <param name="tableName">The name of the table to insert data into.</param>
        /// <param name="columns">The columns that are being inserted into.</param>
        /// <param name="values">The values that are to be inserted, in the same order as columns</param>
        /// <returns>Boolean value representing if the command was successful.</returns>
        public static bool InsertIntoTable(SqlConnection dbInstance, string tableName, string[] columns, object[] values)
        {
            if (dbInstance == null) throw new Exception("Database connection instance is null.");
            if (String.IsNullOrEmpty(tableName)) throw new Exception("No table name was provided.");
            if (columns.Length == 0) throw new Exception("You must provide which columns to insert into.");
            if (columns.Length != values.Length) throw new Exception("Columns length is not equal to values length.");

            string sqlCommand = $"INSERT INTO {tableName} ({String.Join(", ", columns)}) VALUES ('{String.Join("', '", values)}')";

            try
            {
                if (dbInstance.Connect())
                {
                    MySqlCommand command = new MySqlCommand(sqlCommand, dbInstance.Connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    dbInstance.Close();
                    return true;
                }
                else
                {
                    throw new Exception("Could not connect to database.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update a tables data
        /// </summary>
        /// <param name="dbInstance">An instance of DBConnection used to connect to the database.</param>
        /// <param name="tableName">The name of the table to insert data into.</param>
        /// <param name="columns">The columns that are being updated.</param>
        /// <param name="values">The values that are to be inserted, in the same order as columns</param>
        /// <param name="where">The where claus (e.g "id = '1'")</param>
        /// <returns>Boolean value representing if the command was successful.</returns>
        public static bool UpdateTable(SqlConnection dbInstance, string tableName, string[] columns, object[] values, string where)
        {
            if (dbInstance == null) throw new Exception("Database connection instance is null.");
            if (String.IsNullOrEmpty(tableName)) throw new Exception("No table name was provided.");
            if (columns.Length == 0) throw new Exception("You must provide which columns to insert into.");
            if (columns.Length != values.Length) throw new Exception("Columns length is not equal to values length.");
            if (String.IsNullOrEmpty(where)) throw new Exception("You must specify a where claus.");

            string[] updates = new string[columns.Length];
            for (int col = 0; col < columns.Length; col++)
            {
                updates[col] = $"{columns[col]} = '{values[col].ToString()}'";
            }

            string sqlCommand = $"UPDATE {tableName} SET {String.Join(", ", updates)} WHERE {where}";

            try
            {
                if (dbInstance.Connect())
                {
                    MySqlCommand command = new MySqlCommand(sqlCommand, dbInstance.Connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    dbInstance.Close();
                    return true;
                }
                else
                {
                    throw new Exception("Could not connect to database.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Select data from the specified table.
        /// </summary>
        /// <typeparam name="T">The class type of object you wish the data to be returned</typeparam>
        /// <param name="dbInstance">An instance of DBConnection used to connect to the database.</param>
        /// <param name="tableName">The name of the table to insert data into.</param>
        /// <param name="columns">The columns that are being updated.</param>
        /// <param name="where">The where claus (e.g "id = '1'")</param>
        /// <returns>Returns a list of rows as objects of type T</returns>
        public static List<T> SelectDataFromTable<T>(SqlConnection dbInstance, string tableName, string[] columns, string where = null)
        {
            if (dbInstance == null) throw new Exception("Database connection instance is null.");
            if (String.IsNullOrEmpty(tableName)) throw new Exception("No table name was provided.");

            string columnsToSelect;
            if (columns.Length <= 0) columnsToSelect = "*";
            else columnsToSelect = String.Join(", ", columns);
            string sqlCommand = where == null ? $"SELECT {columnsToSelect} FROM {tableName}" : $"SELECT {columnsToSelect} FROM {tableName} WHERE {where}";

            List<T> rows = new List<T>();

            try
            {
                if (dbInstance.Connect())
                {
                    MySqlCommand command = new MySqlCommand(sqlCommand, dbInstance.Connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataSet dataset = new DataSet();

                    adapter.Fill(dataset);
                    rows = ConvertDataTable<T>(dataset.Tables[0]);

                    command.Dispose();
                    dbInstance.Close();
                }
                else
                {
                    throw new Exception("Could not connect to database.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rows;
        }

        /// <summary>
        /// Convert data table to a list of objects of type T
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        /// <param name="dt">Table to convert</param>
        /// <returns>Returns List of T</returns>
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        /// <summary>
        /// Get datarow item
        /// </summary>
        /// <typeparam name="T">Class Type of the object to return.</typeparam>
        /// <param name="dr">Datarow to convert to object of T</param>
        /// <returns>Object of T</returns>
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
