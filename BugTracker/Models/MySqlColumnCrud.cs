using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace BugTracker.Models
{
    public class MySqlColumnCrud
    {
        public static void DeleteColumn(MySqlConnection connection, string table, string column)
        {
            string query = $"ALTER TABLE {table} DROP COLUMN {column};";

            if (MySqlCrud.OpenConnection(connection))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                MySqlCrud.CloseConnection(connection);
            }
        }

        public static void AddColumn(MySqlConnection connection, string table, string column,
            string dataType, bool canNull, List<string> dataRestrictions = null, 
            List<string> valuesToSet = null)
        {
            string dataRestrictString = "";
            string setValues = "";

            //solve for the data restriction values
            if(dataRestrictions == null)
            {
                //do nothing
            }
            else
            {
                string tempVals = "";
                foreach(string i in dataRestrictions)
                {
                    tempVals += $", {i}";
                }
                dataRestrictString = $"({tempVals})";
            }

            //solve for setValues string
            if (canNull)
            {
                // do nothing
            }
            else
            {
                setValues += "NOT NULL";
            }

            if(valuesToSet != null)
            {
                foreach(string i in valuesToSet)
                {
                    setValues += $" {i}";
                }
            }

            //solve for set Values

            string query = $"ALTER TABLE {table} ADD {column} {dataType} {dataRestrictString}" +
                $" {setValues};";
        }
    }
}