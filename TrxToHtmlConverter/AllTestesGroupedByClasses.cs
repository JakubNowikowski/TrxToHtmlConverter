using HtmlAgilityPack;
using System;
using System.Linq;
using TrxToHtmlConverter.TableBuilder;

namespace TrxToHtmlConverter
{
    class AllTestesGroupedByClasses : TableCreator
    {
        //public static HtmlDocument CreateTable(HtmlDocument doc, TestLoadResult testLoadResult)
        //{
        //    var tableTestCase = doc.DocumentNode.SelectSingleNode("/html/body")
        //        .Elements("table").First(d => d.Id == "ReportsTable");
        //    foreach (string testedClass in testLoadResult.AllTestedClasses)
        //    {
        //        CreateTableNodes(doc, testLoadResult, testedClass, tableTestCase);
        //    }
        //    return doc;
        //}

        public static HtmlDocument CreateTable(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            //create main table
            Table table = new Table("ReportsTable", "centerTable", "All Tests Grouped By Classes");

            //create table header row
            Row tableHeadRow = new Row("", "");

            Cell[] tableHeadRowCells = new Cell[]
            {
                new Cell("", "", true,""),
                new Cell("summaryClassTests", "" ,true, "Class Name"),
                new Cell("", "all" ,true, "All"),
                new Cell("", "passed" ,true, "Passed"),
                new Cell("", "failed" ,true, "Failed"),
                new Cell("", "warning" ,true, "Warning"),
                new Cell("", "inconclusive", true, "Inconclusive"),
                new Cell("", "", true,""),
                new Cell("", "", true,"")
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
                new Cell(resultColor, "", false ,""),
                new Cell("Fuction", "", false, testedClass),
                new Cell("statusCount","number",false, testLoadResult.tests.Where(c => c.ClassName == testedClass).Count().ToString()),
                new Cell("statusCount","number",false, testLoadResult.tests.Where(c => c.ClassName == testedClass).Where(t => t.Result == "Passed").Count().ToString()),
                new Cell("statusCount","number",false, testLoadResult.tests.Where(c => c.ClassName == testedClass).Where(t => t.Result == "Failed").Count().ToString()),
                new Cell("statusCount","number",false, testLoadResult.tests.Where(c => c.ClassName == testedClass).Where(t => t.Result == "Warning").Count().ToString()),
                new Cell("statusCount","number",false, testLoadResult.tests.Where(c => c.ClassName == testedClass).Where(t => t.Result == "Inconclusive").Count().ToString()),
                new Cell("ex", "", false ,"")
                };
                headRow.Add(headRowCells);

                //create container table and its head row
                Row contentHeadRow = new Row("", "");
                Cell[] contentHeadRowCells = new Cell[]
                {
                new Cell("TestsTableHeaderFirst","",true,"Status"),
                new Cell("TestsTableHeader","",true,"Test"),
                new Cell("TestsTableHeader","",true,"Start Time"),
                new Cell("TestsTableHeaderLast","",true,"Duration")
                };
                contentHeadRow.Add(contentHeadRowCells);

                Table containerTable = new Table("", "showHideTable", contentHeadRow);

                //create container table body
                foreach (Test test in testLoadResult.tests.Where(PredicateCreator(t => t.ClassName, testedClass)))
                {
                    Row testRow = new Row("Test", test.ID);
                    Cell[] testCells = new Cell[]
                    {
                new Cell(test.Result, "", false, CreateColoredResult(test.Result)),
                new Cell("Function", "", false, test.MethodName),
                new Cell("StartTime", "", false, DateTime.Parse(test.StartTime.ToString()).ToString()),
                new Cell("statusCount", "", false, test.Duration)
                    };
                    testRow.Add(testCells);
                    containerTable.Add(testRow);
                }

                ShowHideTableBody tableBody = new ShowHideTableBody(headRow, containerTable);
                table.Add(tableBody);

            }
            doc.DocumentNode.SelectSingleNode("/html/body").AppendChild(table.cellNode);
            return doc;
        }

        private static void CreateTableNodes(HtmlDocument doc, TestLoadResult testLoadResult, string testedClass, HtmlNode tableTestCase)
        {
            string resultColor = summaryResultColor(testedClass, testLoadResult);
            HtmlNode tbodyNode = HtmlNode.CreateNode(tagsCreator("tbody"));
            HtmlNode headerNode = HtmlNode.CreateNode($"<tr id=\"{testedClass}\"></tr>");
            HtmlNode colummTdNode = HtmlNode.CreateNode($"<td class=\"{resultColor}\"></td>");

            HtmlNode functionNode = HtmlNode.CreateNode($"<td class=\"Function\">{testedClass}</td>");
            HtmlNode allNumberNode = HtmlNode.CreateNode($"<td id=\"number\" class=\"statusCount\" name=\"Id\">{testLoadResult.tests.Where(c => c.ClassName == testedClass).Count()}</td>");
            HtmlNode passedNumberNode = HtmlNode.CreateNode($"<td id=\"number\" class=\"statusCount\" name=\"Id\">{testLoadResult.tests.Where(c => c.ClassName == testedClass).Where(t => t.Result == "Passed").Count()}</td>");
            HtmlNode failedNumberNode = HtmlNode.CreateNode($"<td id=\"number\" class=\"statusCount\" name=\"Id\">{testLoadResult.tests.Where(c => c.ClassName == testedClass).Where(t => t.Result == "Failed").Count()}</td>");
            HtmlNode warningNumberNode = HtmlNode.CreateNode($"<td id=\"number\" class=\"statusCount\" name=\"Id\">{testLoadResult.tests.Where(c => c.ClassName == testedClass).Where(t => t.Result == "Warning").Count()}</td>");
            HtmlNode inconclusiveNumberNode = HtmlNode.CreateNode($"<td id=\"number\" class=\"statusCount\" name=\"Id\">{testLoadResult.tests.Where(c => c.ClassName == testedClass).Where(t => t.Result == "Inconclusive").Count()}</td>");



            HtmlNode exNode = HtmlNode.CreateNode("<td class=\"ex\"></td>");
            HtmlNode openMoreButtonNode = HtmlNode.CreateNode($"<div class=\"OpenMoreButton\" onclick=\"ShowHide('{testedClass}TestsContainer', '{testedClass}Button', 'Show Tests', 'Hide Tests'); \"></div>");
            HtmlNode moreButtonNode = HtmlNode.CreateNode($"<div class=\"MoreButtonText\" id=\"{testedClass}Button\">Show Tests</div>");

            HtmlNode rowsNode = HtmlNode.CreateNode($"<tr id=\"{testedClass}TestsContainer\" class=\"hiddenRow\"></tr>");
            HtmlNode colSpanNode = HtmlNode.CreateNode("<td colspan=\"8\"></td>");
            HtmlNode arrowNode = HtmlNode.CreateNode("<div id=\"exceptionArrow\">↳</div>");
            HtmlNode tableNode = HtmlNode.CreateNode($"<table id=\"{testedClass}\"></table>");
            HtmlNode theadNode = HtmlNode.CreateNode("<thead></thead>");
            HtmlNode oddNode = HtmlNode.CreateNode("<tr class=\"odd\"></tr>");

            HtmlNodeCollection tableTitlesColletion = testTableTitlesCreator(oddNode, testedClass);

            HtmlNode tbodyTestsNode = HtmlNode.CreateNode("<tbody></tbody>");

            var classPredicate = PredicateCreator(c => c.ClassName, testedClass);
            CreateTestCaseTableRows(tbodyTestsNode, classPredicate, testLoadResult);



            headerNode.AppendChild(colummTdNode);
            headerNode.AppendChild(functionNode);
            headerNode.AppendChild(allNumberNode);
            headerNode.AppendChild(passedNumberNode);
            headerNode.AppendChild(failedNumberNode);
            headerNode.AppendChild(warningNumberNode);
            headerNode.AppendChild(inconclusiveNumberNode);

            openMoreButtonNode.AppendChild(moreButtonNode);
            exNode.AppendChild(openMoreButtonNode);
            headerNode.AppendChild(exNode);
            tbodyNode.AppendChild(headerNode);

            oddNode.AppendChildren(tableTitlesColletion);
            theadNode.AppendChild(oddNode);

            tableNode.AppendChild(theadNode);
            tableNode.AppendChild(tbodyTestsNode);
            colSpanNode.AppendChild(arrowNode);
            colSpanNode.AppendChild(tableNode);
            rowsNode.AppendChild(colSpanNode);
            tbodyNode.AppendChild(rowsNode);

            tableTestCase.ChildNodes.Append(tbodyNode);
        }

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

        private static string tagsCreator(string tagName, string content = "")
        {
            return $"<{tagName}>{content}</{tagName}>";
        }

        private static HtmlNodeCollection testTableTitlesCreator(HtmlNode parentNode, string testedClass)
        {
            HtmlNodeCollection columnTitles = new HtmlNodeCollection(parentNode);

            columnTitles.Add(CreateTestTableHeader(1, "Status", testedClass));
            columnTitles.Add(CreateTestTableHeader(2, "Test", testedClass));
            columnTitles.Add(CreateTestTableHeader(3, "Start Time", testedClass));
            columnTitles.Add(CreateTestTableHeader(4, "Duration", testedClass));

            return columnTitles;

        }

        private static void CreateTestCaseTableRows(HtmlNode tableTestCase, Func<Test, bool> func, TestLoadResult testLoadResult)
        {
            foreach (Test test in testLoadResult.tests.Where(func))
            {
                HtmlNode tableRowTestCase = HtmlNode.CreateNode($"<tr id=\"{test.ID}\"class=\"Test\"></tr>");

                tableTestCase.AppendChild(tableRowTestCase);
                tableTestCase = tableTestCase.LastChild;

                tableRowTestCase = HtmlNode.CreateNode($"<td class=\"{test.Result}\">{CreateColoredResult(test.Result)}</td>");
                tableTestCase.AppendChild(tableRowTestCase);

                tableRowTestCase = HtmlNode.CreateNode($"<td class=\"Function\">{test.MethodName}</td>");
                tableTestCase.AppendChild(tableRowTestCase);

                tableRowTestCase = HtmlNode.CreateNode($"<td class=\"StartTime\">{DateTime.Parse(test.StartTime.ToString())}</td>");
                tableTestCase.AppendChild(tableRowTestCase);

                tableRowTestCase = HtmlNode.CreateNode($"<td class=\"Duration\">{test.Duration}</td>");
                tableTestCase.AppendChild(tableRowTestCase);

                tableTestCase = tableTestCase.ParentNode;
            }


        }

        private static HtmlNode CreateTestTableHeader(int headerNumber, string headerContent, string testedClass)
        {
            
            return HtmlNode.CreateNode($"<th onclick=\"sortTable({headerNumber}, '" + testedClass + $"')\" class=\"TestsTableHeader\">{headerContent}</th>");
        }
    }
}
