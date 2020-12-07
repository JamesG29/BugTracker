using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class IssueListSorts
    {
        public static List<Issue> SortBySeverity(List<Issue> list, bool descending = true)
        {
            if (descending)
            {
                list.Sort(CompareIssueBySeverityDescending);
            }
            else
            {
                list.Sort(CompareIssueBySeverityAscending);
            }


            return list;
        }
        public static List<Issue> SortByDate(List<Issue> list, bool descending = true)
        {
            if (descending)
            {
                list.Sort(CompareIssueByDateDescending);
            }
            else
            {
                list.Sort(CompareIssueByDateAscending);
            }

            return list;
        }
        public static List<Issue> SortByCost(List<Issue> list, bool descending = true)
        {
            if (descending)
            {
                list.Sort(CompareIssueByCostDescending);
            }
            else
            {
                list.Sort(CompareIssueByCostAscending);
            }

            return list;
        }
        public static List<Issue> SortByLocation(List<Issue> list, bool descending = true)
        {
            if (descending)
            {
                list.Sort(CompareIssueByLocationDescending);
            }
            else
            {
                list.Sort(CompareIssueByLocationAscending);
            }

            return list;
        }



        //Severities are currently inclusive of: "Low", "Medium" and "High"
        public static int CompareIssueBySeverityAscending(Issue x, Issue y)
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
        public static int CompareIssueBySeverityDescending(Issue x, Issue y)
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

        //Here Ascending order refers to going from oldest to newest
        public static int CompareIssueByDateAscending(Issue x, Issue y)
        {

            string xMonth = "";
            string xDay = "";
            string xYear = "";

            string yMonth = "";
            string yDay = "";
            string yYear = "";

            if(x.DateDiscovered != null)
            {
                xMonth = x.DateDiscovered.Substring(0, 2);
                xDay = x.DateDiscovered.Substring(3, 2);
                xYear = x.DateDiscovered.Substring(6, 4);
            }

            if(y.DateDiscovered != null)
            {
                yMonth = y.DateDiscovered.Substring(0, 2);
                yDay = y.DateDiscovered.Substring(3, 2);
                yYear = y.DateDiscovered.Substring(6, 4);
            }

            int xMonthNum;
            Int32.TryParse(xMonth, out xMonthNum);
            int xDayNum;
            Int32.TryParse(xDay, out xDayNum);
            int xYearNum;
            Int32.TryParse(xYear, out xYearNum);

            int yMonthNum;
            Int32.TryParse(yMonth, out yMonthNum);
            int yDayNum;
            Int32.TryParse(xDay, out yDayNum);
            int yYearNum;
            Int32.TryParse(yYear, out yYearNum);

            if (xYearNum == yYearNum)
            {
                if (xMonthNum == yMonthNum)
                {
                    if (xDayNum == yDayNum)
                    {
                        return 0;
                    }
                    else
                    {
                        if (xDayNum > yDayNum)
                        {
                            return -1;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
                else //month !=
                {
                    if(xMonthNum > yMonthNum)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            else // year !=
            {
                if(xYearNum > yYearNum)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }


        }
        //Here Descending order refers to going from newest to oldest
        public static int CompareIssueByDateDescending(Issue x, Issue y)
        {

            string xMonth = "";
            string xDay = "";
            string xYear = "";

            string yMonth = "";
            string yDay = "";
            string yYear = "";

            if (x.DateDiscovered != null)
            {
                xMonth = x.DateDiscovered.Substring(0, 2);
                xDay = x.DateDiscovered.Substring(3, 2);
                xYear = x.DateDiscovered.Substring(6, 4);
            }

            if (y.DateDiscovered != null)
            {
                yMonth = y.DateDiscovered.Substring(0, 2);
                yDay = y.DateDiscovered.Substring(3, 2);
                yYear = y.DateDiscovered.Substring(6, 4);
            }

            int xMonthNum;
            Int32.TryParse(xMonth, out xMonthNum);
            int xDayNum;
            Int32.TryParse(xDay, out xDayNum);
            int xYearNum;
            Int32.TryParse(xYear, out xYearNum);

            int yMonthNum;
            Int32.TryParse(yMonth, out yMonthNum);
            int yDayNum;
            Int32.TryParse(xDay, out yDayNum);
            int yYearNum;
            Int32.TryParse(yYear, out yYearNum);

            if (xYearNum == yYearNum)
            {
                if (xMonthNum == yMonthNum)
                {
                    if (xDayNum == yDayNum)
                    {
                        return 0;
                    }
                    else
                    {
                        if (xDayNum > yDayNum)
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                else //month !=
                {
                    if (xMonthNum > yMonthNum)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            else // year !=
            {
                if (xYearNum > yYearNum)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }


        }

        public static int CompareIssueByCostAscending(Issue x, Issue y)
        {
            float xCost = x.ProjectedCost;
            float yCost = y.ProjectedCost;

            return -1*(xCost.CompareTo(yCost));


        }
        public static int CompareIssueByCostDescending(Issue x, Issue y)
        {
            float xCost = x.ProjectedCost;
            float yCost = y.ProjectedCost;

            return xCost.CompareTo(yCost);
        }

        //Ascendinger here refers to alphabetically order
        public static int CompareIssueByLocationAscending(Issue x, Issue y)
        {
            //x's Location
            string xLoc = x.Location;
            //y's Location
            string yLoc = y.Location;

            bool xLarger;
            bool xyEqual = false;

            if(xLoc.Length > yLoc.Length)
            {
                xLarger = true;
            }
            else if(xLoc.Length < yLoc.Length)
            {
                xLarger = false;
            }
            else
            {
                xLarger = false;
                xyEqual = true;
            }

            int itterations = 1;
            //we set itterations to lower length as to avoid runtime errors
            if (xLarger)
            {
                itterations = yLoc.Length;
            }
            else if(!xLarger)
            {
                itterations = xLoc.Length;
            }

            for(int i = 0; i < itterations; i++)
            {
                if(xLoc[i] > yLoc[i])
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }

            //If all letters are equivelant up to one locations length, we give it
            //to the larger location name
            if (xLarger)
            {
                return 1;
            }
            else if(!xLarger && !xyEqual)
            {
                return -1;
            }
            else //in this case the two locations are identical
            {
                return 0;
            }

        }
        public static int CompareIssueByLocationDescending(Issue x, Issue y)
        {
            //x's Location
            string xLoc = x.Location;
            //y's Location
            string yLoc = y.Location;

            bool xLarger;
            bool xyEqual = false;

            if (xLoc.Length > yLoc.Length)
            {
                xLarger = true;
            }
            else if (xLoc.Length < yLoc.Length)
            {
                xLarger = false;
            }
            else
            {
                xLarger = false;
                xyEqual = true;
            }

            int itterations = 1;
            //we set itterations to lower length as to avoid runtime errors
            if (xLarger)
            {
                itterations = yLoc.Length;
            }
            else if (!xLarger)
            {
                itterations = xLoc.Length;
            }

            for (int i = 0; i < itterations; i++)
            {
                if (xLoc[i] > yLoc[i])
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }

            //If all letters are equivelant up to one locations length, we give it
            //to the larger location name
            if (xLarger)
            {
                return -1;
            }
            else if (!xLarger && !xyEqual)
            {
                return 1;
            }
            else //in this case the two locations are identical
            {
                return 0;
            }

        }

        public static List<Issue> CreateTypicalIssueList(bool fromOrdered = false)
        {
            List<Issue> issues = new List<Issue>();

            List<string>[] values;
            MySqlTableModel issueTable = SqlTables.Issues;

            if (!fromOrdered)
            {
                values = MySqlCrud.SelectAllRows(SqlConnections.IssueWebsite, issueTable.Table, issueTable.Collumns);
            }
            else
            {
                values = MySqlCrud.SelectAllRows(SqlConnections.IssueWebsite, "issuesordered", issueTable.Collumns);
            }
            
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