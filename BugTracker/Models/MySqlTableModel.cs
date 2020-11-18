using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class MySqlTableModel
    {
        public string Server { get; set; }
        public string Table { get; set; }
        public List<string> Collumns { get; set; }
    }
}