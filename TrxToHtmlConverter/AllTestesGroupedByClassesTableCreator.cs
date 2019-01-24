using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter
{
    class AllTestesGroupedByClassesTableCreator
    {
        public static HtmlDocument CreateAllTestesGroupedByClassesTable(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            var tableTestCase = doc.DocumentNode.SelectSingleNode("/html/body")
                .Element("div").Elements("div").First(d => d.Id == "test")
                .Element("div").Elements("table").First(d => d.Id == "ReportsTable");
            foreach (string testedClass in testLoadResult.AllTestedClasses)
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
                HtmlNode tableNode = HtmlNode.CreateNode("<table></table>");
                HtmlNode theadNode = HtmlNode.CreateNode("<thead></thead>");
                HtmlNode oddNode = HtmlNode.CreateNode("<tr class=\"odd\"></tr>");

                HtmlNodeCollection tableTitlesColletion = testTableTitlesCreator(oddNode);

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

            return doc;
        }

        private static string summaryResultColor(string testedClassName, TestLoadResult testLoadResult)
        {
            var classNameFilter = PredicateCreator(t => t.ClassName, testedClassName);

            var classTests = testLoadResult.tests.Where(classNameFilter);

            int classTestsCount = classTests.Count();

            int passedTestsCount = classTests.Where(PredicateCreator(t => t.Result, "Passed")).Count();

            if (passedTestsCount == classTestsCount)
                return "column1Passed";
            else if (classTests.Where(PredicateCreator(t => t.Result, "Failed")).Count() != 0)
                return "column1Failed";
            else if (classTests.Where(PredicateCreator(t => t.Result, "Warning")).Count() != 0)
                return "column1Warning";
            return "column1Inconclusive";
        }

        private static Func<Test, bool> PredicateCreator<T>(Func<Test, T> selector, T expected) where T : IEquatable<T>
        {
            return t => selector(t).Equals(expected);
        }

        private static string tagsCreator(string tagName, string content = "")
        {
            return $"<{tagName}>{content}</{tagName}>";
        }

        private static HtmlNodeCollection testTableTitlesCreator(HtmlNode parentNode)
        {
            HtmlNodeCollection columnTitles = new HtmlNodeCollection(parentNode);

            columnTitles.Add(CreateTestTableHeader("Status"));
            columnTitles.Add(CreateTestTableHeader("Test"));
            columnTitles.Add(CreateTestTableHeader("Start Time"));
            columnTitles.Add(CreateTestTableHeader("Duration"));

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
            }


        }

        private static HtmlNode CreateTestTableHeader(string headerContent)
        {
            return HtmlNode.CreateNode($"<th class=\"TestsTableHeader\">{headerContent}</th>");
        }

        private static string CreateColoredResult(string result)
        {
            string color = "";
            switch (result)
            {
                case "Passed": color = "green"; break;
                case "Failed": color = "SaddleBrown"; break;
                case "Inconclusive": color = "BlueViolet"; break;
                case "Warning": color = "DarkGoldenrod"; break;
            }

            return $"<font color={color} size=4><strong>{result}</strong></font><br>";
        }
    }
}
