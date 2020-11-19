using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class IssueListModel
    {
        public List<Issue> Issues { get; set; }
        public bool IsAdding { get; set; }
    }
}