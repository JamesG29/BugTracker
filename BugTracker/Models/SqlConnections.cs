using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace BugTracker.Models
{
    public class SqlConnections
    {
        public static MySqlConnection IssueWebsite = new MySqlConnection("SERVER=localhost;DATABASE=issuewebsite;UID=root;PASSWORD=");
    }
}