using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Blurr.SQL
{
    public class Helper
    {
        /// <summary>
        /// Insert data into a specified table.
        /// </summary>
        /// <param name="dbInstance">An instance of DBConnection used to connect to the database.</param>
        /// <param name="tableName">The name of the table to insert data into.</param>
        /// <param name="columns">The columns that are being inserted into.</param>
        /// <param name="values">The values that are to be inserted, in the same order as columns</param>
        /// <returns>Boolean value representing if the command was successful.</returns>
        public static bool InsertIntoTable(DBConnection dbInstance, string tableName, string[] columns, object[] values)
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
                    return false;
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
        public static bool UpdateTable(DBConnection dbInstance, string tableName, string[] columns, object[] values, string where)
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<T> SelectData<T>(DBConnection dbInstance, string tableName, string[] columns, string where = null)
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
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // TODO: need a way to turn a row into an object of T
                    }

                    reader.Close();
                    command.Dispose();
                    dbInstance.Close();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rows;
        }
    }
}
