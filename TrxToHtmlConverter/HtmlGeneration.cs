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
            _TemplatePath = @"C:\Users\ivakrupa\Downloads\xd.html";
            _TrxFilePath = trxFilePath;
        }
        public void Generation()
        {
            _XmlReader = InitializeTrxData();
            var document = LoadTemplate(_TemplatePath);
            
            HtmlNodeCollection htmlNodes = new HtmlNodeCollection(document.DocumentNode);
            
            var headerNode = document.DocumentNode.SelectSingleNode("body").Elements("div").First(d => d.Id == "header");
            var text = headerNode.Element("h1").InnerText;
            text = text.Replace("TEMPLATE", _TestLoadResult.totalTestsProp.TestCategory);
            headerNode.Element("h1").InnerHtml = HtmlDocument.HtmlEncode(text);


            HtmlNode totalResultNode = HtmlNode.CreateNode($"<p>" +
                $"Result: <strong>{TotalResultString()}</strong></br>" +
                $"Start time: {_TestLoadResult.totalTestsProp.StartTime}</br>" +
                $"End time: {_TestLoadResult.totalTestsProp.FinishTime}</br>" +
                $"Test duration: {TestsDuration(_TestLoadResult.totalTestsProp.StartTime, _TestLoadResult.totalTestsProp.FinishTime)}</p>");
            HtmlNode summaryOfTestResultsHeader = HtmlNode.CreateNode($"<h2>Summary of the test cases</h2>");
            HtmlNode summaryList = HtmlNode.CreateNode( $"<ul>" +
                $"<li>{GetTotalTestNumber()} tests in total</li>" +
                $"<li>{_TestLoadResult.totalTestsProp.Passed} tests <code style='color:green;'>PASSED</code></li>" +
                $"<li>{_TestLoadResult.totalTestsProp.Failed} tests <code style='color:red;'>FAILED</code></li>" +
                $"<li>{_TestLoadResult.totalTestsProp.Inconclusive} tests are <code>INCONCLUSIVE</code></li>" +
                $"<li>{_TestLoadResult.totalTestsProp.Warning} tests have <code style='color:orange;'>WARNING</code></li>" +
                $"</ul>");

            htmlNodes.Add(totalResultNode);
            htmlNodes.Add(summaryOfTestResultsHeader);
            htmlNodes.Add(summaryList);
            

            HtmlNode resultOfTestCasesTitle = HtmlNode.CreateNode("<h2>Results of the test cases</h2>");
            htmlNodes.Add(resultOfTestCasesTitle);

            IEnumerable<Test> tests = _TestLoadResult.tests;
            foreach (Test test in tests)
            {
                HtmlNode testNode = HtmlNode.CreateNode($"<p>{test.ID}</p>");
                htmlNodes.Add(testNode);
            }

            

            document.DocumentNode.AppendChildren(htmlNodes);

            ExportToFile(document.DocumentNode.InnerHtml);
        }


        private TimeSpan TestsDuration(string startTime, string stopTime)
        {
            return Convert.ToDateTime(stopTime) - Convert.ToDateTime(startTime);
        }

        private XmlReader InitializeTrxData()
        {
            XmlReader xmlReader = new XmlReader(_TrxFilePath);
            _TestLoadResult = xmlReader.CreateTestLoadResult();
            
            return xmlReader;
        }
        
        private string TotalResultString()
        {
            if ((Convert.ToInt32(_TestLoadResult.totalTestsProp.Passed)/Convert.ToInt32(GetTotalTestNumber())) == 1)
                return "<code style='color:green;'>PASSED</code>";
            else
                return "<code style='color:red;'>FAILED</code>";
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
            doc.Load(templatePath);

            return doc;
        }
    }
}
