﻿using System;
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

            //var headerNode = document.DocumentNode.SelectSingleNode("body").Elements("div").First(d => d.Id == "header");
            //var text = headerNode.Element("h1").InnerText;
            //text = text.Replace("TEMPLATE", _TestLoadResult.totalTestsProp.TestCategory);
            //headerNode.Element("h1").InnerHtml = HtmlDocument.HtmlEncode(text);


            //HtmlNode totalResultNode = HtmlNode.CreateNode($"<p>" +
            //	$"Result: <strong>{CreateColoredResult(TotalResultString())}</strong></br>" +
            //	$"Start time: {_TestLoadResult.totalTestsProp.StartTime}</br>" +
            //	$"End time: {_TestLoadResult.totalTestsProp.FinishTime}</br>" +
            //	$"Test duration: {TestsDuration(_TestLoadResult.totalTestsProp.StartTime, _TestLoadResult.totalTestsProp.FinishTime)}</p>");
            //HtmlNode summaryOfTestResultsHeader = HtmlNode.CreateNode($"<h2>Summary of the test cases</h2>");
            //HtmlNode summaryList = HtmlNode.CreateNode($"<ul>" +
            //	$"<li>{GetTotalTestNumber()} tests in TOTAL </li>" +
            //	$"<li>{_TestLoadResult.totalTestsProp.Passed} tests <font color=green>PASSED</font></li>" +
            //	$"<li>{_TestLoadResult.totalTestsProp.Failed} tests <font color=red>FAILED</font></li>" +
            //	$"<li>{_TestLoadResult.totalTestsProp.Inconclusive} tests are <font color=blue>INCONCLUSIVE</font></li>" +
            //	$"<li>{_TestLoadResult.totalTestsProp.Warning} tests have <font color=orange>WARNING</font></li>" +
            //	$"</ul>");

            //htmlNodes.Add(totalResultNode);
            //htmlNodes.Add(summaryOfTestResultsHeader);
            //htmlNodes.Add(summaryList);


            //HtmlNode resultOfTestCasesTitle = HtmlNode.CreateNode("<h2>Results of the test cases</h2>");
            //htmlNodes.Add(resultOfTestCasesTitle);

            //HtmlNode startForm = HtmlNode.CreateNode("<form action=\"\" method=\"post\">" + CreateClassFilter(_TestLoadResult.AllTestedClasses) + CreateResultFilter() + "<input type=\"submit\" value=\"SHOW\"></form>");
            //htmlNodes.Add(startForm);

            //HtmlNode classFilter = HtmlNode.CreateNode(CreateClassFilter(_TestLoadResult.AllTestedClasses));
            //htmlNodes.Add(classFilter);

            //HtmlNode resultFilter = HtmlNode.CreateNode(CreateResultFilter());
            //htmlNodes.Add(resultFilter);

            //HtmlNode buttonShow = HtmlNode.CreateNode("<input type=\"submit\" value=\"SHOW\"></form>");
            //htmlNodes.Add(buttonShow);

            //HtmlNode php = HtmlNode.CreateNode("<p><?php echo \"cos\"?></p>");
            //htmlNodes.Add(php);


            var tableTestCase = document.DocumentNode.SelectSingleNode("/html/body/table/tbody").Elements("tr").First(d => d.Id == "TestsContainer").SelectSingleNode("td/table/thead");

            HtmlNode tableRowTestCase = HtmlNode.CreateNode("<tr class=\"Test\"></tr>");

            tableTestCase.AppendChild(tableRowTestCase);
            tableTestCase = tableTestCase.LastChild;

            tableRowTestCase = HtmlNode.CreateNode("<th scope=\"row\" class=\"column1\">7/28/2014 9:47:32 PM</th>");
            tableTestCase.AppendChild(tableRowTestCase);

            tableRowTestCase = HtmlNode.CreateNode("<td class=\"failed\">FAILED</td>");
            tableTestCase.AppendChild(tableRowTestCase);

            tableRowTestCase = HtmlNode.CreateNode("<td class=\"Function\">MixedStatuses</td>");
            tableTestCase.AppendChild(tableRowTestCase);
			//var headerNode = document.DocumentNode.SelectSingleNode("body").Elements("div").First(d => d.Id == "header");
			var headerNode = document.DocumentNode.SelectSingleNode("body").Element("div").Element("div");
			var text = headerNode.Element("h1").InnerText;
			text = text.Replace("TEMPLATE", _TestLoadResult.totalTestsProp.TestCategory);
			headerNode.Element("h1").InnerHtml = HtmlDocument.HtmlEncode(text);

            tableRowTestCase = HtmlNode.CreateNode("<td class=\"Message\"></td>");
            tableTestCase.AppendChild(tableRowTestCase);

			var totalResultNode = document.DocumentNode.SelectSingleNode("body")
				.Element("div").Elements("div").First(d => d.Id == "test")
				.Element("div").Elements("table").First(d => d.Id == "DetailsTable StatusesTable")
				.Element("tbody").Elements("tr").First(d => d.Id == "odd");
			var value = totalResultNode.Element("td").InnerText;
			value = value.Replace("VALUE", _TestLoadResult.totalTestsProp.Total);
			totalResultNode.Element("td").InnerHtml = HtmlDocument.HtmlEncode(value);


			HtmlNode.CreateNode($"<p>" +
				$"Result: <strong>{CreateColoredResult(TotalResultString())}</strong></br>" +
				$"Start time: {_TestLoadResult.totalTestsProp.StartTime}</br>" +
				$"End time: {_TestLoadResult.totalTestsProp.FinishTime}</br>" +
				$"Test duration: {TestsDuration(_TestLoadResult.totalTestsProp.StartTime, _TestLoadResult.totalTestsProp.FinishTime)}</p>");

			//HtmlNode totalResultNode = HtmlNode.CreateNode($"<p>" +
			//	$"Result: <strong>{CreateColoredResult(TotalResultString())}</strong></br>" +
			//	$"Start time: {_TestLoadResult.totalTestsProp.StartTime}</br>" +
			//	$"End time: {_TestLoadResult.totalTestsProp.FinishTime}</br>" +
			//	$"Test duration: {TestsDuration(_TestLoadResult.totalTestsProp.StartTime, _TestLoadResult.totalTestsProp.FinishTime)}</p>");
			HtmlNode summaryOfTestResultsHeader = HtmlNode.CreateNode($"<h2>Summary of the test cases</h2>");
			HtmlNode summaryList = HtmlNode.CreateNode($"<ul>" +
				$"<li>{GetTotalTestNumber()} tests in TOTAL </li>" +
				$"<li>{_TestLoadResult.totalTestsProp.Passed} tests <font color=green>PASSED</font></li>" +
				$"<li>{_TestLoadResult.totalTestsProp.Failed} tests <font color=red>FAILED</font></li>" +
				$"<li>{_TestLoadResult.totalTestsProp.Inconclusive} tests are <font color=blue>INCONCLUSIVE</font></li>" +
				$"<li>{_TestLoadResult.totalTestsProp.Warning} tests have <font color=orange>WARNING</font></li>" +
				$"</ul>");
            tableRowTestCase = HtmlNode.CreateNode("<td class=\"Message\"></td>");
            tableTestCase.AppendChild(tableRowTestCase);

            tableRowTestCase = HtmlNode.CreateNode("<td class=\"Message\">138.6413 ms</td>");
            tableTestCase.AppendChild(tableRowTestCase);

            IEnumerable<Test> tests = _TestLoadResult.tests;

			List<string> listOfClasses = _TestLoadResult.AllTestedClasses;
			foreach (string e in listOfClasses)
			{
				HtmlNode testNodeClassName = HtmlNode.CreateNode($"<h3>{e}</h3>");
				htmlNodes.Add(testNodeClassName);
				IEnumerable<Test> testsByClassName = _TestLoadResult.tests.Where(x => x.ClassName == e);
				GetMethods(testsByClassName, htmlNodes);
			}

			//IEnumerable<Test>  testsByResult = _TestLoadResult.tests.Where(x => x.Result== "passed");
			//GetMethods(testsByResult, htmlNodes);

			document.DocumentNode.AppendChildren(htmlNodes);

			ExportToFile(document.DocumentNode.InnerHtml);
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
            string filter="<select>";

            foreach(string e in listOfClasses)
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
			doc.Load(templatePath);

			return doc;
		}
	}
}
