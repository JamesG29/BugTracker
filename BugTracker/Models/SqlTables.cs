using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class SqlTables
    {
        public static MySqlTableModel Issues = new MySqlTableModel()
        {
            Server = "localhost",
            Table = "issues",
            Collumns = new List<string>()
            {
                "idissues",
                "project",
                "severity",
                "datediscovered",
                "timediscovered",
                "projectedmanhours",
                "projectedcost",
                "shortdescription",
                "location",
                "popularity",
                "description"
            }
        };
    }
}