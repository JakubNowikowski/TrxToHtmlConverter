using HtmlAgilityPack;
using System;
using System.Linq;
using TrxToHtmlConverter.TableBuilder;

namespace TrxToHtmlConverter
{
    public class TableCreator
    {
        #region create tables
        public static HtmlDocument CreateTopTables(HtmlDocument doc, TestLoadResult testLoadResult, string pbiNumber, string changeSetNumber)
        {
            HtmlNode div = HtmlNode.CreateNode("<div id=\"SummaryTables\" class=\"wrap\"></div");

            div.AppendChild(CreateTestInformationTable(doc, pbiNumber, changeSetNumber));
            div.AppendChild(CreateRunTimeSummaryTable(doc, testLoadResult));
            div.AppendChild(CreateTestStatusesTable(doc, testLoadResult));
            div.AppendChild(CreateTestedClassesTable(doc, testLoadResult));
            doc.DocumentNode.SelectSingleNode("/html/body").AppendChild(div);
            return doc;
        }

        private static HtmlNode CreateTestInformationTable(HtmlDocument doc, string pbiNumber, string changeSetNumber)
        {
            Table table = new Table("TestInformationTable", "rightTable", "Test Information");
            Row[] rows = new Row[]
            {
                new Row("", "pbiNumber"),
                new Row("", "changeSetNumber"),
            };
            foreach (Row row in rows)
            {
                string value = "", rowName = "", cellClass = "";
                if (row.id == "pbiNumber") { value = pbiNumber; rowName = "PBI Number"; cellClass = "mainColumn"; }
                if (row.id == "changeSetNumber") { value = changeSetNumber; rowName = "Changeset Number"; cellClass = "mainColumnLastRow"; }

                Cell[] cells = new Cell[]
            {
                new Cell(rowName, cellClass, "", true),
                new Cell(value)
            };
                row.Add(cells);
            }

            table.Add(rows);

            return table.Export();
        }

        private static HtmlNode CreateTestStatusesTable(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            Table table = new Table("testsStatusesTable", "rightTable", "Tests Statuses");
            Row[] rows = new Row[]
            {
                new Row("", "total"),
                new Row("", "passed"),
                new Row("", "failed"),
                new Row("", "inconclusive"),
                new Row("", "warning"),
                new Row("", "not executed")
            };
            foreach (Row row in rows)
            {
                string value = "", cellClass = "";
                if (row.id == "total") { value = testLoadResult.totalTestsProp.Total; cellClass = "mainColumn"; }
                if (row.id == "passed") { value = testLoadResult.totalTestsProp.Passed; cellClass = "mainColumn"; }
                if (row.id == "failed") { value = testLoadResult.totalTestsProp.Failed; cellClass = "mainColumn"; }
                if (row.id == "inconclusive") { value = testLoadResult.totalTestsProp.Inconclusive; cellClass = "mainColumn"; }
                if (row.id == "warning") { value = testLoadResult.totalTestsProp.Warning; cellClass = "mainColumn"; }
                if (row.id == "not executed") { value = (Convert.ToInt32(testLoadResult.totalTestsProp.Total) - Convert.ToInt32(testLoadResult.totalTestsProp.Executed)).ToString(); cellClass = "mainColumnLastRow"; }

                Cell[] cells = new Cell[]
            {
                new Cell(Table.ToUpperFirstLetter(row.id), cellClass, "", true),
                new Cell(value)
            };
                row.Add(cells);
            }

            table.Add(rows);

            return table.Export();
        }

        private static HtmlNode CreateRunTimeSummaryTable(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            Table table = new Table("RunTimeSummaryTable", "leftTable", "Run Time Summary");
            Row[] rows = new Row[]
            {
                new Row("", "classCount"),
                new Row("", "startTime"),
                new Row("", "endTime"),
                new Row("", "duration")
            };
            foreach (Row row in rows)
            {
                string value = "", rowName = "", cellClass = "";
                if (row.id == "classCount") { value = testLoadResult.AllTestedClasses.Count.ToString(); rowName = "Number of tested classes"; cellClass = "mainColumn"; }
                if (row.id == "startTime") { value = testLoadResult.totalTestsProp.StartTime.ToString(); rowName = "Start Time"; cellClass = "mainColumn"; }
                if (row.id == "endTime") { value = testLoadResult.totalTestsProp.FinishTime.ToString(); rowName = "End Time"; cellClass = "mainColumn"; }
                if (row.id == "duration") { value = TestsDuration(testLoadResult.totalTestsProp.StartTime, testLoadResult.totalTestsProp.FinishTime); rowName = "Duration"; cellClass = "mainColumnLastRow"; }

                Cell[] cells = new Cell[]
            {
                new Cell(rowName, cellClass, "", true),
                new Cell(value)
            };
                row.Add(cells);
            }

            table.Add(rows);

            return table.Export();
        }

        private static HtmlNode CreateTestedClassesTable(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            Table table = new Table("TestedClassesTable", "hiddenTable", "Tested Classes");

            foreach (string className in testLoadResult.AllTestedClasses)
            {
                Row row = new Row("", "");
                Cell cell = new Cell(className);
                row.Add(cell);
                table.Add(row);
            }

            return table.Export();
        }

        public static HtmlDocument CreateAllFailedTestsTable(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            Table table = new Table("AllFailedTestsTable", "centerTable", "All Failed Tests");

            Row headRow = new Row("", "");

            Cell[] headRowCells = new Cell[]
            {
                new Cell("", "marginCell", "",false),
                new Cell("", "Failed", "",false),
                new Cell("Failed Tests", "leftAlign","" , false),
                new Cell(testLoadResult.totalTestsProp.Failed, "", "number", false)
            };

            headRow.Add(headRowCells);

            Row contentHeadRow = new Row("", "");
            Cell[] contentHeadRowCells = new Cell[]
            {
                new Cell("Status","TestsTableHeaderFirst","",true),
                new Cell("Test","TestsTableHeader","",true),
                new Cell("Class Name","TestsTableHeader","",true),
                new Cell("Start Time","TestsTableHeader","",true),
                new Cell("Duration","TestsTableHeaderLast","",true),
            };
            contentHeadRow.Add(contentHeadRowCells);

            Table content = new Table("", "showHideTable", contentHeadRow);

            Row contentBodyRow = new Row("", "");
            var failedResults = PredicateCreator(t => t.Result, "Failed");
            foreach (Test test in testLoadResult.tests.Where(PredicateCreator(t => t.Result, "Failed")))
            {
                Row testRow = new Row("Test", test.ID);
                Cell[] testCells = new Cell[]
                {
                new Cell(CreateColoredResult(test.Result), test.Result, "", false),
                new Cell(test.MethodName, "leftAlign", "", false),
                new Cell(test.ClassName, "ClassName", "", false),
                new Cell(DateTime.Parse(test.StartTime.ToString()).ToString(), "StartTime", "", false),
                new Cell(test.Duration, "statusCount", "", false)
                };
                testRow.Add(testCells);
                content.Add(testRow);

                if (test.Message != "")
                {
                    Row msgRow = new Row("Test", test.ID);
                    Cell[] msgCells = new Cell[]
                    {
                new Cell("Message", "messageTitle", "", false),
                new Cell(test.Message, "message", "", false,colSpan: "4")
                    };

                    msgRow.Add(msgCells);
                    content.Add(msgRow);
                }

            }

            ShowHideTableBody tableBody = new ShowHideTableBody(headRow, content);

            doc.DocumentNode.SelectSingleNode("/html/body").AppendChild(table.Export()).AppendChild(tableBody.Export());

            return doc;
        }

        public static HtmlDocument CreateAllTestsGroupedByClassesTable(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            //create main table
            Table table = new Table("ReportsTable", "centerTable", "All Tests Grouped By Classes");

            //create table header row
            Row tableHeadRow = new Row("", "");

            Cell[] tableHeadRowCells = new Cell[]
            {
                new Cell("", "", "",true),
                new Cell("", "", "",true),
                new Cell("Class Name", "summaryClassTests","" , true),
                new Cell("All", "","all" , true),
                new Cell("Passed", "","passed" , true),
                new Cell("Failed", "","failed" , true),
                new Cell("Warning", "","warning" , true),
                new Cell("Inconclusive", "", "inconclusive", true),
                new Cell("Not Executed", "", "not executed", true),
                new Cell("", "", "",true),
                new Cell("", "", "",true)
            };

            tableHeadRow.Add(tableHeadRowCells);
            table.Add(tableHeadRow);

            //create table show-hide rows
            foreach (string testedClass in testLoadResult.AllTestedClasses)
            {
                //create head row for show-hide row
                string resultColor = summaryResultColor(testedClass, testLoadResult);
                Row headRow = new Row("", testedClass);
                Cell[] headRowCells = new Cell[]
                {
                new Cell("", "marginCell", "",false),
                new Cell("", resultColor, "",false ),
                new Cell(testedClass, "leftAlign", "", false),
                new Cell(testLoadResult.tests.Where(c => c.ClassName == testedClass).Count().ToString(),"statusCount","number", false),
                new Cell(testLoadResult.tests.Where(c => c.ClassName == testedClass).Where(t => t.Result == "Passed").Count().ToString(),"statusCount","number", false),
                new Cell(testLoadResult.tests.Where(c => c.ClassName == testedClass).Where(t => t.Result == "Failed").Count().ToString(),"statusCount","number", false),
                new Cell(testLoadResult.tests.Where(c => c.ClassName == testedClass).Where(t => t.Result == "Warning").Count().ToString(),"statusCount","number", false),
                new Cell(testLoadResult.tests.Where(c => c.ClassName == testedClass).Where(t => t.Result == "Inconclusive").Count().ToString(),"statusCount","number", false),
                new Cell(testLoadResult.tests.Where(c => c.ClassName == testedClass).Where(t => t.Result == "NotExecuted").Count().ToString(),"statusCount","number", false),
                new Cell("", "ex", "",false )
                };
                headRow.Add(headRowCells);

                //create container table and its head row
                Row contentHeadRow = new Row("", "");
                Cell[] contentHeadRowCells = new Cell[]
                {
                new Cell("Status","TestsTableHeaderFirst","",true),
                new Cell("Test","TestsTableHeader","",true),
                new Cell("Start Time","TestsTableHeader","",true),
                new Cell("Duration","TestsTableHeaderLast","",true)
                };
                contentHeadRow.Add(contentHeadRowCells);

                Table containerTable = new Table("", "showHideTable", contentHeadRow);

                //create container table body
                foreach (Test test in testLoadResult.tests.Where(PredicateCreator(t => t.ClassName, testedClass)))
                {
                    Row testRow = new Row("Test", test.ID);
                    Cell[] testCells = new Cell[]
                    {
                new Cell(CreateColoredResult(test.Result), test.Result, "", false),
                new Cell(test.MethodName, "leftAlign", "", false),
                new Cell(DateTime.Parse(test.StartTime.ToString()).ToString(), "StartTime", "", false),
                new Cell(test.Duration, "statusCount", "", false)
                    };

                    testRow.Add(testCells);
                    containerTable.Add(testRow);

                    if (test.Message != "")
                    {
                        Row msgRow = new Row("Test", test.ID);
                        Cell[] msgCells = new Cell[]
                        {
                new Cell("Message:", "messageTitle", "", false),
                new Cell(test.Message, "message", "", false,colSpan: "3")
                        };

                        msgRow.Add(msgCells);
                        containerTable.Add(msgRow);
                    }
                }

                ShowHideTableBody tableBody = new ShowHideTableBody(headRow, containerTable);
                table.Add(tableBody);

            }
            doc.DocumentNode.SelectSingleNode("/html/body").AppendChild(table.Export());
            return doc;
        }
        #endregion

        #region additional methods
        private static string summaryResultColor(string testedClassName, TestLoadResult testLoadResult)
        {
            var classNameFilter = PredicateCreator(t => t.ClassName, testedClassName);

            var classTests = testLoadResult.tests.Where(classNameFilter);

            int classTestsCount = classTests.Count();

            int passedTestsCount = classTests.Where(PredicateCreator(t => t.Result, "Passed")).Count();

            if (passedTestsCount == classTestsCount)
                return "Passed";
            else if (classTests.Where(PredicateCreator(t => t.Result, "Failed")).Count() != 0)
                return "Failed";
            else if (classTests.Where(PredicateCreator(t => t.Result, "Warning")).Count() != 0)
                return "Warning";
            return "Inconclusive";
        }

        private static Func<Test, bool> PredicateCreator<T>(Func<Test, T> selector, T expected) where T : IEquatable<T>
        {
            return t => selector(t).Equals(expected);
        }

        private static string TestsDuration(DateTime startTime, DateTime stopTime)
        {
            return string.Format("{0:hh\\:mm\\:ss\\.fff}", (stopTime - startTime));
        }

        private static string CreateColoredResult(string result)
        {
            string color = "";
            switch (result)
            {
                case "Passed": color = "green"; break;
                case "Failed": color = "SaddleBrown"; break;
                case "Inconclusive": color = "green"; break;
                case "Warning": color = "DarkGoldenrod"; break;
                case "NotExecuted": color = "DarkGoldenrod"; break;
            }

            return $"<font color={color} size=4><strong>{result}</strong></font><br>";
        }
        #endregion
    }
}
