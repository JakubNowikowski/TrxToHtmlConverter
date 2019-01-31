using HtmlAgilityPack;
using System;
using System.Linq;
using TrxToHtmlConverter.TableBuilder;

namespace TrxToHtmlConverter
{
    class AllFailedTests : TableCreator
    {
        public static HtmlDocument CreateTable(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            Table table = new Table("AllFailedTestsTable", "centerTable", "All Failed Tests");

            Row headRow = new Row("","");

            Cell[] headRowCells = new Cell[]
            {
                new Cell("columnFailed", "", false,""),
                new Cell("failedTest", "" ,false, "Failed Tests"),
                new Cell("failedTest", "number", false, testLoadResult.totalTestsProp.Failed)
            };

            headRow.Add(headRowCells);
            
            Row contentHeadRow = new Row("","");
            Cell[] contentHeadRowCells = new Cell[]
            {
                new Cell("TestsTableHeaderFirst","",true,"Status"),
                new Cell("TestsTableHeader","",true,"Test"),
                new Cell("TestsTableHeader","",true,"Class Name"),
                new Cell("TestsTableHeader","",true,"Start Time"),
                new Cell("TestsTableHeaderLast","",true,"Duration"),
            };
            contentHeadRow.Add(contentHeadRowCells);
            
            Table content = new Table("", "centerTable", contentHeadRow);

            Row contentBodyRow = new Row("","");
            var failedResults = PredicateCreator(t => t.Result, "Failed");
            foreach (Test test in testLoadResult.tests.Where(PredicateCreator(t => t.Result, "Failed")))
            {
                Row testRow = new Row("Test", test.ID);
                Cell[] testCells = new Cell[]
                {
                new Cell(test.Result, "", false, CreateColoredResult(test.Result)),
                new Cell("Function", "", false, test.MethodName),
                new Cell("ClassName", "", false, test.ClassName),
                new Cell("StartTime", "", false, DateTime.Parse(test.StartTime.ToString()).ToString()),
                new Cell("statusCount", "", false, test.Duration)
                };
                testRow.Add(testCells);
                content.Add(testRow);
            }
            
            ShowHideTableBody tableBody = new ShowHideTableBody(headRow, content);

            doc.DocumentNode.SelectSingleNode("/html/body").AppendChild(table.cellNode).AppendChild(tableBody.cellNode);

            return doc;
        }
    }
}
