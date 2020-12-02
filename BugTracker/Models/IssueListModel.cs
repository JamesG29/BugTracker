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
        public bool IsSortingSeverity { get; set; }
        public bool SortingSeverityDescending { get; set; }
        public bool IsSortingDate { get; set; }
        public bool SortingDateDescending { get; set; }
        public bool isSortingCost { get; set; }
        public bool SortingCostDescending { get; set; }
        public bool IsSortingLocation { get; set; }
        public bool SortingLocationDescending { get; set; }
    }
}