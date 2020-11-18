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
            return View();
        }

        public ActionResult List()
        {
            List<Issue> issues = new List<Issue>();

            List<string>[] values;
            MySqlTableModel issueTable = SqlTables.Issues;

            values = MySqlCrud.SelectAllRows(SqlConnections.IssueWebsite, issueTable.Table, issueTable.Collumns);
            /*
            idIssues DEFAULT(int)
            project name
            severity string
            datediscovered string  XX/XX/XXXX
            time discovered string  XX:XX:XX
            projectedmanhours float
            projectedcost float
            shortdescription string
            location string
            popularity int
            description string
            */

            int r = 0;
            foreach (var i in values[0])
            {

                int issueId;
                Int32.TryParse(values[0][r], out issueId);

                string project = values[1][r]; //100 MaxChars
                string severity = values[2][r]; //45 MaxChars
                string datediscovered = values[3][r]; //45 MaxChars
                string timediscovered = values[4][r]; //45 MaxChars

                int pmhTemp;
                Int32.TryParse(values[5][r], out pmhTemp);
                float projectedmanhours = (float)pmhTemp;

                int pcTemp;
                Int32.TryParse(values[6][r], out pcTemp);
                float projectedcost = (float)pcTemp;

                string shortdescription = values[7][r]; //100 MaxChars
                string location = values[8][r]; //45 MaxChars

                int popularity;
                Int32.TryParse(values[9][r], out popularity);

                string description = values[10][r]; //300 MaxChars


                Issue ish = new Issue()
                {
                    Project = project,
                    Severity = severity,
                    DateDiscovered = datediscovered,
                    TimeDiscovered = timediscovered,
                    ProjectedManHours = projectedmanhours,
                    ProjectedCost = projectedcost,
                    ShortDescription = shortdescription,
                    Location = location,
                    Popularity = popularity,
                    Description = description

                };

                issues.Add(ish);
                r++;
            }


            IssueListModel IssueListModel = new IssueListModel()
            {
                Issues = issues
            };

            return View(IssueListModel);
        }

        [HttpPost]
        public ActionResult List(string project, string severity, string datediscovered,
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
    }
}