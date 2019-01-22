using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using HtmlAgilityPack;

namespace TrxToHtmlConverter
{
    class HtmlGeneration
    {
        private string _OutputPath;
        private string _TemplatePath;
        private string _TrxFilePath;
        private XmlReader _XmlReader;
        private TestLoadResult _TestLoadResult;
        public HtmlGeneration(string trxFilePath, string outputPath)
        {
            _OutputPath = outputPath;
            _TemplatePath = @"../../template.html";
            _TrxFilePath = trxFilePath;
        }
        public void Generation()
        {
            _XmlReader = InitializeTrxData();
            var document = LoadTemplate(_TemplatePath);

            HtmlNodeCollection htmlNodes = new HtmlNodeCollection(document.DocumentNode);

            document = ChangeNameOfDocument(document, _TestLoadResult.totalTestsProp.TestCategory);
            document = ReplaceAllTotalValues(document);
            document = ReplaceAllRunTimeSummaryValues(document);
            document = FillFailedTestsTable(document);
            

            document.DocumentNode.AppendChildren(htmlNodes);

            ExportToFile(document.DocumentNode.InnerHtml);
        }

        private HtmlDocument FillFailedTestsTable(HtmlDocument doc)
        {
            var tableTestCase = doc.DocumentNode.SelectSingleNode("/html/body")
                .Element("div").Elements("div").First(d => d.Id == "test")
                .Element("div").Elements("table").First(d => d.Id == "FailedTestTable").Element("tbody")
                .Elements("tr").First(d => d.Id == "TestsContainer").Element("td").Element("table").Element("tbody");

            doc = ChangeNumberOfAllFailedTests(doc, _TestLoadResult.totalTestsProp.Failed);
            var failedResults = PredicateCreator(t => t.Result, "Failed");
            CreateTestCaseTableRows(tableTestCase, failedResults);

            return doc;
        }

        private HtmlDocument ChangeNumberOfAllFailedTests(HtmlDocument doc, string number)
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

        private HtmlDocument ChangeNameOfDocument(HtmlDocument doc, string nameValue)
        {
            var titleNode = doc.DocumentNode.SelectSingleNode("/html/body")
                .Element("div").Elements("div").First(d => d.Id == "header");
            var valueNode = titleNode.Element("h1").InnerHtml;
            valueNode = valueNode.Replace("TEMPLATE", nameValue);
            titleNode.Element("h1").InnerHtml = HtmlDocument.HtmlEncode(valueNode);

            return doc;
        }

        private HtmlDocument ReplaceAllRunTimeSummaryValues(HtmlDocument doc)
        {
            doc = ReplaceOneRunTimeSummaryValue(doc, "startTime", _TestLoadResult.totalTestsProp.StartTime.ToString());
            doc = ReplaceOneRunTimeSummaryValue(doc, "endTime", _TestLoadResult.totalTestsProp.FinishTime.ToString());
            doc = ReplaceOneRunTimeSummaryValue(doc, "duration", TestsDuration(_TestLoadResult.totalTestsProp.StartTime, _TestLoadResult.totalTestsProp.FinishTime).ToString());

            return doc;
        }

        private HtmlDocument ReplaceOneRunTimeSummaryValue(HtmlDocument doc, string id, string value)
        {
            var totalResultNode = doc.DocumentNode.SelectSingleNode("/html/body")
                .Element("div").Elements("div").First(d => d.Id == "test")
                .Element("div").Elements("table").First(d => d.Id == "SummaryTable")
                .Element("tbody").Elements("tr").First(d => d.Id == id);
            var valueNode = totalResultNode.Element("td").InnerText;
            valueNode = valueNode.Replace("VALUE", value);
            totalResultNode.Element("td").InnerHtml = HtmlDocument.HtmlEncode(valueNode);

            return doc;
        }

        private HtmlDocument ReplaceAllTotalValues(HtmlDocument doc)
        {
            doc = ReplaceOneTotalValue(doc, "total", _TestLoadResult.totalTestsProp.Total);
            doc = ReplaceOneTotalValue(doc, "passed", _TestLoadResult.totalTestsProp.Passed);
            doc = ReplaceOneTotalValue(doc, "failed", _TestLoadResult.totalTestsProp.Failed);
            doc = ReplaceOneTotalValue(doc, "inconclusive", _TestLoadResult.totalTestsProp.Inconclusive);
            doc = ReplaceOneTotalValue(doc, "warning", _TestLoadResult.totalTestsProp.Warning);

            return doc;
        }

        private HtmlDocument ReplaceOneTotalValue(HtmlDocument doc, string id, string value)
        {
            var totalResultNode = doc.DocumentNode.SelectSingleNode("/html/body")
                .Element("div").Elements("div").First(d => d.Id == "test")
                .Element("div").Elements("table").First(d => d.Id == "DetailsTable_StatusesTable")
                .Element("tbody").Elements("tr").First(d => d.Id == id);
            var valueNode = totalResultNode.Element("td").InnerText;
            valueNode = valueNode.Replace("VALUE", value);
            totalResultNode.Element("td").InnerHtml = HtmlDocument.HtmlEncode(valueNode);

            return doc;
        }

        public Func<Test, bool> PredicateCreator<T>(Func<Test, T> selector, T expected) where T : IEquatable<T>
        {
            return t => selector(t).Equals(expected);
        }

        private void CreateTestCaseTableRows(HtmlNode tableTestCase, Func<Test, bool> func)
        {
            foreach (Test test in _TestLoadResult.tests.Where(func))
            {
                HtmlNode tableRowTestCase = HtmlNode.CreateNode("<tr class=\"Test\"></tr>");

                tableTestCase.AppendChild(tableRowTestCase);
                tableTestCase = tableTestCase.LastChild;

                tableRowTestCase = HtmlNode.CreateNode($"<th scope=\"row\" class=\"column1\"></th>");
                tableTestCase.AppendChild(tableRowTestCase);

                tableRowTestCase = HtmlNode.CreateNode($"<td class=\"{test.Result}\">{CreateColoredResult(test.Result)}</td>");
                tableTestCase.AppendChild(tableRowTestCase);

                tableRowTestCase = HtmlNode.CreateNode($"<td class=\"Function\">{test.MethodName}</td>");
                tableTestCase.AppendChild(tableRowTestCase);

                tableRowTestCase = HtmlNode.CreateNode("<td class=\"Message\"></td>");
                tableTestCase.AppendChild(tableRowTestCase);

                tableRowTestCase = HtmlNode.CreateNode($"<td class=\"Message\">{test.ID}</td>");
                tableTestCase.AppendChild(tableRowTestCase);

                tableRowTestCase = HtmlNode.CreateNode("<td class=\"Message\"></td>");
                tableTestCase.AppendChild(tableRowTestCase);
            }


        }

        private void GetMethods(IEnumerable<Test> methodList, HtmlNodeCollection htmlNode)
        {
            foreach (Test method in methodList)
            {
                HtmlNode testNodeMethod = HtmlNode.CreateNode($"<ul>" +
                $"<li>{CreateColoredResult(method.Result)}" +
                $"<b>Method Name</b> <br> {method.MethodName}</li>" +
                $"</ul>");
                htmlNode.Add(testNodeMethod);
            }
        }

        public string CreateClassFilter(List<string> listOfClasses)
        {
            string filter = "<select>";

            foreach (string e in listOfClasses)
            {
                filter += $"<option>{e}</option>";
            }
            filter += "<option>Show all</option></select>";

            return filter;
        }

        public string CreateResultFilter()
        {
            return "<select><option>Passed</option><option>Failed</option><option>Inconclusive</option><option>Warning</option></select>";
        }

        private string CreateColoredResult(string result)
        {
            string color = "";
            switch (result)
            {
                case "Passed": color = "green"; break;
                case "Failed": color = "red"; break;
                case "Inconclusive": color = "blue"; break;
                case "Warning": color = "orange"; break;
            }

            return $"<font color={color} size=4><strong>{result}</strong></font><br>";
        }

        private TimeSpan TestsDuration(DateTime startTime, DateTime stopTime)
        {
            return stopTime - startTime;
        }

        private XmlReader InitializeTrxData()
        {
            XmlReader xmlReader = new XmlReader(_TrxFilePath);
            _TestLoadResult = xmlReader.CreateTestLoadResult();

            return xmlReader;
        }

        private string TotalResultString()
        {
            if ((Convert.ToInt32(_TestLoadResult.totalTestsProp.Passed) / Convert.ToInt32(GetTotalTestNumber())) == 1)
                return "Passed";
            else
                return "Failed";
        }

        private string GetTotalTestNumber()
        {
            return _TestLoadResult.totalTestsProp.Total;
        }

        private void ExportToFile(string fileContent)
        {
            StreamWriter fw = new StreamWriter(_OutputPath);
            fw.Write(fileContent);
            fw.Close();
        }

        private HtmlDocument LoadTemplate(string templatePath)
        {
            var doc = new HtmlDocument();
            doc.Load(templatePath, Encoding.UTF8);

            return doc;
        }
    }
}
