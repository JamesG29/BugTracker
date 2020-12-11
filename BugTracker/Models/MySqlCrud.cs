using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace BugTracker.Models
{
    public class MySqlCrud
    {
        public static bool OpenConnection(MySqlConnection connection)
        {
            try
            {
                connection.Open();
                Console.WriteLine("Opened DataBase");
                return true;
            }
            //catch the error here as 'ex'
            catch (MySqlException ex)
            {
                //error 0: cant connect to server
                //error 1045: invalud username or password
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Can't connect to server. Contact Supprt");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid user/password, try again.");
                        break;
                }

                return false;
            }
        }

        public static bool CloseConnection(MySqlConnection connection)
        {
            try
            {
                connection.Close();
                Console.WriteLine("Closed Database");
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        
        public static void InsertRow(MySqlConnection connection, string table,
              List<string> tableArgTypes, List<string> tableArgVals)
        {

            //to double check expections
            CloseConnection(connection);

            //tablrArgumentsString starts Empty
            string tableArgumentsString = "";
            //add all arguments that will be in the inserted into the Table
            for (int i = 0; i < tableArgTypes.Count; i++)
            {
                if (i == tableArgTypes.Count - 1)
                {
                    tableArgumentsString += $"{tableArgTypes[i]}";
                }
                else
                {
                    tableArgumentsString += $"{tableArgTypes[i]}, ";
                }
            }


            //tableValuesString starts Empty
            string tableValuesString = "";
            //add all values that will be inserted into the table
            for (int i = 0; i < tableArgVals.Count; i++)
            {
                if (i == tableArgVals.Count - 1)
                {
                    //The worlds DEFAULT and NULL are not strings and there for must not contain quotation marks
                    if (tableArgVals[i] == "DEFAULT" || tableArgVals[i] == "NULL")
                    {
                        tableValuesString += $"{tableArgVals[i]}";
                    }
                    else
                    {
                        tableValuesString += $"'{tableArgVals[i]}'";
                    }
                }
                else
                {
                    if (tableArgVals[i] == "DEFAULT" || tableArgVals[i] == "NULL")
                    {
                        tableValuesString += $"{tableArgVals[i]}, ";
                    }
                    else
                    {
                        tableValuesString += $"'{tableArgVals[i]}', ";
                    }
                }
            }



            string query = $"INSERT INTO {table} ({tableArgumentsString}) VALUES({tableValuesString})";
            Console.WriteLine($"Inputed Query: {query}");
            //open the connection to insert
            //create the commadn to input the queary intp MySQL

            if (OpenConnection(connection))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //execute the command
                cmd.ExecuteNonQuery();
                Console.WriteLine("Executed DB Insert Command");

                //end connection(close)
                CloseConnection(connection);
            }
        }
        
        public static void UpdateRow(MySqlConnection connection, string table, List<string> tableCollumns, List<string> tableVals,
            string locateCollumn, string locateValue)
        {
            //to double check expections
            CloseConnection(connection);
            //initially define set and where as empty strings
            string set = "";
            string where = "";

            //SET
            //create the set string base off of functions inputs
            for (int i = 0; i < tableCollumns.Count; i++)
            {
                //this would be the last one in the last, so dont add a comma or space at the end
                if (i == tableCollumns.Count - 1)
                {
                    if (tableVals[i] == "DEFAULT" || tableVals[i] == "NULL")
                    {
                        //remove quitation marks of DEFAULT or NULL
                        set += $"{tableCollumns[i]}={tableVals[i]}";
                    }
                    else
                    {
                        set += $"{tableCollumns[i]}='{tableVals[i]}'";
                    }
                }
                else
                {
                    if (tableVals[i] == "DEFAULT" || tableVals[i] == "NULL")
                    {
                        //remove quitation marks of DEFAULT or NULL
                        set += $"{tableCollumns[i]}={tableVals[i]}, ";
                    }
                    else
                    {
                        set += $"{tableCollumns[i]}='{tableVals[i]}', ";
                    }
                }

            }

            //WHERE
            if (locateValue == "DEFAULT" || locateValue == "NULL")
            {
                //remove quitation marks of DEFAULT or NULL
                where = $"{locateCollumn}={locateValue}";
            }
            else
            {
                where = $"{locateCollumn}='{locateValue}'";
            }


            //EXAMPLE query: "UPDATE user SET name='Jeffery', age='43' WHERE userid='12'"
            string query = $"UPDATE {table} SET {set} WHERE {where}";
            Console.WriteLine("UPDATE QUERY: " + query);

            if (OpenConnection(connection) == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                CloseConnection(connection);

            }
        }
        
        public static void DeleteRow(MySqlConnection connection, string table, string locateCollumn, string locateValue)
        {
            //to double check expections
            CloseConnection(connection);
            string where = "";
            //WHERE
            if (locateValue == "DEFAULT" || locateValue == "NULL")
            {
                //make sure that DEFAULT and NULL aren't enetered as mysql strings
                where = $"{locateCollumn}={locateValue}";
            }
            else
            {
                where = $"{locateCollumn}='{locateValue}'";
            }
            string query = $"DELETE FROM {table} WHERE {where}";
            Console.WriteLine("DELETE QUERY: " + query);

            if (OpenConnection(connection))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                CloseConnection(connection);
            }
        }
        
        public static List<string>[] SelectAllRows(MySqlConnection connection, string table, List<string> tableArgTypes)
        {
            //to double check expections
            CloseConnection(connection);
            //create the query
            string query = $"SELECT * FROM {table}";
            //create temp list to eventually send as our actual return list
            List<string>[] templist = new List<string>[tableArgTypes.Count];
            for (int i = 0; i < tableArgTypes.Count; i++)
            {
                templist[i] = new List<string>();
            }

            if (OpenConnection(connection))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataRead = cmd.ExecuteReader();

                while (dataRead.Read())
                {
                    for (int i = 0; i < templist.Length; i++)
                    {
                        templist[i].Add(dataRead[tableArgTypes[i]] + "");
                    }
                }
                //end the data read once we have finished reading
                dataRead.Close();
                //close the connection
                CloseConnection(connection);

                //return our string array list
                return templist;
            }
            else
            {
                //return an empty array list
                return templist;
            }

        }
    
        public static List<string> SelectRow(MySqlConnection connection, string table, List<string> tableArgTypes, int id)
        {
            CloseConnection(connection);

            string query = $"SELECT * FROM {table} WHERE idissues = {id.ToString()} LIMIT 1";
            List<string> list = new List<string>();

            if (OpenConnection(connection))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataRead = cmd.ExecuteReader();

                while (dataRead.Read())
                {
                    for(int i = 0; i < tableArgTypes.Count; i++)
                    {
                        list.Add(dataRead[tableArgTypes[i]] + "");
                    }
                    
                }

                dataRead.Close();

                CloseConnection(connection);

                return list;
            }
            else
            {
                return list;
            }

        }

        public static void TruncateTable(MySqlConnection connection, string table)
        {
            string query = $"TRUNCATE TABLE {table};";

            CloseConnection(connection);

            if (OpenConnection(connection))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                CloseConnection(connection);
            }
        }
    }
}