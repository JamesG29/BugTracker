using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Issue
    {
        public string IssueId { get; set; }
        public string Project { get; set; }
        public string Severity { get; set; }
        public string DateDiscovered { get; set; }
        public string TimeDiscovered { get; set; }
        public float ProjectedManHours { get; set; }
        public float ProjectedCost { get; set; }
        public string ShortDescription { get; set; }
        public string Location { get; set; }
        public int Popularity { get; set; }
        public string Description { get; set; }
    }
}