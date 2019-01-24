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
		private TestLoadResult _TestLoadResult;
		public HtmlGeneration(string trxFilePath, string outputPath)
		{
			_OutputPath = outputPath;
			_TemplatePath = @"../../template.html";
			_TrxFilePath = trxFilePath;
		}
		public void Generation()
		{
            if (_TestLoadResult == null)
                throw new Exception("No TRX data loaded");

			var document = LoadTemplate(_TemplatePath);

			document = ChangeNameOfDocument(document, _TestLoadResult.totalTestsProp.TestCategory);
            document = TestStatusesTableCreator.CreateStatusesTable(document, _TestLoadResult);
            document = RunTimeSummaryTableCreator.CreateRunTimeSummaryTable(document, _TestLoadResult);
            document = AllFailedTestsTableCreator.CreateAllFailedTestsTable(document, _TestLoadResult);
            document = AllTestesGroupedByClassesTableCreator.CreateAllTestesGroupedByClassesTable(document, _TestLoadResult);

			ExportToFile(document.DocumentNode.InnerHtml);
		}

        private HtmlDocument testTemplateTable(HtmlDocument doc)
        {
            var template = LoadTemplate(@"../../tableTemplate.html");

            throw new NotImplementedException();
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

		private Func<Test, bool> PredicateCreator<T>(Func<Test, T> selector, T expected) where T : IEquatable<T>
		{
			return t => selector(t).Equals(expected);
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
				case "Inconclusive": color = "BlueViolet"; break;
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

        //TODO: write exeption
		private HtmlDocument LoadTemplate(string templatePath)
		{
			var doc = new HtmlDocument();
			doc.Load(templatePath, Encoding.UTF8);

			return doc;
		}
	}
}
