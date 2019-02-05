using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using HtmlAgilityPack;

namespace TrxToHtmlConverter
{
    public class HtmlGeneration
    {
        private string _OutputPath;
        private string _TemplatePath;
        private string _TrxFilePath;
        private string _ChangeSetNumber;
        private string _PbiNumber;
        private TestLoadResult _TestLoadResult;

        public HtmlGeneration(string trxFilePath, string outputPath, string changeSetNumber, string pbiNumber)
        {
            _OutputPath = outputPath;
            _TemplatePath = @"../../../TrxToHtmlConverter/template.html";
            _TrxFilePath = trxFilePath;
            _ChangeSetNumber = changeSetNumber;
            _PbiNumber = pbiNumber;
        }
        public void Generation()
        {
            if (_TestLoadResult == null)
                throw new Exception("No TRX data loaded");

            var document = LoadTemplate(_TemplatePath);

            document = ChangeNameOfDocument(document, _TestLoadResult.totalTestsProp.TestCategory);
            document = LoadTables(document);

            ExportToFile(document.DocumentNode.InnerHtml);
        }

        private HtmlDocument testTemplateTable(HtmlDocument doc)
        {
            var template = LoadTemplate(@"../../../TrxToHtmlConverter/tableTemplate.html");

            throw new NotImplementedException();
        }

        private HtmlDocument ChangeNameOfDocument(HtmlDocument doc, string nameValue)
        {
            var titleNode = doc.DocumentNode.SelectSingleNode("/html/body")
                .Elements("div").First(d => d.Id == "header");
            var valueNode = titleNode.Element("h1").InnerHtml;
            valueNode = valueNode.Replace("TEMPLATE", nameValue);
            titleNode.Element("h1").InnerHtml = HtmlDocument.HtmlEncode(valueNode);

            return doc;
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
                case "Failed": color = "SaddleBrown"; break;
                case "Inconclusive": color = "green"; break;
                case "NotExecuted": color = "DarkGoldenrod"; break;
                case "Warning": color = "DarkGoldenrod"; break;
            }

            return $"<font color={color} size=4><strong>{result}</strong></font><br>";
        }

        public XmlReader InitializeTrxData()
        {
            XmlReader xmlReader = new XmlReader(_TrxFilePath);
            _TestLoadResult = xmlReader.CreateTestLoadResult();

            return xmlReader;
        }

        private void ExportToFile(string fileContent)
        {
            StreamWriter fw = new StreamWriter(_OutputPath);
            fw.Write(fileContent);
            fw.Close();
        }

        private HtmlDocument LoadTables(HtmlDocument doc)
        {
            doc = TableCreator.CreateTopTables(doc, _TestLoadResult,_PbiNumber, _ChangeSetNumber);
            doc = TableCreator.CreateAllFailedTestsTable(doc, _TestLoadResult);
            doc = TableCreator.CreateAllTestsGroupedByClassesTable(doc, _TestLoadResult);

            return doc;
        }
        //TODO: write exeption
        private HtmlDocument LoadTemplate(string templatePath)
        {
            var doc = new HtmlDocument();
            doc.Load(templatePath, Encoding.UTF8);

            return doc;
        }
    }
}
