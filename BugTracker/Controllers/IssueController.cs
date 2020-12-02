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
            List<Issue> issues = IssueListSorts.CreateTypicalIssueList();

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
            string timediscovered, int projectedmanhours,string shortdescription,
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

        
        [HttpPost]
        public ActionResult SortList()
        {
            return View();
        }


        
    }
}