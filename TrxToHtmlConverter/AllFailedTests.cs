using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter
{
    class AllFailedTests : TableCreator
    {
        public static HtmlDocument CreateTable(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            var tableTestCase = doc.DocumentNode.SelectSingleNode("/html/body")
                .Element("div").Elements("div").First(d => d.Id == "test")
                .Element("div").Elements("table").First(d => d.Id == "FailedTestTable").Element("tbody")
                .Elements("tr").First(d => d.Id == "TestsContainer").Element("td").Element("table").Element("tbody");

            doc = ChangeNumberOfAllFailedTests(doc, testLoadResult.totalTestsProp.Failed);
            var failedResults = PredicateCreator(t => t.Result, "Failed");
            CreateFailedTestCaseTableRows(tableTestCase, failedResults, testLoadResult);

            return doc;
        }

        private static HtmlDocument ChangeNumberOfAllFailedTests(HtmlDocument doc, string number)
        {
            var failedTableTitleNode = doc.DocumentNode.SelectSingleNode("html/body")
                .Element("div").Elements("div").First(d => d.Id == "test")
                .Element("div").Elements("table").First(d => d.Id == "FailedTestTable")
                .Element("tbody").Elements("tr").First(t => t.Id == "FailedTestsHeader")
                .Elements("td").First(t => t.Id == "number");
            var valueNode = failedTableTitleNode.InnerHtml;
            valueNode = valueNode.Replace("VALUE", number);
            failedTableTitleNode.InnerHtml = HtmlDocument.HtmlEncode(valueNode);

            return doc;
        }

        private static void CreateFailedTestCaseTableRows(HtmlNode tableTestCase, Func<Test, bool> func, TestLoadResult testLoadResult)
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

                tableRowTestCase = HtmlNode.CreateNode($"<td class=\"ClassName\">{test.ClassName}</td>");
                tableTestCase.AppendChild(tableRowTestCase);

                tableRowTestCase = HtmlNode.CreateNode($"<td class=\"StartTime\">{DateTime.Parse(test.StartTime.ToString())}</td>");
                tableTestCase.AppendChild(tableRowTestCase);

                tableRowTestCase = HtmlNode.CreateNode($"<td class=\"statusCount\">{test.Duration}</td>");
                tableTestCase.AppendChild(tableRowTestCase);
            }
        }
    }
}
