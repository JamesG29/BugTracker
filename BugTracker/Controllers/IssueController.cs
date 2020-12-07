using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using MySql.Data.MySqlClient;

namespace BugTracker.Controllers
{
    public class IssueController : Controller
    {
        // GET: Issue
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {

            List<Issue> issues = IssueListSorts.CreateTypicalIssueList();

            //first we clear the table for initializing
            MySqlCrud.TruncateTable(SqlConnections.IssueWebsite, "issuesordered");
            //keep track of new id(primary key)
            int L = 1;

            foreach (Issue issue in issues)
            {
                List<string> arguments = new List<string>()
                {
                    L.ToString(),
                    issue.Project,
                    issue.Severity,
                    issue.DateDiscovered,
                    issue.TimeDiscovered,
                    issue.ProjectedManHours.ToString(),
                    issue.ProjectedCost.ToString(),
                    issue.ShortDescription,
                    issue.Location,
                    issue.Popularity.ToString(),
                    issue.Description,
                    issue.IssueId
                };

                L++;

                //we now assign values to the table based off of default issue order
                MySqlCrud.InsertRow(SqlConnections.IssueWebsite, "issuesordered",
                    SqlTables.IssuesOrdered.Collumns, arguments);
            }

            IssueListModel IssueListModel = new IssueListModel()
            {
                Issues = issues,
                IsAdding = false,
            };

            return View(IssueListModel);
        }


        //instantiate the List ActionResult with IsAdding as true
        public ActionResult ListAdding()
        {
            List<Issue> issues = IssueListSorts.CreateTypicalIssueList(true);

            IssueListModel IssueListModel = new IssueListModel()
            {
                Issues = issues,
                IsAdding = true
            };

            return View("List", IssueListModel);
        }

        //this is where we add to the list(done by useing a partial view in the list view)
        //and then call the base list again
        [HttpPost]
        public ActionResult ListAdding(string project, string severity, string datediscovered,
            string timediscovered, int projectedmanhours, string shortdescription,
            string location, string description)
        {
            List<string> newArgs = new List<string>();

            MySqlTableModel issueTable = SqlTables.Issues;

            //projectedmanhours
            string pmh = projectedmanhours.ToString();
            //projectedcost
            float tempPc = projectedmanhours * 20;
            string pc = tempPc.ToString();

            newArgs = new List<string>()
            {
               "DEFAULT",
                project,
                severity,
                datediscovered,
                timediscovered,
                pmh,
                pc,
                shortdescription,
                location,
                "DEFAULT",
                description
            };



            MySqlCrud.InsertRow(SqlConnections.IssueWebsite, issueTable.Table,
                issueTable.Collumns, newArgs);

            return RedirectToAction("List");
        }

        //values set to a 0 here are false, where as 1 is true and 2 is true ascending
        //sev = severity, 
       // [Route("Issue/SortList/{sev:regex(\\d{1}):range(0,2)}/{date:regex(\\d{1}):range(0,2)}" +
         //   "/{cost:regex(\\d{1}):range(0,2)}/{location:regex(\\d{1}):range(0,2)}")]
        public ActionResult SortList(int? sev = 0, int? date = 0, int? cost = 0,
            int? location = 0)
        {
            List<Issue> issues = IssueListSorts.CreateTypicalIssueList(true);

            bool sevOrdered = false;
            bool sevDesc = true;
            bool dateOrdered = false;
            bool dateDesc = true;
            bool costOrdered = false;
            bool costDesc = true;
            bool locOrdered = false;
            bool locDesc = true;

            //Here we decide how to sort the current issue list
            //we also decide here issueListModel traits
            if(sev >= 1)
            {
                sevOrdered = true;
            }
            switch (sev)
            {
                case 1:
                    issues = IssueListSorts.SortBySeverity(issues);
                    sevDesc = true;
                    break;
                case 2:
                    issues = IssueListSorts.SortBySeverity(issues, false);
                    sevDesc = false;
                    break;
                case 3:
                    sevDesc = true;
                    break;
                case 4:
                    sevDesc = false;
                    break;
            }

            if(date >= 1)
            {
                dateOrdered = true;
            }
            switch (date)
            {
                case 1:
                    issues = IssueListSorts.SortByDate(issues);
                    dateDesc = true;
                    break;
                case 2:
                    issues = IssueListSorts.SortByDate(issues, false);
                    dateDesc = false;
                    break;
                case 3:
                    dateDesc = true;
                    break;
                case 4:
                    dateDesc = false;
                    break;
            }

            if(cost >= 1)
            {
                costOrdered = true;
            }
            switch (cost)
            {
                case 1:
                    issues = IssueListSorts.SortByCost(issues);
                    costDesc = true;
                    break;
                case 2:
                    issues = IssueListSorts.SortByCost(issues, false);
                    costDesc = false;
                    break;
                case 3:
                    costDesc = true;
                    break;
                case 4:
                    costDesc = false;
                    break;
            }

            if(location >= 1)
            {
                locOrdered = true;
            }
            switch (location)
            {
                case 1:
                    issues = IssueListSorts.SortByLocation(issues);
                    break;
                case 2:
                    issues = IssueListSorts.SortByLocation(issues, false);
                    break;
                case 3:
                    locDesc = true;
                    break;
                case 4:
                    locDesc = false;
                    break;
            }

            //first we clear the table for initializing
            MySqlCrud.TruncateTable(SqlConnections.IssueWebsite, "issuesordered");
            //new ids
            int L = 1;
            //Add all of the issues, now sorted, back to the issuesordered table
            foreach (Issue issue in issues)
            {
                List<string> arguments = new List<string>()
                {
                    L.ToString(),
                    issue.Project,
                    issue.Severity,
                    issue.DateDiscovered,
                    issue.TimeDiscovered,
                    issue.ProjectedManHours.ToString(),
                    issue.ProjectedCost.ToString(),
                    issue.ShortDescription,
                    issue.Location,
                    issue.Popularity.ToString(),
                    issue.Description,
                    issue.IssueId
                };

                L++;

                //we now assign values to the table based off of our sort
                MySqlCrud.InsertRow(SqlConnections.IssueWebsite, "issuesordered",
                   SqlTables.IssuesOrdered.Collumns, arguments);
            }
            

            IssueListModel issueListModel = new IssueListModel()
            {
                Issues = issues,
                IsAdding = false,
                IsSortingSeverity = sevOrdered,
                SortingSeverityDescending = sevDesc,
                IsSortingCost = costOrdered,
                SortingCostDescending = costDesc,
                IsSortingDate = dateOrdered,
                SortingDateDescending = dateDesc,
                IsSortingLocation = locOrdered,
                SortingLocationDescending = locDesc
            };

            

            return View("List", issueListModel);
        }


        
    }
}