using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class IssueListSorts
    {
        public static List<Issue> SortBySeverity(List<Issue> list, bool reverse = false)
        {
            if (reverse)
            {
                list.Sort(CompareIssueBySeverityReverse);
            }
            else
            {
                list.Sort(CompareIssueBySeverity);
            }


            return list;
        }

        public static int CompareIssueBySeverity(Issue x, Issue y)
        {
            //Null severities are considered less severe than those with any severity
            if(x == null)
            {
                if(y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if(y == null)
                {
                    return 1;
                }
                else
                {
                    if(y.Severity == x.Severity)
                    {
                        return 0;
                    }
                    else
                    {
                        if(x.Severity == "Low")
                        {
                            return 1;
                        }

                        if(y.Severity == "Low")
                        {
                            return -1;
                        }

                        //we now know that neither are Low

                        if(x.Severity == "Medium")
                        {
                            return 1;
                        }

                        if(y.Severity == "Medium")
                        {
                            return -1;
                        }

                        return 0;
                      
                    }

                }
            }
        }
        public static int CompareIssueBySeverityReverse(Issue x, Issue y)
        {
            //Null severities are considered less severe than those with any severity
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (y == null)
                {
                    return -1;
                }
                else
                {
                    if (y.Severity == x.Severity)
                    {
                        return 0;
                    }
                    else
                    {
                        if (x.Severity == "Low")
                        {
                            return -1;
                        }

                        if (y.Severity == "Low")
                        {
                            return 1;
                        }

                        //we now know that neither are Low

                        if (x.Severity == "Medium")
                        {
                            return -1;
                        }

                        if (y.Severity == "Medium")
                        {
                            return 1;
                        }

                        return 0;

                    }

                }
            }
        }

        public static List<Issue> CreateTypicalIssueList()
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
                /*
                int issueId;
                Int32.TryParse(values[0][r], out issueId);
                */

                string issueId = values[0][r];

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
                    IssueId = issueId,
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

            return issues;
        }
    }
}